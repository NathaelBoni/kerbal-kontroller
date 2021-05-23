using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources.Debounces;

namespace KerbalKontroller.Resources.Helpers
{
    public static class ControlHelper
    {
        public static void SetSASMode(IHardwareClient hardwareClient, IKSPClient kspClient)
        {
            var currentSASMode = kspClient.GetSASMode();
            var sasMode = hardwareClient.ReadSASModesButtons();

            if (sasMode == null || sasMode == currentSASMode)
                return;

            hardwareClient.WriteSASModeLed(currentSASMode, sasMode);
            kspClient.SetSASMode(sasMode.Value);
            return;
        }

        public static void SetToggleSwitches(IHardwareClient hardwareClient, IKSPClient kspClient)
        {
            var landingGearToggle = hardwareClient.ReadLandingGearSwitch();
            var brakesToggle = hardwareClient.ReadBrakesSwitch();
            var lightsToggle = hardwareClient.ReadLightsSwitch();
            var sasToggle = hardwareClient.ReadSASSwitch();
            var rcsToggle = hardwareClient.ReadRCSSwitch();

            kspClient.SetLandingGear(landingGearToggle);
            kspClient.SetBrakes(brakesToggle);
            kspClient.SetLights(lightsToggle);
            kspClient.SetSASActive(sasToggle);
            kspClient.SetRCSMode(rcsToggle);
        }

        public static void ActionGroup(VesselControlDebounce debounce, IKSPClient kspClient)
        {
            if (debounce.GetAction1ButtonState()) kspClient.ActivateAction(1);
            if (debounce.GetAction2ButtonState()) kspClient.ActivateAction(2);
            if (debounce.GetAction3ButtonState()) kspClient.ActivateAction(3);
            if (debounce.GetAction4ButtonState()) kspClient.ActivateAction(4);
            if (debounce.GetAction5ButtonState()) kspClient.ActivateAction(5);
            if (debounce.GetAction6ButtonState()) kspClient.ActivateAction(6);
            if (debounce.GetAction7ButtonState()) kspClient.ActivateAction(7);
            if (debounce.GetAction8ButtonState()) kspClient.ActivateAction(8);
            if (debounce.GetAction9ButtonState()) kspClient.ActivateAction(9);
            if (debounce.GetAction10ButtonState()) kspClient.ActivateAction(10);
        }
    }
}
