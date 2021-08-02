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

            kspClient.SetSASMode(sasMode.Value);
            hardwareClient.WriteSASModeLed(sasMode.Value);
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
    }
}
