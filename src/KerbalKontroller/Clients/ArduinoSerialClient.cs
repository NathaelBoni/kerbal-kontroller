using KerbalKontroller.Config;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using KerbalKontroller.Resources.Attributes;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using command = KerbalKontroller.Resources.ArduinoCommands;

namespace KerbalKontroller.Clients
{
    public class ArduinoSerialClient : IHardwareClient, IDisposable
    {
		private const float HALF_MAXIMUM_INPUT = 511.5f;
		private readonly byte[] serialPorts;
		private readonly IDictionary<SASModes, byte> sASModeToLedPin;

		private readonly SerialPort serialPort;
		private readonly ControllerFunctions controller;
        private readonly PinConfiguration pinConfiguration;
        private readonly AppSettings appSettings;
        private readonly Logger logger;

        public ArduinoSerialClient(PinConfiguration pinConfiguration, AppSettings appSettings, Logger logger)
        {
            this.pinConfiguration = pinConfiguration;
            this.appSettings = appSettings;
            this.logger = logger;
            this.logger.Information("Configuring Arduino...");

            try
            {
                serialPort = new SerialPort("COM11", 9600);
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedCallback);

                serialPort.Open();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Fatal error - unable to connect to Arduino");
                throw;
            }

			controller = new ControllerFunctions();

			this.serialPorts = new byte[] { 0, 1 };
			ConfigurePins();

			sASModeToLedPin = CreateSASModeToLedPinConversion();

			this.logger.Information("Arduino configured");
        }

		private void ConfigurePins()
        {
			SetInputPins();
			SetOutputPins();
			SetSASLedPins();
			WriteBytes(command.Configured, command.Nop);
		}

		private void WriteBytes(byte byte1, byte byte2)
        {
			serialPort.Write(new byte[] { byte1, byte2 }, 0, 2);
		}

		private void SetInputPins()
		{
			var analogPins = GetAnalogPins(pinConfiguration, PinModes.Input);
			var digitalPins = GetDigitalPins(pinConfiguration, PinModes.Input);

			var pins = new List<byte>();
			pins.AddRange(analogPins);
			pins.AddRange(digitalPins);

			foreach (var pin in pins)
				WriteBytes(command.Input, pin);
		}

		private void SetOutputPins()
		{
			var analogPins = GetAnalogPins(pinConfiguration, PinModes.Output);
			var digitalPins = GetDigitalPins(pinConfiguration, PinModes.Output);

			var pins = new List<byte>();
			pins.AddRange(analogPins);
			pins.AddRange(digitalPins);

			foreach (var pin in pins)
				WriteBytes(command.Output, pin);
		}

		private void SetSASLedPins()
        {
			var sasLedPins = GetDigitalPins(pinConfiguration, PinModes.SASLeds);

			foreach (var pin in sasLedPins)
				WriteBytes(command.OutputSASLeds, pin);
        }

		private IDictionary<SASModes, byte> CreateSASModeToLedPinConversion()
		{
			return new Dictionary<SASModes, byte>()
			{
				{ SASModes.Free, pinConfiguration.SASFreeLed },
				{ SASModes.Maneuver, pinConfiguration.SASManeuverLed },
				{ SASModes.Prograde, pinConfiguration.SASProgradeLed },
				{ SASModes.Retrograde, pinConfiguration.SASRetrogradeLed },
				{ SASModes.RadialIn, pinConfiguration.SASRadialInLed },
				{ SASModes.RadialOut, pinConfiguration.SASRadialOutLed },
				{ SASModes.Normal, pinConfiguration.SASNormalLed },
				{ SASModes.AntiNormal, pinConfiguration.SASAntiNormalLed },
				{ SASModes.Target, pinConfiguration.SASTargetLed },
				{ SASModes.AntiTarget, pinConfiguration.SASAntiTargetLed }
			};
		}

		private IEnumerable<byte> GetAnalogPins(PinConfiguration pins, PinModes pinMode)
		{
			return pins.GetType().GetProperties()
				.Where(_ => _.CustomAttributes.Any(__ => __.AttributeType == typeof(PinTypeAttribute) &&
					(PinTypes)__.ConstructorArguments[0].Value == PinTypes.Analog))
				.Where(_ => _.CustomAttributes.Any(__ => __.AttributeType == typeof(PinModeAttribute) &&
					(PinModes)__.ConstructorArguments[0].Value == pinMode))
				.Select(_ => (byte)_.GetValue(pinConfiguration))
				.Where(_ => !serialPorts.Contains(_))
				.Select(_ => (byte)(_ + appSettings.NumberOfDigitalPorts));
		}

