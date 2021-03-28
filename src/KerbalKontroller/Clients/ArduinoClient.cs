using ArduinoDriver.SerialProtocol;
using ArduinoDriver;
using ArduinoUploader.Hardware;
using KerbalKontroller.Config;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using System;

namespace KerbalKontroller.Clients
{
    public class ArduinoClient : IHardwareClient
    {
        private const float HALF_MAXIMUM_INPUT = 511.5f;

        private readonly ArduinoDriver.ArduinoDriver ArduinoDriver;
        private readonly PinConfiguration pinConfiguration;
        private readonly float deadZoneThreshold;

        public ArduinoClient(PinConfiguration pinConfiguration, AppSettings appSettings, ArduinoModel model)
        {
            ArduinoDriver = new ArduinoDriver.ArduinoDriver(model, true);
            this.pinConfiguration = pinConfiguration;
            deadZoneThreshold = appSettings.JoystickDeadZone;
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
                YValue = ReadFromAnalogPin(pinConfiguration.RightJoyStickY),
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
                YValue = ReadFromAnalogPin(pinConfiguration.ExtraRightJoyStickY),
            };
        }

        public JoystickAxis ReadAnalogThrottle()
        {
            return new JoystickAxis
            {
                YValue = ReadFromAnalogPin(pinConfiguration.AnalogThrottle),
            };
        }

        public ButtonState ReadFullThrottleButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.FullThrottleButton),
            };
        }

        public ButtonState ReadCutOffThrottleButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.CutOffThrottleButton),
            };
        }

        public ButtonState ReadStageButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.StageButton),
            };
        }

        public ButtonState ReadAbortButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.AbortButton),
            };
        }

        public ButtonState ReadLandingGearSwitch()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.LandingGearSwitch),
            };
        }

        public ButtonState ReadBrakesSwitch()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.BrakesSwitch),
            };
        }

        public ButtonState ReadBrakesButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.BrakesButton),
            };
        }

        public ButtonState ReadLightsSwitch()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.LightsSwitch),
            };
        }

        public ButtonState ReadSASSwitch()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASSwitch),
            };
        }

        public ButtonState ReadRCSSwitch()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.RCSSwitch),
            };
        }

        public ButtonState ReadPrecisionSwitch()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.PrecisionSwitch),
            };
        }

        public ButtonState ReadAction1Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action1Button),
            };
        }

        public ButtonState ReadAction2Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action2Button),
            };
        }

        public ButtonState ReadAction3Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action3Button),
            };
        }

        public ButtonState ReadAction4Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action4Button),
            };
        }

        public ButtonState ReadAction5Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action5Button),
            };
        }

        public ButtonState ReadAction6Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action6Button),
            };
        }

        public ButtonState ReadAction7Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action7Button),
            };
        }

        public ButtonState ReadAction8Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action8Button),
            };
        }

        public ButtonState ReadAction9Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action9Button),
            };
        }

        public ButtonState ReadAction10Button()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.Action10Button),
            };
        }

        public ButtonState ReadSASFreeButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASFreeButton),
            };
        }

        public ButtonState ReadSASProgradeButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASProgradeButton),
            };
        }

        public ButtonState ReadSASRetrogadeButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASRetrogadeButton),
            };
        }

        public ButtonState ReadSASRadialInButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASRadialInButton),
            };
        }

        public ButtonState ReadSASRadialOutButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASRadialOutButton),
            };
        }

        public ButtonState ReadSASNormalButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASNormalButton),
            };
        }

        public ButtonState ReadSASAntiNormalButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASAntiNormalButton),
            };
        }

        public ButtonState ReadSASTargetButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASTargetButton),
            };
        }

        public ButtonState ReadSASAntiTargetButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.SASAntiTargetButton),
            };
        }

        public ButtonState ReadKerbalUseButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalUseButton),
            };
        }

        public ButtonState ReadKerbalBoard()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalBoard),
            };
        }

        public ButtonState ReadKerbalParachute()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalParachute),
            };
        }

        public ButtonState ReadKerbalJetPack()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.KerbalJetPack),
            };
        }

        public ButtonState ReadOrbitalViewButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.OrbitalViewButton),
            };
        }

        public ButtonState ReadTimeWarpPlusButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.TimeWarpPlusButton),
            };
        }

        public ButtonState ReadTimeWarpMinusButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.TimeWarpMinusButton),
            };
        }

        public ButtonState ReadNextVesselButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.NextVesselButton),
            };
        }

        public ButtonState ReadPreviousVesselButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.PreviousVesselButton),
            };
        }

        public ButtonState ReadPauseButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.PauseButton),
            };
        }

        public ButtonState ReadQuickSaveButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.QuickSaveButton),
            };
        }

        public ButtonState ReadQuickLoadButton()
        {
            return new ButtonState
            {
                Active = ReadFromDigitalPin(pinConfiguration.QuickLoadButton),
            };
        }

        public void WriteLandingGearLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.LandingGearLed, ledState);
        }

        public void WriteBrakesLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.BrakesLed, ledState);
        }

        public void WriteLightsLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.LightsLed, ledState);
        }

        public void WriteSASLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASLed, ledState);
        }

        public void WriteRCSLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.RCSLed, ledState);
        }

        public void WritePrecisionLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.PrecisionLed, ledState);
        }

        public void WriteSASFreeLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASFreeLed, ledState);
        }

        public void WriteSASProgradeLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASProgradeLed, ledState);
        }

        public void WriteSASRetrogadeLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASRetrogadeLed, ledState);
        }

        public void WriteSASRadialInLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASRadialInLed, ledState);
        }

        public void WriteSASRadialOutLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASRadialOutLed, ledState);
        }

        public void WriteSASNormalLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASNormalLed, ledState);
        }

        public void WriteSASAntiNormalLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASAntiNormalLed, ledState);
        }

        public void WriteSASTargetLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASTargetLed, ledState);
        }

        public void WriteSASAntiTargetLed(LedState ledState)
        {
            WriteToDigitalPin(pinConfiguration.SASAntiTargetLed, ledState);
        }

        private float ReadFromAnalogPin(byte pin, bool inverted = false)
        {
            var analogResponse = ArduinoDriver.Send(new AnalogReadRequest(pin));
            return AnalogConversor(analogResponse.PinValue, inverted);
        }

        private bool ReadFromDigitalPin(byte pin)
        {
            var digitalResponse = ArduinoDriver.Send(new DigitalReadRequest(pin));
            return digitalResponse.PinValue == DigitalValue.High;
        }

        private void WriteToDigitalPin(byte pin, LedState ledState)
        {
            var digitalValue = DigitalValueConversor(ledState);
            ArduinoDriver.Send(new DigitalWriteRequest(13, digitalValue));
        }

        private float AnalogConversor(int analogValue, bool inverted)
        {
            var deadZone = deadZoneThreshold;
            var convertedValue = (analogValue / HALF_MAXIMUM_INPUT) - 1;

            if (Math.Abs(convertedValue) < deadZone) return 0;
            return inverted ? -convertedValue : convertedValue;
        }

        private DigitalValue DigitalValueConversor(LedState ledState)
        {
            if (ledState == LedState.Off) return DigitalValue.Low;
            return DigitalValue.High;
        }
    }
}
