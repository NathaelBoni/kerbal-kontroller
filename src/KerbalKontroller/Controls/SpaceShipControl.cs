using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using KRPC.Client.Services.SpaceCenter;
using Serilog.Core;
using System;

namespace KerbalKontroller.Controls
{
    public class SpaceShipControl : IControl
    {
        private readonly KRPCClient kRPCClient;
        private readonly IHardwareClient hardwareClient;
        private readonly Logger logger;

        public SpaceShipControl(KRPCClient krpcClient, IHardwareClient hardwareClient, Logger logger)
        {
            this.kRPCClient = krpcClient;
            this.hardwareClient = hardwareClient;
            this.logger = logger;
        }

        public ControlType ControlType => ControlType.SpaceShip;

        public void ControlLoop()
        {
            var leftJoystick = hardwareClient.ReadLeftJoystick();
            var extraLeftJoystick = hardwareClient.ReadExtraLeftJoystick();
            var rightJoystick = hardwareClient.ReadRightJoystick();
            var extraRightJoystick = hardwareClient.ReadExtraRightJoystick();
            var throttleAxis = hardwareClient.ReadAnalogThrottle();

            var landingGearToggle = hardwareClient.ReadLandingGearSwitch();
            var brakesToggle = hardwareClient.ReadBrakesSwitch();
            var lightsToggle = hardwareClient.ReadLightsSwitch();
            var sasToggle = hardwareClient.ReadSASSwitch();
            var rcsToggle = hardwareClient.ReadRCSSwitch();

            kRPCClient.SetVesselRotation(leftJoystick, extraLeftJoystick);
            kRPCClient.SetVesselTranslation(rightJoystick, extraRightJoystick);
            kRPCClient.SetThrottle(throttleAxis);

            SetSASMode().Invoke();

            kRPCClient.SetLandingGear(landingGearToggle);
            kRPCClient.SetBrakes(brakesToggle);
            kRPCClient.SetLights(lightsToggle);
            kRPCClient.SetSASMode(sasToggle);
            kRPCClient.SetRCSMode(rcsToggle);
        }

        private Action SetSASMode()
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
    }
}