		private IEnumerable<byte> GetDigitalPins(PinConfiguration pins, PinModes pinMode)
		{
			return pins.GetType().GetProperties()
				.Where(_ => _.CustomAttributes.Any(__ => __.AttributeType == typeof(PinTypeAttribute) &&
					(PinTypes)__.ConstructorArguments[0].Value == PinTypes.Digital))
				.Where(_ => _.CustomAttributes.Any(__ => __.AttributeType == typeof(PinModeAttribute) &&
					(PinModes)__.ConstructorArguments[0].Value == pinMode))
				.Select(_ => (byte)_.GetValue(pinConfiguration))
				.Where(_ => !serialPorts.Contains(_));
		}

		private void DataReceivedCallback(object sender, SerialDataReceivedEventArgs ev)
        {
            var data = ((SerialPort)sender).ReadExisting();
			controller.DeserializeData(data);
        }

        public void Dispose()
        {
            serialPort.Close();
        }

		private float AnalogConversor(int analogValue, bool inverted)
		{
			var convertedValue = (analogValue / HALF_MAXIMUM_INPUT) - 1;

			if (Math.Abs(convertedValue) < appSettings.JoystickDeadZone) return 0;
			return inverted ? -convertedValue : convertedValue;
		}

		public JoystickAxis ReadLeftJoystick(bool xAxisInverted = false, bool yAxisInverted = false)
        {
			return new JoystickAxis
			{
				XValue = AnalogConversor(controller.LeftJoystickX, xAxisInverted),
				YValue = AnalogConversor(controller.LeftJoystickY, yAxisInverted)
			};
        }

		public JoystickAxis ReadRightJoystick(bool xAxisInverted = false, bool yAxisInverted = false)
        {
			return new JoystickAxis
			{
				XValue = AnalogConversor(controller.RightJoystickX, xAxisInverted),
				YValue = AnalogConversor(controller.RightJoystickY, yAxisInverted)
			};
		}

		public JoystickAxis ReadExtraLeftJoystick(bool xAxisInverted = false, bool yAxisInverted = false)
        {
			return new JoystickAxis
			{
				XValue = AnalogConversor(controller.ExtraLeftJoystickX, xAxisInverted),
				YValue = AnalogConversor(controller.ExtraLeftJoystickY, yAxisInverted)
			};
		}

		public JoystickAxis ReadExtraRightJoystick(bool xAxisInverted = false, bool yAxisInverted = false)
		{
			return new JoystickAxis
			{
				XValue = AnalogConversor(controller.ExtraRightJoystickX, xAxisInverted),
				YValue = AnalogConversor(controller.ExtraRightJoystickY, yAxisInverted)
			};
		}

		public JoystickAxis ReadAnalogThrottle()
        {
			return new JoystickAxis
			{
				YValue = AnalogConversor(controller.AnalogThrottle, false)
			};
		}

		public DigitalState ReadFullThrottleButton()
		{
			throw new NotImplementedException();
		}

		public DigitalState ReadCutOffThrottleButton()
		{
			throw new NotImplementedException();
		}

		public DigitalState ReadStageButton()
		{
			return new DigitalState
			{
				Active = controller.StageButton
			};
		}

		public DigitalState ReadAbortButton()
		{
			return new DigitalState
			{
				Active = controller.AbortButton
			};
		}

		public DigitalState ReadLandingGearSwitch()
		{
			return new DigitalState
			{
				Active = controller.LandingGearSwitch
			};
		}

		public DigitalState ReadBrakesSwitch()
		{
			return new DigitalState
			{
				Active = controller.BrakesSwitch
			};
		}

		public DigitalState ReadBrakesButton()
		{
			return new DigitalState
			{
				Active = controller.BrakesButton
			};
		}

		public DigitalState ReadLightsSwitch()
		{
			return new DigitalState
			{
				Active = controller.LightsSwitch
			};
		}

		public DigitalState ReadSASSwitch()
		{
			return new DigitalState
			{
				Active = controller.SASSwitch
			};
		}

		public DigitalState ReadRCSSwitch()
		{
			return new DigitalState
			{
				Active = controller.RCSSwitch
			};
		}

		public DigitalState ReadPrecisionSwitch()
		{
			return new DigitalState
			{
				Active = controller.PrecisionSwitch
			};
		}

		public DigitalState ReadPrecisionButton()
		{
			return new DigitalState
			{
				Active = controller.PrecisionButton
			};
		}

		public DigitalState ReadAction1Button()
		{
			return new DigitalState
			{
				Active = controller.Action1Button
			};
		}

		public DigitalState ReadAction2Button()
		{
			return new DigitalState
			{
				Active = controller.Action2Button
			};
		}

		public DigitalState ReadAction3Button()
		{
			return new DigitalState
			{
				Active = controller.Action3Button
			};
		}

		public DigitalState ReadAction4Button()
		{
			return new DigitalState
			{
				Active = controller.Action4Button
			};
		}

		public DigitalState ReadAction5Button()
		{
			return new DigitalState
			{
				Active = controller.Action5Button
			};
		}

