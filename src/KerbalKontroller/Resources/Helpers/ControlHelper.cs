using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources.Debounces;
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
            return () => { };
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

        public static void ActionGroup(VesselControlDebounce debounce, KRPCClient kRPCClient)
        {
            if (debounce.GetAction1ButtonState()) kRPCClient.ActivateAction(1);
            if (debounce.GetAction2ButtonState()) kRPCClient.ActivateAction(2);
            if (debounce.GetAction3ButtonState()) kRPCClient.ActivateAction(3);
            if (debounce.GetAction4ButtonState()) kRPCClient.ActivateAction(4);
            if (debounce.GetAction5ButtonState()) kRPCClient.ActivateAction(5);
            if (debounce.GetAction6ButtonState()) kRPCClient.ActivateAction(6);
            if (debounce.GetAction7ButtonState()) kRPCClient.ActivateAction(7);
            if (debounce.GetAction8ButtonState()) kRPCClient.ActivateAction(8);
            if (debounce.GetAction9ButtonState()) kRPCClient.ActivateAction(9);
            if (debounce.GetAction10ButtonState()) kRPCClient.ActivateAction(10);
        }
    }
}
