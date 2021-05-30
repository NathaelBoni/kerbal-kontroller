using System.Collections.Generic;
using System.Linq;

namespace KerbalKontroller.Resources
{
    public class ControllerFunctions
    {
        private int index;
        private IEnumerable<int> analogInputs;
        private IEnumerable<bool> digitalInputs;

        public void DeserializeData(string data)
        {
            analogInputs = data.Split('|')[0].Split(',').Select(_ => int.Parse(_));
            digitalInputs = data.Split('|')[1].Split(',').Select(_ => _.Equals("1"));

            index = 0;
            LeftJoystickX = ReadNextAnalog();
            LeftJoystickY = ReadNextAnalog();
            RightJoystickX = ReadNextAnalog();
            RightJoystickY = ReadNextAnalog();
            ExtraLeftJoystickX = ReadNextAnalog();
            ExtraLeftJoystickY = ReadNextAnalog();
            ExtraRightJoystickX = ReadNextAnalog();
            ExtraRightJoystickY = ReadNextAnalog();
            AnalogThrottle = ReadNextAnalog();

            index = 0;
            LandingGearSwitch = ReadNextDigital();
            BrakesSwitch = ReadNextDigital();
            LightsSwitch = ReadNextDigital();
            SASSwitch = ReadNextDigital();
            RCSSwitch = ReadNextDigital();
            PrecisionSwitch = ReadNextDigital();
            StageButton = ReadNextDigital();
            AbortButton = ReadNextDigital();
            BrakesButton = ReadNextDigital();
            PrecisionButton = ReadNextDigital();
            Action1Button = ReadNextDigital();
            Action2Button = ReadNextDigital();
            Action3Button = ReadNextDigital();
            Action4Button = ReadNextDigital();
            Action5Button = ReadNextDigital();
            Action6Button = ReadNextDigital();
            Action7Button = ReadNextDigital();
            Action8Button = ReadNextDigital();
            Action9Button = ReadNextDigital();
            Action10Button = ReadNextDigital();
            SASFreeButton = ReadNextDigital();
            SASManeuverButton = ReadNextDigital();
            SASProgradeButton = ReadNextDigital();
            SASRetrogradeButton = ReadNextDigital();
            SASRadialInButton = ReadNextDigital();
            SASRadialOutButton = ReadNextDigital();
            SASNormalButton = ReadNextDigital();
            SASAntiNormalButton = ReadNextDigital();
            SASTargetButton = ReadNextDigital();
            SASAntiTargetButton = ReadNextDigital();
            KerbalUseButton = ReadNextDigital();
            KerbalJumpButton = ReadNextDigital();
            KerbalRunButton = ReadNextDigital();
            KerbalBoardButton = ReadNextDigital();
            KerbalLetGoButton = ReadNextDigital();
            KerbalParachuteButton = ReadNextDigital();
            KerbalJetPackButton = ReadNextDigital();
            KerbalConstructionButton = ReadNextDigital();
            CameraCycleButton = ReadNextDigital();
            OrbitalViewButton = ReadNextDigital();
            IncreaseTimeWarpButton = ReadNextDigital();
            DecreaseTimeWarpButton = ReadNextDigital();
            NextVesselButton = ReadNextDigital();
            PreviousVesselButton = ReadNextDigital();
            PauseButton = ReadNextDigital();
            QuickSaveButton = ReadNextDigital();
            QuickLoadButton = ReadNextDigital();
        }

        private int ReadNextAnalog()
        {
            return analogInputs.ElementAt(index++);
        }

        private bool ReadNextDigital()
        {
            return digitalInputs.ElementAt(index++);
        }

        public int LeftJoystickX { get; private set; }
        public int LeftJoystickY { get; private set; }
        public int RightJoystickX { get; private set; }
        public int RightJoystickY { get; private set; }
        public int ExtraLeftJoystickX { get; private set; }
        public int ExtraLeftJoystickY { get; private set; }
        public int ExtraRightJoystickX { get; private set; }
        public int ExtraRightJoystickY { get; private set; }
        public int AnalogThrottle { get; private set; }
        public bool LandingGearSwitch { get; private set; }
        public bool BrakesSwitch { get; private set; }
        public bool LightsSwitch { get; private set; }
        public bool SASSwitch { get; private set; }
        public bool RCSSwitch { get; private set; }
        public bool PrecisionSwitch { get; private set; }
        public bool StageButton { get; private set; }
        public bool AbortButton { get; private set; }
        public bool BrakesButton { get; private set; }
        public bool PrecisionButton { get; private set; }
        public bool Action1Button { get; private set; }
        public bool Action2Button { get; private set; }
        public bool Action3Button { get; private set; }
        public bool Action4Button { get; private set; }
        public bool Action5Button { get; private set; }
        public bool Action6Button { get; private set; }
        public bool Action7Button { get; private set; }
        public bool Action8Button { get; private set; }
        public bool Action9Button { get; private set; }
        public bool Action10Button { get; private set; }
        public bool SASFreeButton { get; private set; }
        public bool SASManeuverButton { get; private set; }
        public bool SASProgradeButton { get; private set; }
        public bool SASRetrogradeButton { get; private set; }
        public bool SASRadialInButton { get; private set; }
        public bool SASRadialOutButton { get; private set; }
        public bool SASNormalButton { get; private set; }
        public bool SASAntiNormalButton { get; private set; }
        public bool SASTargetButton { get; private set; }
        public bool SASAntiTargetButton { get; private set; }
        public bool KerbalUseButton { get; private set; }
        public bool KerbalJumpButton { get; private set; }
        public bool KerbalRunButton { get; private set; }
        public bool KerbalBoardButton { get; private set; }
        public bool KerbalLetGoButton { get; private set; }
        public bool KerbalParachuteButton { get; private set; }
        public bool KerbalJetPackButton { get; private set; }
        public bool KerbalConstructionButton { get; private set; }
        public bool CameraCycleButton { get; private set; }
        public bool OrbitalViewButton { get; private set; }
        public bool IncreaseTimeWarpButton { get; private set; }
        public bool DecreaseTimeWarpButton { get; private set; }
        public bool NextVesselButton { get; private set; }
        public bool PreviousVesselButton { get; private set; }
        public bool PauseButton { get; private set; }
        public bool QuickSaveButton { get; private set; }
        public bool QuickLoadButton { get; private set; }
    }
}
