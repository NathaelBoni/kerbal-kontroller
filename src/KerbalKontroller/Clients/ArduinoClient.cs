using ArduinoDriver.SerialProtocol;
using ArduinoDriver;
using ArduinoUploader.Hardware;
using KerbalKontroller.Config;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using System;
using System.Linq;
using Serilog.Core;

namespace KerbalKontroller.Clients
{
    public class ArduinoClient : IHardwareClient
    {
        private const float HALF_MAXIMUM_INPUT = 511.5f;

        private readonly ArduinoDriver.ArduinoDriver arduinoDriver;
        private readonly PinConfiguration pinConfiguration;
        private readonly Logger logger;
        private readonly float deadZoneThreshold;

        public ArduinoClient(PinConfiguration pinConfiguration, AppSettings appSettings, ArduinoModel model, Logger logger)
        {
            this.logger = logger;
            this.logger.Information("Configuring Arduino...");

            try
            {
                arduinoDriver = new ArduinoDriver.ArduinoDriver(model, true);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex, "Fatal error - unable to connect to Arduino");
                throw;
            }

            this.pinConfiguration = pinConfiguration;
            deadZoneThreshold = appSettings.JoystickDeadZone;

            SetInputPins();
            SetOutputPins();

            this.logger.Information("Arduino configured!");
        }

        public JoystickAxis ReadLeftJoyStick()
        {
            return new JoystickAxis
            {
                XValue = ReadFromAnalogPin(pinConfiguration.LeftJoyStickX),
                YValue = ReadFromAnalogPin(pinConfiguration.LeftJoyStickY, true)
            };
        }

        public JoystickAxis ReadRightJoyStick()
        {
            return new JoystickAxis
            {
                XValue = ReadFromAnalogPin(pinConfiguration.RightJoyStickX),
                YValue = ReadFromAnalogPin(pinConfiguration.RightJoyStickY)
            };
        }

        public JoystickAxis ReadExtraLeftJoyStick()
        {
            return new JoystickAxis
            {
                XValue = ReadFromAnalogPin(pinConfiguration.ExtraLeftJoyStickX),
                YValue = ReadFromAnalogPin(pinConfiguration.ExtraLeftJoyStickY, true)
            };
        }

        public JoystickAxis ReadExtraRightJoyStick()
        {
            return new JoystickAxis
            {
                XValue = ReadFromAnalogPin(pinConfiguration.ExtraRightJoyStickX),
                YValue = ReadFromAnalogPin(pinConfiguration.ExtraRightJoyStickY)
            };
        }

        public JoystickAxis ReadAnalogThrottle()
        {
            return new JoystickAxis
            {
                YValue = ReadFromAnalogPin(pinConfiguration.AnalogThrottle)
            };
        }

        public ButtonState ReadFullThrottleButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.FullThrottleButton)
            };
        }

        public ButtonState ReadCutOffThrottleButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.CutOffThrottleButton)
            };
        }

        public ButtonState ReadStageButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.StageButton)
            };
        }

        public ButtonState ReadAbortButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.AbortButton)
            };
        }

        public ButtonState ReadLandingGearSwitch()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.LandingGearSwitch)
            };
        }

        public ButtonState ReadBrakesSwitch()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.BrakesSwitch)
            };
        }

        public ButtonState ReadBrakesButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.BrakesButton)
            };
        }

        public ButtonState ReadLightsSwitch()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.LightsSwitch)
            };
        }

        public ButtonState ReadSASSwitch()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASSwitch)
            };
        }

        public ButtonState ReadRCSSwitch()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.RCSSwitch)
            };
        }

        public ButtonState ReadPrecisionSwitch()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.PrecisionSwitch)
            };
        }

        public ButtonState ReadAction1Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action1Button)
            };
        }

        public ButtonState ReadAction2Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action2Button)
            };
        }

        public ButtonState ReadAction3Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action3Button)
            };
        }

        public ButtonState ReadAction4Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action4Button)
            };
        }

        public ButtonState ReadAction5Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action5Button)
            };
        }

        public ButtonState ReadAction6Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action6Button)
            };
        }

        public ButtonState ReadAction7Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action7Button)
            };
        }

        public ButtonState ReadAction8Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action8Button)
            };
        }

        public ButtonState ReadAction9Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action9Button)
            };
        }

        public ButtonState ReadAction10Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action10Button)
            };
        }

        public ButtonState ReadSASFreeButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASFreeButton)
            };
        }

        public ButtonState ReadSASProgradeButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASProgradeButton)
            };
        }

        public ButtonState ReadSASRetrogadeButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASRetrogadeButton)
            };
        }

        public ButtonState ReadSASRadialInButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASRadialInButton)
            };
        }

        public ButtonState ReadSASRadialOutButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASRadialOutButton)
            };
        }

        public ButtonState ReadSASNormalButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASNormalButton)
            };
        }

        public ButtonState ReadSASAntiNormalButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASAntiNormalButton)
            };
        }

        public ButtonState ReadSASTargetButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASTargetButton)
            };
        }

        public ButtonState ReadSASAntiTargetButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASAntiTargetButton)
            };
        }

        public ButtonState ReadKerbalUseButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalUseButton)
            };
        }

        public ButtonState ReadKerbalBoard()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalBoardButton)
            };
        }

        public ButtonState ReadKerbalParachute()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalParachuteButton)
            };
        }

        public ButtonState ReadKerbalJetPack()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalJetPackButton)
            };
        }

        public ButtonState ReadOrbitalViewButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.OrbitalViewButton)
            };
        }

        public ButtonState ReadIncreaseTimeWarpButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.IncreaseTimeWarpButton)
            };
        }

        public ButtonState ReadDecreaseTimeWarpButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.DecreaseTimeWarpsButton)
            };
        }

        public ButtonState ReadNextVesselButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.NextVesselButton)
            };
        }

        public ButtonState ReadPreviousVesselButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.PreviousVesselButton)
            };
        }

        public ButtonState ReadPauseButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.PauseButton)
            };
        }

        public ButtonState ReadUnpauseButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.UnpauseButton)
            };
        }

        public ButtonState ReadQuickSaveButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.QuickSaveButton)
            };
        }

        public ButtonState ReadQuickLoadButton()
        {
            return new ButtonState
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
            var inputPins = pinConfiguration.GetType().GetProperties()
                .Where(_ => _.CustomAttributes.Any(__ => __.AttributeType == typeof(PinModeAttribute) &&
                    (PinModes)__.ConstructorArguments[0].Value == PinModes.Input));

            foreach (var prop in inputPins)
                arduinoDriver.Send(new PinModeRequest((byte)prop.GetValue(pinConfiguration), PinMode.Input));
        }

        private void SetOutputPins()
        {
            var outputPins = pinConfiguration.GetType().GetProperties()
                .Where(_ => _.CustomAttributes.Any(__ => __.AttributeType == typeof(PinModeAttribute) &&
                    (PinModes)__.ConstructorArguments[0].Value == PinModes.Output));

            foreach (var prop in outputPins)
                arduinoDriver.Send(new PinModeRequest((byte)prop.GetValue(pinConfiguration), PinMode.Output));
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
            var deadZone = deadZoneThreshold;
            var convertedValue = (analogValue / HALF_MAXIMUM_INPUT) - 1;

            if (Math.Abs(convertedValue) < deadZone) return 0;
            return inverted ? -convertedValue : convertedValue;
        }
    }
}
