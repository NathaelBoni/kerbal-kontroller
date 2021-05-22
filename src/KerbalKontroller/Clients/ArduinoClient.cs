using ArduinoDriver.SerialProtocol;
using ArduinoDriver;
using KerbalKontroller.Config;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using System;
using System.Linq;
using Serilog.Core;
using KerbalKontroller.Resources.Attributes;
using System.Collections.Generic;

namespace KerbalKontroller.Clients
{
    public class ArduinoClient : IHardwareClient
    {
        private const float HALF_MAXIMUM_INPUT = 511.5f;

        private readonly ArduinoDriver.ArduinoDriver arduinoDriver;
        private readonly int numberOfDigitalPorts;
        private readonly byte[] serialPorts;
        private readonly PinConfiguration pinConfiguration;
        private readonly AppSettings settings;
        private readonly IDictionary<SASModes, byte> SASModeToLedPin;
        private readonly IDictionary<byte, SASModes> ButtonPinToSASMode;
        private readonly Logger logger;

        public ArduinoClient(PinConfiguration pinConfiguration, AppSettings appSettings, ArduinoInfo model, Logger logger)
        {
            this.logger = logger;
            this.logger.Information("Configuring Arduino...");

            try
            {
                arduinoDriver = new ArduinoDriver.ArduinoDriver(model.ArduinoModel, "COM11", true);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Fatal error - unable to connect to Arduino");
                throw;
            }

            this.numberOfDigitalPorts = model.NumberOfDigitalPorts;
            this.serialPorts = new byte[] { 0, 1 };

            this.pinConfiguration = pinConfiguration;
            this.settings = appSettings;

            SetInputPins();
            SetOutputPins();

            this.SASModeToLedPin = CreateSASModeToLedPinConversion();
            this.ButtonPinToSASMode = CreateButtonPinToSASModeConversion();
            this.logger.Information("Arduino configured!");
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

        private IDictionary<byte, SASModes> CreateButtonPinToSASModeConversion()
        {
            var pinToSASModeConversion = new Dictionary<byte, SASModes>();

            if (!serialPorts.Contains(pinConfiguration.SASFreeButton)) pinToSASModeConversion.Add(pinConfiguration.SASFreeButton, SASModes.Free);
            if (!serialPorts.Contains(pinConfiguration.SASManeuverButton)) pinToSASModeConversion.Add(pinConfiguration.SASManeuverButton, SASModes.Maneuver);
            if (!serialPorts.Contains(pinConfiguration.SASProgradeButton)) pinToSASModeConversion.Add(pinConfiguration.SASProgradeButton, SASModes.Prograde);
            if (!serialPorts.Contains(pinConfiguration.SASRetrogradeButton)) pinToSASModeConversion.Add(pinConfiguration.SASRetrogradeButton, SASModes.Retrograde);
            if (!serialPorts.Contains(pinConfiguration.SASRadialInButton)) pinToSASModeConversion.Add(pinConfiguration.SASRadialInButton, SASModes.RadialIn);
            if (!serialPorts.Contains(pinConfiguration.SASRadialOutButton)) pinToSASModeConversion.Add(pinConfiguration.SASRadialOutButton, SASModes.RadialOut);
            if (!serialPorts.Contains(pinConfiguration.SASNormalButton)) pinToSASModeConversion.Add(pinConfiguration.SASNormalButton, SASModes.Normal);
            if (!serialPorts.Contains(pinConfiguration.SASAntiNormalButton)) pinToSASModeConversion.Add(pinConfiguration.SASAntiNormalButton, SASModes.AntiNormal);
            if (!serialPorts.Contains(pinConfiguration.SASTargetButton)) pinToSASModeConversion.Add(pinConfiguration.SASTargetButton, SASModes.Target);
            if (!serialPorts.Contains(pinConfiguration.SASAntiTargetButton)) pinToSASModeConversion.Add(pinConfiguration.SASAntiTargetButton, SASModes.AntiTarget);

            return pinToSASModeConversion;
        }

        public JoystickAxis ReadLeftJoystick(bool xAxisInverted = false, bool yAxisInverted = false)
        {
            return ReadJoystickAxis(pinConfiguration.LeftJoyStickX, pinConfiguration.LeftJoyStickY, xAxisInverted, yAxisInverted);
        }

        public JoystickAxis ReadRightJoystick(bool xAxisInverted = false, bool yAxisInverted = false)
        {
            return ReadJoystickAxis(pinConfiguration.RightJoyStickX, pinConfiguration.RightJoyStickY, xAxisInverted, yAxisInverted);
        }

        public JoystickAxis ReadExtraLeftJoystick(bool xAxisInverted = false, bool yAxisInverted = false)
        {
            return ReadJoystickAxis(pinConfiguration.ExtraLeftJoyStickX, pinConfiguration.ExtraLeftJoyStickY, xAxisInverted, yAxisInverted);
        }

        public JoystickAxis ReadExtraRightJoystick(bool xAxisInverted = false, bool yAxisInverted = false)
        {
            return ReadJoystickAxis(pinConfiguration.ExtraRightJoyStickX, pinConfiguration.ExtraRightJoyStickY, xAxisInverted, yAxisInverted);
        }

        private JoystickAxis ReadJoystickAxis(byte joystickX, byte joystickY, bool xAxisInverted = false, bool yAxisInverted = false)
        {
            return new JoystickAxis
            {
                XValue = ReadFromAnalogPin(joystickX, xAxisInverted),
                YValue = ReadFromAnalogPin(joystickY, yAxisInverted)
            };
        }

        public JoystickAxis ReadAnalogThrottle()
        {
            return new JoystickAxis
            {
                YValue = ReadFromAnalogPin(pinConfiguration.AnalogThrottle)
            };
        }

        public DigitalState ReadFullThrottleButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.FullThrottleButton)
            };
        }

        public DigitalState ReadCutOffThrottleButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.CutOffThrottleButton)
            };
        }

        public DigitalState ReadStageButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.StageButton)
            };
        }

        public DigitalState ReadAbortButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.AbortButton)
            };
        }

        public DigitalState ReadLandingGearSwitch()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.LandingGearSwitch)
            };
        }

        public DigitalState ReadBrakesSwitch()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.BrakesSwitch)
            };
        }

        public DigitalState ReadBrakesButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.BrakesButton)
            };
        }

        public DigitalState ReadLightsSwitch()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.LightsSwitch)
            };
        }

        public DigitalState ReadSASSwitch()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASSwitch)
            };
        }

        public DigitalState ReadRCSSwitch()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.RCSSwitch)
            };
        }

        public DigitalState ReadPrecisionSwitch()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.PrecisionSwitch)
            };
        }

        public DigitalState ReadPrecisionButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.PrecisionButton)
            };
        }

        public DigitalState ReadAction1Button()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action1Button)
            };
        }

        public DigitalState ReadAction2Button()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action2Button)
            };
        }

        public DigitalState ReadAction3Button()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action3Button)
            };
        }

        public DigitalState ReadAction4Button()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action4Button)
            };
        }

        public DigitalState ReadAction5Button()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action5Button)
            };
        }

        public DigitalState ReadAction6Button()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action6Button)
            };
        }

        public DigitalState ReadAction7Button()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action7Button)
            };
        }

        public DigitalState ReadAction8Button()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action8Button)
            };
        }

        public DigitalState ReadAction9Button()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action9Button)
            };
        }

        public DigitalState ReadAction10Button()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action10Button)
            };
        }

        public SASModes? ReadSASModesButtons()
        {
            foreach (var pinToSASModePair in ButtonPinToSASMode)
            {
                if (ReadFromDigitalPin(pinToSASModePair.Key))
                    return pinToSASModePair.Value;
            }
            return null;
        }

        public DigitalState ReadKerbalUseButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalUseButton)
            };
        }

        public DigitalState ReadKerbalJumpButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalJumpButton)
            };
        }

        public DigitalState ReadKerbalRunButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalRunButton)
            };
        }

        public DigitalState ReadKerbalBoardButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalBoardButton)
            };
        }

        public DigitalState ReadKerbalLetGoButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalLetGoButton)
            };
        }

        public DigitalState ReadKerbalParachuteButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalParachuteButton)
            };
        }

        public DigitalState ReadKerbalJetPackButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalJetPackButton)
            };
        }

        public DigitalState ReadKerbalConstructionButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalConstructionMode)
            };
        }

        public DigitalState ReadCameraCycleButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.CameraCycleButton)
            };
        }

        public DigitalState ReadOrbitalViewButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.OrbitalViewButton)
            };
        }

        public DigitalState ReadIncreaseTimeWarpButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.IncreaseTimeWarpButton)
            };
        }

        public DigitalState ReadDecreaseTimeWarpButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.DecreaseTimeWarpsButton)
            };
        }

        public DigitalState ReadNextVesselButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.NextVesselButton)
            };
        }

        public DigitalState ReadPreviousVesselButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.PreviousVesselButton)
            };
        }

        public DigitalState ReadPauseButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.PauseButton)
            };
        }

        public DigitalState ReadQuickSaveButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.QuickSaveButton)
            };
        }

        public DigitalState ReadQuickLoadButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.QuickLoadButton)
            };
        }

        public void WriteLandingGearLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.LandingGearLed, ledState);
        }

        public void WriteBrakesLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.BrakesLed, ledState);
        }

        public void WriteLightsLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.LightsLed, ledState);
        }

        public void WriteSASLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASLed, ledState);
        }

        public void WriteRCSLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.RCSLed, ledState);
        }

        public void WritePrecisionLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.PrecisionLed, ledState);
        }

        public void WriteSASModeLed(SASModes sasMode)
        {
            foreach(var sasModeToPinPair in SASModeToLedPin)
            {
                if(sasModeToPinPair.Key == sasMode)
                    WriteToDigitalPin(sasModeToPinPair.Value, true);
                else
                    WriteToDigitalPin(sasModeToPinPair.Value, false);
            }
        }

        private void SetInputPins()
        {
            var analogPins = GetAnalogPins(pinConfiguration, PinModes.Input);
            var digitalPins = GetDigitalPins(pinConfiguration, PinModes.Input);

            var pins = new List<byte>();
            pins.AddRange(analogPins);
            pins.AddRange(digitalPins);

            foreach (var pin in pins)
                arduinoDriver.Send(new PinModeRequest(pin, PinMode.Input));
        }

        private void SetOutputPins()
        {
            var analogPins = GetAnalogPins(pinConfiguration, PinModes.Output);
            var digitalPins = GetDigitalPins(pinConfiguration, PinModes.Output);

            var pins = new List<byte>();
            pins.AddRange(analogPins);
            pins.AddRange(digitalPins);

            foreach (var pin in pins)
                arduinoDriver.Send(new PinModeRequest(pin, PinMode.Output));
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
                .Select(_ => (byte)(_ + numberOfDigitalPorts));
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

        private float ReadFromAnalogPin(byte pin, bool inverted = false)
        {
            var analogResponse = arduinoDriver.Send(new AnalogReadRequest(pin));
            return AnalogConversor(analogResponse.PinValue, inverted);
        }

        private bool ReadFromDigitalPin(byte pin)
        {
            if (pin == 0) return false;
            var digitalResponse = arduinoDriver.Send(new DigitalReadRequest(pin));
            return digitalResponse.PinValue == DigitalValue.High;
        }

        private void WriteToDigitalPin(byte pin, bool ledState)
        {
            var digitalValue = ledState ? DigitalValue.High : DigitalValue.Low;
            arduinoDriver.Send(new DigitalWriteRequest(pin, digitalValue));
        }

        private float AnalogConversor(int analogValue, bool inverted)
        {
            var convertedValue = (analogValue / HALF_MAXIMUM_INPUT) - 1;

            if (Math.Abs(convertedValue) < settings.JoystickDeadZone) return 0;
            return inverted ? -convertedValue : convertedValue;
        }
    }
}
