using KerbalKontroller.Resources;

namespace KerbalKontroller.Config
{
    public class PinConfiguration
    {
        [PinMode(PinModes.Input)]
        public byte LeftJoyStickX { get; set; }
        [PinMode(PinModes.Input)]
        public byte LeftJoyStickY { get; set; }
        [PinMode(PinModes.Input)]
        public byte ExtraLeftJoyStickX { get; set; }
        [PinMode(PinModes.Input)]
        public byte ExtraLeftJoyStickY { get; set; }
        [PinMode(PinModes.Input)]
        public byte RightJoyStickX { get; set; }
        [PinMode(PinModes.Input)]
        public byte RightJoyStickY { get; set; }
        [PinMode(PinModes.Input)]
        public byte ExtraRightJoyStickX { get; set; }
        [PinMode(PinModes.Input)]
        public byte ExtraRightJoyStickY { get; set; }
        [PinMode(PinModes.Input)]
        public byte AnalogThrottle { get; set; }
        [PinMode(PinModes.Input)]
        public byte FullThrottleButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte CutOffThrottleButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte StageButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte AbortButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte LandingGearSwitch { get; set; }
        [PinMode(PinModes.Input)]
        public byte BrakesSwitch { get; set; }
        [PinMode(PinModes.Input)]
        public byte BrakesButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte LightsSwitch { get; set; }
        [PinMode(PinModes.Input)]
        public byte SASSwitch { get; set; }
        [PinMode(PinModes.Input)]
        public byte RCSSwitch { get; set; }
        [PinMode(PinModes.Input)]
        public byte PrecisionSwitch { get; set; }
        [PinMode(PinModes.Input)]
        public byte Action1Button { get; set; }
        [PinMode(PinModes.Input)]
        public byte Action2Button { get; set; }
        [PinMode(PinModes.Input)]
        public byte Action3Button { get; set; }
        [PinMode(PinModes.Input)]
        public byte Action4Button { get; set; }
        [PinMode(PinModes.Input)]
        public byte Action5Button { get; set; }
        [PinMode(PinModes.Input)]
        public byte Action6Button { get; set; }
        [PinMode(PinModes.Input)]
        public byte Action7Button { get; set; }
        [PinMode(PinModes.Input)]
        public byte Action8Button { get; set; }
        [PinMode(PinModes.Input)]
        public byte Action9Button { get; set; }
        [PinMode(PinModes.Input)]
        public byte Action10Button { get; set; }
        [PinMode(PinModes.Input)]
        public byte SASFreeButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte SASProgradeButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte SASRetrogadeButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte SASRadialInButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte SASRadialOutButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte SASNormalButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte SASAntiNormalButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte SASTargetButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte SASAntiTargetButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte KerbalUseButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte KerbalBoardButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte KerbalParachuteButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte KerbalJetPackButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte OrbitalViewButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte IncreaseTimeWarpButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte DecreaseTimeWarpsButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte NextVesselButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte PreviousVesselButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte PauseButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte QuickSaveButton { get; set; }
        [PinMode(PinModes.Input)]
        public byte QuickLoadButton { get; set; }
        [PinMode(PinModes.Output)]
        public byte LandingGearLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte BrakesLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte LightsLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte SASLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte RCSLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte PrecisionLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte SASFreeLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte SASProgradeLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte SASRetrogadeLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte SASRadialInLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte SASRadialOutLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte SASNormalLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte SASAntiNormalLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte SASTargetLed { get; set; }
        [PinMode(PinModes.Output)]
        public byte SASAntiTargetLed { get; set; }
    }
}