		public DigitalState ReadAction6Button()
		{
			return new DigitalState
			{
				Active = controller.Action6Button
			};
		}

		public DigitalState ReadAction7Button()
		{
			return new DigitalState
			{
				Active = controller.Action7Button
			};
		}

		public DigitalState ReadAction8Button()
		{
			return new DigitalState
			{
				Active = controller.Action8Button
			};
		}

		public DigitalState ReadAction9Button()
		{
			return new DigitalState
			{
				Active = controller.Action9Button
			};
		}

		public DigitalState ReadAction10Button()
		{
			return new DigitalState
			{
				Active = controller.Action10Button
			};
		}

		public SASModes? ReadSASModesButtons()
		{
			if (controller.SASFreeButton)
				return SASModes.Free;
			if (controller.SASManeuverButton)
				return SASModes.Maneuver;
			if (controller.SASProgradeButton)
				return SASModes.Prograde;
			if (controller.SASRetrogradeButton)
				return SASModes.Retrograde;
			if (controller.SASRadialInButton)
				return SASModes.RadialIn;
			if (controller.SASRadialOutButton)
				return SASModes.RadialOut;
			if (controller.SASNormalButton)
				return SASModes.Normal;
			if (controller.SASAntiNormalButton)
				return SASModes.AntiNormal;
			if (controller.SASTargetButton)
				return SASModes.Target;
			if (controller.SASAntiTargetButton)
				return SASModes.AntiTarget;
			return null;
		}

		public DigitalState ReadKerbalUseButton()
		{
			return new DigitalState
			{
				Active = controller.KerbalUseButton
			};
		}

		public DigitalState ReadKerbalJumpButton()
		{
			return new DigitalState
			{
				Active = controller.KerbalJumpButton
			};
		}

		public DigitalState ReadKerbalRunButton()
		{
			return new DigitalState
			{
				Active = controller.KerbalRunButton
			};
		}

		public DigitalState ReadKerbalBoardButton()
		{
			return new DigitalState
			{
				Active = controller.KerbalBoardButton
			};
		}

		public DigitalState ReadKerbalLetGoButton()
		{
			return new DigitalState
			{
				Active = controller.KerbalLetGoButton
			};
		}

		public DigitalState ReadKerbalParachuteButton()
		{
			return new DigitalState
			{
				Active = controller.KerbalParachuteButton
			};
		}

		public DigitalState ReadKerbalJetPackButton()
		{
			return new DigitalState
			{
				Active = controller.KerbalJetPackButton
			};
		}

		public DigitalState ReadKerbalConstructionButton()
		{
			return new DigitalState
			{
				Active = controller.KerbalConstructionButton
			};
		}

		public DigitalState ReadCameraCycleButton()
		{
			return new DigitalState
			{
				Active = controller.CameraCycleButton
			};
		}

		public DigitalState ReadOrbitalViewButton()
		{
			return new DigitalState
			{
				Active = controller.OrbitalViewButton
			};
		}

		public DigitalState ReadIncreaseTimeWarpButton()
		{
			return new DigitalState
			{
				Active = controller.IncreaseTimeWarpButton
			};
		}

		public DigitalState ReadDecreaseTimeWarpButton()
		{
			return new DigitalState
			{
				Active = controller.DecreaseTimeWarpButton
			};
		}

		public DigitalState ReadNextVesselButton()
		{
			return new DigitalState
			{
				Active = controller.NextVesselButton
			};
		}

		public DigitalState ReadPreviousVesselButton()
		{
			return new DigitalState
			{
				Active = controller.PreviousVesselButton
			};
		}

		public DigitalState ReadPauseButton()
		{
			return new DigitalState
			{
				Active = controller.PauseButton
			};
		}

		public DigitalState ReadQuickSaveButton()
		{
			return new DigitalState
			{
				Active = controller.QuickSaveButton
			};
		}

		public DigitalState ReadQuickLoadButton()
		{
			return new DigitalState
			{
				Active = controller.QuickLoadButton
			};
		}

		public void WriteLandingGearLed(bool ledState)
		{
			throw new NotImplementedException();
		}

		public void WriteBrakesLed(bool ledState)
		{
			throw new NotImplementedException();
		}

		public void WriteLightsLed(bool ledState)
		{
			throw new NotImplementedException();
		}

		public void WriteSASLed(bool ledState)
		{
			throw new NotImplementedException();
		}

		public void WriteRCSLed(bool ledState)
		{
			throw new NotImplementedException();
		}

		public void WritePrecisionLed(bool ledState)
		{
			throw new NotImplementedException();
		}

		public void WriteSASModeLed(SASModes sASMode)
		{
			WriteBytes(command.SetSASLed, sASModeToLedPin[sASMode]);
		}
	}
}
