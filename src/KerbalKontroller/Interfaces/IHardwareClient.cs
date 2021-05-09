using KerbalKontroller.Resources;

namespace KerbalKontroller.Interfaces
{
    public interface IHardwareClient
    {
        JoystickAxis ReadLeftJoystick(bool xAxisInverted = false, bool yAxisInverted = false);
        JoystickAxis ReadExtraLeftJoystick(bool xAxisInverted = false, bool yAxisInverted = false);
        JoystickAxis ReadRightJoystick(bool xAxisInverted = false, bool yAxisInverted = false);
        JoystickAxis ReadExtraRightJoystick(bool xAxisInverted = false, bool yAxisInverted = false);
        JoystickAxis ReadAnalogThrottle();
        DigitalState ReadFullThrottleButton();
        DigitalState ReadCutOffThrottleButton();
        DigitalState ReadStageButton();
        DigitalState ReadAbortButton();
        DigitalState ReadLandingGearSwitch();
        DigitalState ReadBrakesSwitch();
        DigitalState ReadBrakesButton();
        DigitalState ReadLightsSwitch();
        DigitalState ReadSASSwitch();
        DigitalState ReadRCSSwitch();
        DigitalState ReadPrecisionSwitch();
        DigitalState ReadAction1Button();
        DigitalState ReadAction2Button();
        DigitalState ReadAction3Button();
        DigitalState ReadAction4Button();
        DigitalState ReadAction5Button();
        DigitalState ReadAction6Button();
        DigitalState ReadAction7Button();
        DigitalState ReadAction8Button();
        DigitalState ReadAction9Button();
        DigitalState ReadAction10Button();
        DigitalState ReadSASFreeButton();
        DigitalState ReadSASManeuverButton();
        DigitalState ReadSASProgradeButton();
        DigitalState ReadSASRetrogadeButton();
        DigitalState ReadSASRadialInButton();
        DigitalState ReadSASRadialOutButton();
        DigitalState ReadSASNormalButton();
        DigitalState ReadSASAntiNormalButton();
        DigitalState ReadSASTargetButton();
        DigitalState ReadSASAntiTargetButton();
        DigitalState ReadKerbalUseButton();
        DigitalState ReadKerbalJumpButton();
        DigitalState ReadKerbalRunButton();
        DigitalState ReadKerbalBoardButton();
        DigitalState ReadKerbalLetGoButton();
        DigitalState ReadKerbalParachuteButton();
        DigitalState ReadKerbalJetPackButton();
        DigitalState ReadKerbalConstructionButton();
        DigitalState ReadCameraCycleButton();
        DigitalState ReadOrbitalViewButton();
        DigitalState ReadIncreaseTimeWarpButton();
        DigitalState ReadDecreaseTimeWarpButton();
        DigitalState ReadNextVesselButton();
        DigitalState ReadPreviousVesselButton();
        DigitalState ReadPauseButton();
        DigitalState ReadQuickSaveButton();
        DigitalState ReadQuickLoadButton();
        void WriteLandingGearLed(bool ledState);
        void WriteBrakesLed(bool ledState);
        void WriteLightsLed(bool ledState);
        void WriteSASLed(bool ledState);
        void WriteRCSLed(bool ledState);
        void WritePrecisionLed(bool ledState);
        void WriteSASFreeLed(bool ledState);
        void WriteSASManeuverLed(bool ledState);
        void WriteSASProgradeLed(bool ledState);
        void WriteSASRetrogradeLed(bool ledState);
        void WriteSASRadialInLed(bool ledState);
        void WriteSASRadialOutLed(bool ledState);
        void WriteSASNormalLed(bool ledState);
        void WriteSASAntiNormalLed(bool ledState);
        void WriteSASTargetLed(bool ledState);
        void WriteSASAntiTargetLed(bool ledState);
    }
}
