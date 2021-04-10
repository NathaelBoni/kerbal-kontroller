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
        private readonly Logger logger;

        public ArduinoClient(PinConfiguration pinConfiguration, AppSettings appSettings, ArduinoInfo model, Logger logger)
        {
            this.logger = logger;
            this.logger.Information("Configuring Arduino...");

            try
            {
                arduinoDriver = new ArduinoDriver.ArduinoDriver(model.ArduinoModel, true);
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

            this.logger.Information("Arduino configured!");
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

        public DigitalState ReadSASFreeButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASFreeButton)
            };
        }

        public DigitalState ReadSASManeuverButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASManeuverButton)
            };
        }

        public DigitalState ReadSASProgradeButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASProgradeButton)
            };
        }

        public DigitalState ReadSASRetrogadeButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASRetrogadeButton)
            };
        }

        public DigitalState ReadSASRadialInButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASRadialInButton)
            };
        }

        public DigitalState ReadSASRadialOutButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASRadialOutButton)
            };
        }

        public DigitalState ReadSASNormalButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASNormalButton)
            };
        }

        public DigitalState ReadSASAntiNormalButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASAntiNormalButton)
            };
        }

        public DigitalState ReadSASTargetButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASTargetButton)
            };
        }

        public DigitalState ReadSASAntiTargetButton()
        {
            return new DigitalState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASAntiTargetButton)
            };
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

        public void WriteSASFreeLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASFreeLed, ledState);
        }

        public void WriteSASProgradeLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASProgradeLed, ledState);
        }

        public void WriteSASRetrogadeLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASRetrogadeLed, ledState);
        }

        public void WriteSASRadialInLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASRadialInLed, ledState);
        }

        public void WriteSASRadialOutLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASRadialOutLed, ledState);
        }

        public void WriteSASNormalLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASNormalLed, ledState);
        }

        public void WriteSASAntiNormalLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASAntiNormalLed, ledState);
        }

        public void WriteSASTargetLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASTargetLed, ledState);
        }

        public void WriteSASAntiTargetLed(bool ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASAntiTargetLed, ledState);
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
