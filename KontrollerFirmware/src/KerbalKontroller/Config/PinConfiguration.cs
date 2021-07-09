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
        public byte RightJoyStickX { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte RightJoyStickY { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte ExtraRightJoyStickY { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Analog)]
        public byte AnalogThrottle { get; set; }
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
        public byte PrecisionLeftButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte PrecisionRightButton { get; set; }
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
        public byte SASRetrogradeButton { get; set; }
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
        public byte KeyQ { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KeyW { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KeyE { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KeyA { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KeyS { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KeyD { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KeyC { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KeyF { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KeyB { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KeyR { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KeyP { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte KeyI { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte CameraCycleButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte OrbitalViewButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte IncreaseTimeWarpButton { get; set; }
        [PinMode(PinModes.Input), PinType(PinTypes.Digital)]
        public byte DecreaseTimeWarpButton { get; set; }
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
        public byte SASLedS0 { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASLedS1 { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASLedS2 { get; set; }
        [PinMode(PinModes.Output), PinType(PinTypes.Digital)]
        public byte SASLedS3 { get; set; }
    }
}
