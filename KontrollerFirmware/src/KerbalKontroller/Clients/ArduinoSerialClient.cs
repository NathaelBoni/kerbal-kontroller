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
                serialPort = new SerialPort(appSettings.COMPort, 9600);
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedCallback);

                serialPort.Open();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Fatal error - unable to connect to Arduino");
                throw;
            }

            controller = new ControllerFunctions(appSettings.NumberOfDigitalPorts);

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
            var data = ((SerialPort)sender).ReadLine();
            controller.DeserializeData(data);
        }

        public void Dispose()
        {
            serialPort.Close();
        }

        private float AnalogConversor(int analogValue, bool inverted)
        {
            var convertedValue = (analogValue / appSettings.HalfMaximumInput) - 1;

            if (Math.Abs(convertedValue) < appSettings.JoystickDeadZone) return 0;
            return inverted ? -convertedValue : convertedValue;
        }

        public JoystickAxis ReadLeftJoystick(bool xAxisInverted = false, bool yAxisInverted = false)
        {
            return new JoystickAxis
            {
                XValue = AnalogConversor(controller.GetAnalogValue(pinConfiguration.LeftJoyStickX), xAxisInverted),
                YValue = AnalogConversor(controller.GetAnalogValue(pinConfiguration.LeftJoyStickY), yAxisInverted)
            };
        }

        public JoystickAxis ReadRightJoystick(bool xAxisInverted = false, bool yAxisInverted = false)
        {
            return new JoystickAxis
            {
                XValue = AnalogConversor(controller.GetAnalogValue(pinConfiguration.RightJoyStickX), xAxisInverted),
                YValue = AnalogConversor(controller.GetAnalogValue(pinConfiguration.RightJoyStickY), yAxisInverted)
            };
        }

        public JoystickAxis ReadExtraLeftJoystick(bool xAxisInverted = false, bool yAxisInverted = false)
        {
            return new JoystickAxis
            {
                XValue = AnalogConversor(controller.GetAnalogValue(pinConfiguration.ExtraLeftJoyStickX), xAxisInverted),
                YValue = AnalogConversor(controller.GetAnalogValue(pinConfiguration.ExtraLeftJoyStickY), yAxisInverted)
            };
        }

        public JoystickAxis ReadExtraRightJoystick(bool xAxisInverted = false, bool yAxisInverted = false)
        {
            return new JoystickAxis
            {
                XValue = AnalogConversor(controller.GetAnalogValue(pinConfiguration.ExtraRightJoyStickX), xAxisInverted),
                YValue = AnalogConversor(controller.GetAnalogValue(pinConfiguration.ExtraRightJoyStickY), yAxisInverted)
            };
        }

        public JoystickAxis ReadAnalogThrottle()
        {
            return new JoystickAxis
            {
                YValue = AnalogConversor(controller.GetAnalogValue(pinConfiguration.AnalogThrottle), false)
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
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.StageButton));
        }

        public DigitalState ReadAbortButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.AbortButton));
        }

        public DigitalState ReadLandingGearSwitch()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.LandingGearSwitch));
        }

        public DigitalState ReadBrakesSwitch()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.BrakesSwitch));
        }

        public DigitalState ReadBrakesButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.BrakesButton));
        }

        public DigitalState ReadLightsSwitch()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.LightsSwitch));
        }

        public DigitalState ReadSASSwitch()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.SASSwitch));
        }

        public DigitalState ReadRCSSwitch()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.RCSSwitch));
        }

        public DigitalState ReadPrecisionSwitch()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.PrecisionSwitch));
        }

        public DigitalState ReadPrecisionButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.PrecisionButton));
        }

        public DigitalState ReadAction1Button()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.Action1Button));
        }

        public DigitalState ReadAction2Button()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.Action2Button));
        }

        public DigitalState ReadAction3Button()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.Action3Button));
        }

        public DigitalState ReadAction4Button()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.Action4Button));
        }

        public DigitalState ReadAction5Button()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.Action5Button));
        }

        public DigitalState ReadAction6Button()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.Action6Button));
        }

        public DigitalState ReadAction7Button()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.Action7Button));
        }

        public DigitalState ReadAction8Button()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.Action8Button));
        }

        public DigitalState ReadAction9Button()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.Action9Button));
        }

        public DigitalState ReadAction10Button()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.Action10Button));
        }

        public SASModes? ReadSASModesButtons()
        {
            if (controller.GetDigitalValue(pinConfiguration.SASFreeButton))
                return SASModes.Free;
            if (controller.GetDigitalValue(pinConfiguration.SASManeuverButton))
                return SASModes.Maneuver;
            if (controller.GetDigitalValue(pinConfiguration.SASProgradeButton))
                return SASModes.Prograde;
            if (controller.GetDigitalValue(pinConfiguration.SASRetrogradeButton))
                return SASModes.Retrograde;
            if (controller.GetDigitalValue(pinConfiguration.SASRadialInButton))
                return SASModes.RadialIn;
            if (controller.GetDigitalValue(pinConfiguration.SASRadialOutButton))
                return SASModes.RadialOut;
            if (controller.GetDigitalValue(pinConfiguration.SASNormalButton))
                return SASModes.Normal;
            if (controller.GetDigitalValue(pinConfiguration.SASAntiNormalButton))
                return SASModes.AntiNormal;
            if (controller.GetDigitalValue(pinConfiguration.SASTargetButton))
                return SASModes.Target;
            if (controller.GetDigitalValue(pinConfiguration.SASAntiTargetButton))
                return SASModes.AntiTarget;
            return null;
        }

        public DigitalState ReadKerbalUseButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.KerbalUseButton));
        }

        public DigitalState ReadKerbalJumpButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.KerbalJumpButton));
        }

        public DigitalState ReadKerbalRunButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.KerbalRunButton));
        }

        public DigitalState ReadKerbalBoardButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.KerbalBoardButton));
        }

        public DigitalState ReadKerbalLetGoButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.KerbalLetGoButton));
        }

        public DigitalState ReadKerbalParachuteButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.KerbalParachuteButton));
        }

        public DigitalState ReadKerbalJetPackButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.KerbalJetPackButton));
        }

        public DigitalState ReadKerbalConstructionButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.KerbalConstructionMode));
        }

        public DigitalState ReadCameraCycleButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.CameraCycleButton));
        }

        public DigitalState ReadOrbitalViewButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.OrbitalViewButton));
        }

        public DigitalState ReadIncreaseTimeWarpButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.IncreaseTimeWarpButton));
        }

        public DigitalState ReadDecreaseTimeWarpButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.DecreaseTimeWarpButton));
        }

        public DigitalState ReadNextVesselButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.NextVesselButton));
        }

        public DigitalState ReadPreviousVesselButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.PreviousVesselButton));
        }

        public DigitalState ReadPauseButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.PauseButton));
        }

        public DigitalState ReadQuickSaveButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.QuickSaveButton));
        }

        public DigitalState ReadQuickLoadButton()
        {
            return new DigitalState(controller.GetDigitalValue(pinConfiguration.QuickLoadButton));
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
