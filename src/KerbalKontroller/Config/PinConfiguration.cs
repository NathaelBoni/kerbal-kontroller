using KerbalKontroller.Resources;
using KerbalKontroller.Resources.Attributes;

namespace KerbalKontroller.Config
{
    public class PinConfiguration
    {
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte LeftJoyStickX { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte LeftJoyStickY { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte ExtraLeftJoyStickX { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte ExtraLeftJoyStickY { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte RightJoyStickX { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte RightJoyStickY { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte ExtraRightJoyStickX { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte ExtraRightJoyStickY { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte AnalogThrottle { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte FullThrottleButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte CutOffThrottleButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte StageButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte AbortButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte LandingGearSwitch { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte BrakesSwitch { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte BrakesButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte LightsSwitch { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte SASSwitch { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte RCSSwitch { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte PrecisionSwitch { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte Action1Button { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte Action2Button { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte Action3Button { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte Action4Button { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte Action5Button { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte Action6Button { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte Action7Button { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte Action8Button { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte Action9Button { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte Action10Button { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte SASFreeButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte SASManeuverButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte SASProgradeButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte SASRetrogadeButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte SASRadialInButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte SASRadialOutButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte SASNormalButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte SASAntiNormalButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte SASTargetButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte SASAntiTargetButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KerbalUseButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KerbalJumpButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KerbalRunButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KerbalBoardButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KerbalLetGoButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KerbalParachuteButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KerbalJetPackButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KerbalConstructionMode { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte CameraCycleButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte OrbitalViewButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte IncreaseTimeWarpButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte DecreaseTimeWarpsButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte NextVesselButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte PreviousVesselButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte PauseButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte QuickSaveButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte QuickLoadButton { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte LandingGearLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte BrakesLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte LightsLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte RCSLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte PrecisionLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASFreeLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASProgradeLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASRetrogadeLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASRadialInLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASRadialOutLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASNormalLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASAntiNormalLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASTargetLed { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASAntiTargetLed { get; set; }
    }
}
