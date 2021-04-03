using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using KerbalKontroller.Resources.Debounces;
using KerbalKontroller.Resources.Helpers;
using Serilog.Core;

namespace KerbalKontroller.Controls
{
    public class PlaneControl : IControl
    {
        private readonly KRPCClient kRPCClient;
        private readonly IHardwareClient hardwareClient;
        private readonly VesselControlDebounce debounce;
        private readonly Logger logger;

        public PlaneControl(KRPCClient krpcClient, IHardwareClient hardwareClient, Logger logger)
        {
            this.kRPCClient = krpcClient;
            this.hardwareClient = hardwareClient;
            debounce = new VesselControlDebounce(hardwareClient);
            this.logger = logger;
        }

        public ControlType ControlType => ControlType.Plane;

        public void ControlLoop()
        {
            var leftJoystick = hardwareClient.ReadLeftJoystick();
            var extraLeftJoystick = hardwareClient.ReadExtraLeftJoystick();
            var rightJoystick = hardwareClient.ReadRightJoystick();
            var extraRightJoystick = hardwareClient.ReadExtraRightJoystick();
            var throttleAxis = hardwareClient.ReadAnalogThrottle();

            kRPCClient.SetPlaneRotation(leftJoystick, extraLeftJoystick);
            kRPCClient.SetPlaneTranslation(rightJoystick, extraRightJoystick);
            kRPCClient.SetThrottle(throttleAxis);

            ControlHelper.SetSASMode(hardwareClient, kRPCClient).Invoke();
            ControlHelper.SetToggleSwitches(hardwareClient, kRPCClient);
            ControlHelper.ActionGroup(debounce, kRPCClient);

            kRPCClient.SetBrakes(hardwareClient.ReadBrakesButton());

            debounce.UpdateState();
        }
    }
}
