using KerbalKontroller.Resources;

namespace KerbalKontroller.Interfaces
{
    public interface IHardwareClient
    {
        JoystickAxis ReadLeftJoystick();
        JoystickAxis ReadExtraLeftJoystick();
        JoystickAxis ReadRightJoystick();
        JoystickAxis ReadExtraRightJoystick();
        JoystickAxis ReadAnalogThrottle();
        ButtonState ReadFullThrottleButton();
        ButtonState ReadCutOffThrottleButton();
        ButtonState ReadStageButton();
        ButtonState ReadAbortButton();
        ButtonState ReadLandingGearSwitch();
        ButtonState ReadBrakesSwitch();
        ButtonState ReadBrakesButton();
        ButtonState ReadLightsSwitch();
        ButtonState ReadSASSwitch();
        ButtonState ReadRCSSwitch();
        ButtonState ReadPrecisionSwitch();
        ButtonState ReadAction1Button();
        ButtonState ReadAction2Button();
        ButtonState ReadAction3Button();
        ButtonState ReadAction4Button();
        ButtonState ReadAction5Button();
        ButtonState ReadAction6Button();
        ButtonState ReadAction7Button();
        ButtonState ReadAction8Button();
        ButtonState ReadAction9Button();
        ButtonState ReadAction10Button();
        ButtonState ReadSASFreeButton();
        ButtonState ReadSASManeuverButton();
        ButtonState ReadSASProgradeButton();
        ButtonState ReadSASRetrogadeButton();
        ButtonState ReadSASRadialInButton();
        ButtonState ReadSASRadialOutButton();
        ButtonState ReadSASNormalButton();
        ButtonState ReadSASAntiNormalButton();
        ButtonState ReadSASTargetButton();
        ButtonState ReadSASAntiTargetButton();
        ButtonState ReadKerbalUseButton();
        ButtonState ReadKerbalBoard();
        ButtonState ReadKerbalParachute();
        ButtonState ReadKerbalJetPack();
        ButtonState ReadOrbitalViewButton();
        ButtonState ReadIncreaseTimeWarpButton();
        ButtonState ReadDecreaseTimeWarpButton();
        ButtonState ReadNextVesselButton();
        ButtonState ReadPreviousVesselButton();
        ButtonState ReadPauseButton();
        ButtonState ReadQuickSaveButton();
        ButtonState ReadQuickLoadButton();
        void WriteLandingGearLed(bool ledState);
        void WriteBrakesLed(bool ledState);
        void WriteLightsLed(bool ledState);
        void WriteSASLed(bool ledState);
        void WriteRCSLed(bool ledState);
        void WritePrecisionLed(bool ledState);
        void WriteSASFreeLed(bool ledState);
        void WriteSASProgradeLed(bool ledState);
        void WriteSASRetrogadeLed(bool ledState);
        void WriteSASRadialInLed(bool ledState);
        void WriteSASRadialOutLed(bool ledState);
        void WriteSASNormalLed(bool ledState);
        void WriteSASAntiNormalLed(bool ledState);
        void WriteSASTargetLed(bool ledState);
        void WriteSASAntiTargetLed(bool ledState);
    }
}
