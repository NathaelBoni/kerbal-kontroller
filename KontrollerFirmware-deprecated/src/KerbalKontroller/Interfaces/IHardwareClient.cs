﻿using KerbalKontroller.Resources;
using System;

namespace KerbalKontroller.Interfaces
{
    public interface IHardwareClient : IDisposable
    {
        JoystickAxis ReadLeftJoystick(bool xAxisInverted = false, bool yAxisInverted = false);
        JoystickAxis ReadExtraLeftJoystick(bool xAxisInverted = false, bool yAxisInverted = false);
        JoystickAxis ReadRightJoystick(bool xAxisInverted = false, bool yAxisInverted = false);
        JoystickAxis ReadExtraRightJoystick(bool xAxisInverted = false, bool yAxisInverted = false);
        JoystickAxis ReadAnalogThrottle();
        DigitalState ReadStageButton();
        DigitalState ReadAbortButton();
        DigitalState ReadLandingGearSwitch();
        DigitalState ReadBrakesSwitch();
        DigitalState ReadBrakesButton();
        DigitalState ReadLightsSwitch();
        DigitalState ReadSASSwitch();
        DigitalState ReadRCSSwitch();
        DigitalState ReadPrecisionSwitch();
        DigitalState ReadPrecisionLeftButton();
        DigitalState ReadPrecisionRightButton();
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
        SASModes? ReadSASModesButtons();
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
        void WriteSASModeLed(SASModes sASMode);
    }
}
