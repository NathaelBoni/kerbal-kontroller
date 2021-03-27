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
                XValue = ReadAnalogPin(pinConfiguration.LeftJoyStickX),
                YValue = ReadAnalogPin(pinConfiguration.LeftJoyStickY, true)
            };
        }

        public JoystickAxis ReadRightJoyStick()
        {
            return new JoystickAxis
            {
                XValue = ReadAnalogPin(pinConfiguration.RightJoyStickX),
                YValue = ReadAnalogPin(pinConfiguration.RightJoyStickY),
            };
        }

        public JoystickAxis ReadExtraLeftJoyStick()
        {
            return new JoystickAxis
            {
                XValue = ReadAnalogPin(pinConfiguration.ExtraLeftJoyStickX),
                YValue = ReadAnalogPin(pinConfiguration.ExtraLeftJoyStickY, true)
            };
        }

        public JoystickAxis ReadExtraRightJoyStick()
        {
            return new JoystickAxis
            {
                XValue = ReadAnalogPin(pinConfiguration.ExtraRightJoyStickX),
                YValue = ReadAnalogPin(pinConfiguration.ExtraRightJoyStickY),
            };
        }

        public JoystickAxis ReadAnalogThrottle()
        {
            return new JoystickAxis
            {
                YValue = ReadAnalogPin(pinConfiguration.AnalogThrottle),
            };
        }

        public ButtonState ReadFullThrottleButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.FullThrottleButton),
            };
        }

        public ButtonState ReadCutOffThrottleButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.CutOffThrottleButton),
            };
        }

        public ButtonState ReadStageButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.StageButton),
            };
        }

        public ButtonState ReadAbortButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.AbortButton),
            };
        }

        public ButtonState ReadLandingGearSwitch()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.LandingGearSwitch),
            };
        }

        public ButtonState ReadBrakesSwitch()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.BrakesSwitch),
            };
        }

        public ButtonState ReadBrakesButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.BrakesButton),
            };
        }

        public ButtonState ReadLightsSwitch()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.LightsSwitch),
            };
        }

        public ButtonState ReadSASSwitch()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.SASSwitch),
            };
        }

        public ButtonState ReadRCSSwitch()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.RCSSwitch),
            };
        }

        public ButtonState ReadPrecisionSwitch()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.PrecisionSwitch),
            };
        }

        public ButtonState ReadAction1Button()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.Action1Button),
            };
        }

        public ButtonState ReadAction2Button()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.Action2Button),
            };
        }

        public ButtonState ReadAction3Button()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.Action3Button),
            };
        }

        public ButtonState ReadAction4Button()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.Action4Button),
            };
        }

        public ButtonState ReadAction5Button()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.Action5Button),
            };
        }

        public ButtonState ReadAction6Button()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.Action6Button),
            };
        }

        public ButtonState ReadAction7Button()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.Action7Button),
            };
        }

        public ButtonState ReadAction8Button()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.Action8Button),
            };
        }

        public ButtonState ReadAction9Button()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.Action9Button),
            };
        }

        public ButtonState ReadAction10Button()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.Action10Button),
            };
        }

        public ButtonState ReadSASFreeButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.SASFreeButton),
            };
        }

        public ButtonState ReadSASProgradeButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.SASProgradeButton),
            };
        }

        public ButtonState ReadSASRetrogadeButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.SASRetrogadeButton),
            };
        }

        public ButtonState ReadSASRadialInButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.SASRadialInButton),
            };
        }

        public ButtonState ReadSASRadialOutButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.SASRadialOutButton),
            };
        }

        public ButtonState ReadSASNormalButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.SASNormalButton),
            };
        }

        public ButtonState ReadSASAntiNormalButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.SASAntiNormalButton),
            };
        }

        public ButtonState ReadSASTargetButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.SASTargetButton),
            };
        }

        public ButtonState ReadSASAntiTargetButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.SASAntiTargetButton),
            };
        }

        public ButtonState ReadKerbalUseButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.KerbalUseButton),
            };
        }

        public ButtonState ReadKerbalBoard()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.KerbalBoard),
            };
        }

        public ButtonState ReadKerbalParachute()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.KerbalParachute),
            };
        }

        public ButtonState ReadKerbalJetPack()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.KerbalJetPack),
            };
        }

        public ButtonState ReadOrbitalViewButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.OrbitalViewButton),
            };
        }

        public ButtonState ReadTimeWarpPlusButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.TimeWarpPlusButton),
            };
        }

        public ButtonState ReadTimeWarpMinusButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.TimeWarpMinusButton),
            };
        }

        public ButtonState ReadNextVesselButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.NextVesselButton),
            };
        }

        public ButtonState ReadPreviousVesselButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.PreviousVesselButton),
            };
        }

        public ButtonState ReadPauseButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.PauseButton),
            };
        }

        public ButtonState ReadQuickSaveButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.QuickSaveButton),
            };
        }

        public ButtonState ReadQuickLoadButton()
        {
            return new ButtonState
            {
                Active = ReadDigitalPin(pinConfiguration.QuickLoadButton),
            };
        }

        private float ReadAnalogPin(byte pin, bool inverted = false)
        {
            var analogResponse = ArduinoDriver.Send(new AnalogReadRequest(pin));
            return AnalogConversor(analogResponse.PinValue, inverted);
        }

        private bool ReadDigitalPin(byte pin)
        {
            var digitalResponse = ArduinoDriver.Send(new DigitalReadRequest(pin));
            return digitalResponse.PinValue == DigitalValue.High;
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
