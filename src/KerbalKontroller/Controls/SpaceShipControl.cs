using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using KerbalKontroller.Resources.Debounces;
using KerbalKontroller.Resources.Helpers;
using Serilog.Core;

namespace KerbalKontroller.Controls
{
    public class SpaceShipControl : IControl
    {
        private readonly KRPCClient kRPCClient;
        private readonly IHardwareClient hardwareClient;
        private VesselControlDebounce debounce;
        private readonly Logger logger;

        public SpaceShipControl(KRPCClient krpcClient, IHardwareClient hardwareClient, Logger logger)
        {
            this.kRPCClient = krpcClient;
            this.hardwareClient = hardwareClient;
            debounce = new VesselControlDebounce(hardwareClient);
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

            kRPCClient.SetVesselRotation(leftJoystick, extraLeftJoystick);
            kRPCClient.SetVesselTranslation(rightJoystick, extraRightJoystick);
            kRPCClient.SetThrottle(throttleAxis);

            ControlHelper.SetSASMode(hardwareClient, kRPCClient).Invoke();
            ControlHelper.SetToggleSwitches(hardwareClient, kRPCClient);
            ControlHelper.ActionGroup(debounce, kRPCClient);

            if (debounce.GetAbortButtonState()) kRPCClient.Abort();
            if (debounce.GetStageButtonState()) kRPCClient.Stage();

            debounce.UpdateState();
        }
    }
}
