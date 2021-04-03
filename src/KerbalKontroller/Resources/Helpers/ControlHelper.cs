using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using System;

namespace KerbalKontroller.Resources.Helpers
{
    public static class ControlHelper
    {
        public static Action SetSASMode(IHardwareClient hardwareClient, KRPCClient kRPCClient)
        {
            if (hardwareClient.ReadSASFreeButton().Active)
                return kRPCClient.SetSASModeFree;
            if (hardwareClient.ReadSASManeuverButton().Active)
                return kRPCClient.SetSASModeManeuver;
            if (hardwareClient.ReadSASProgradeButton().Active)
                return kRPCClient.SetSASModePrograde;
            if (hardwareClient.ReadSASRetrogadeButton().Active)
                return kRPCClient.SetSASModeRetrograde;
            if (hardwareClient.ReadSASRadialInButton().Active)
                return kRPCClient.SetSASModeRadialIn;
            if (hardwareClient.ReadSASRadialOutButton().Active)
                return kRPCClient.SetSASModeRadialOut;
            if (hardwareClient.ReadSASNormalButton().Active)
                return kRPCClient.SetSASModeNormal;
            if (hardwareClient.ReadSASAntiNormalButton().Active)
                return kRPCClient.SetSASModeAntiNormal;
            if (hardwareClient.ReadSASTargetButton().Active)
                return kRPCClient.SetSASModeTarget;
            if (hardwareClient.ReadSASAntiTargetButton().Active)
                return kRPCClient.SetSASModeAntiTarget;
            return null;
        }

        public static void SetToggleSwitches(IHardwareClient hardwareClient, KRPCClient kRPCClient)
        {
            var landingGearToggle = hardwareClient.ReadLandingGearSwitch();
            var brakesToggle = hardwareClient.ReadBrakesSwitch();
            var lightsToggle = hardwareClient.ReadLightsSwitch();
            var sasToggle = hardwareClient.ReadSASSwitch();
            var rcsToggle = hardwareClient.ReadRCSSwitch();

            kRPCClient.SetLandingGear(landingGearToggle);
            kRPCClient.SetBrakes(brakesToggle);
            kRPCClient.SetLights(lightsToggle);
            kRPCClient.SetSASMode(sasToggle);
            kRPCClient.SetRCSMode(rcsToggle);
        }
    }
}
