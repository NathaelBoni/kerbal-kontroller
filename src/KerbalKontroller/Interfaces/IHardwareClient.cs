using KerbalKontroller.Resources;

namespace KerbalKontroller.Interfaces
{
    public interface IHardwareClient
    {
        JoystickAxis ReadLeftJoyStick();
        JoystickAxis ReadExtraLeftJoyStick();
        JoystickAxis ReadRightJoyStick();
        JoystickAxis ReadExtraRightJoyStick();
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
        ButtonState ReadTimeWarpPlusButton();
        ButtonState ReadTimeWarpMinusButton();
        ButtonState ReadNextVesselButton();
        ButtonState ReadPreviousVesselButton();
        ButtonState ReadPauseButton();
        ButtonState ReadQuickSaveButton();
        ButtonState ReadQuickLoadButton();
        void WriteLandingGearLed(LedState ledState);
        void WriteBrakesLed(LedState ledState);
        void WriteLightsLed(LedState ledState);
        void WriteSASLed(LedState ledState);
        void WriteRCSLed(LedState ledState);
        void WritePrecisionLed(LedState ledState);
        void WriteSASFreeLed(LedState ledState);
        void WriteSASProgradeLed(LedState ledState);
        void WriteSASRetrogadeLed(LedState ledState);
        void WriteSASRadialInLed(LedState ledState);
        void WriteSASRadialOutLed(LedState ledState);
        void WriteSASNormalLed(LedState ledState);
        void WriteSASAntiNormalLed(LedState ledState);
        void WriteSASTargetLed(LedState ledState);
        void WriteSASAntiTargetLed(LedState ledState);
    }
}
