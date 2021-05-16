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
        private readonly IKSPClient kspClient;
        private readonly IHardwareClient hardwareClient;
        private readonly VesselControlDebounce debounce;
        private readonly Logger logger;

        public PlaneControl(IKSPClient kspClient, IHardwareClient hardwareClient, Logger logger)
        {
            this.kspClient = kspClient;
            this.hardwareClient = hardwareClient;
            debounce = new VesselControlDebounce(hardwareClient);
            this.logger = logger;
        }

        public VesselTypes ControlType => VesselTypes.Plane;

        public void ControlLoop()
        {
            var leftJoystick = hardwareClient.ReadLeftJoystick();
            var extraLeftJoystick = hardwareClient.ReadExtraLeftJoystick();
            var rightJoystick = hardwareClient.ReadRightJoystick();
            var extraRightJoystick = hardwareClient.ReadExtraRightJoystick();
            var throttleAxis = hardwareClient.ReadAnalogThrottle();

            kspClient.SetPlaneRotation(leftJoystick, extraLeftJoystick);
            kspClient.SetPlaneTranslation(rightJoystick, extraRightJoystick);
            kspClient.SetThrottle(throttleAxis);

            ControlHelper.SetSASMode(hardwareClient, kspClient).Invoke();
            ControlHelper.SetToggleSwitches(hardwareClient, kspClient);
            ControlHelper.ActionGroup(debounce, kspClient);

            kspClient.SetBrakes(hardwareClient.ReadBrakesButton());

            debounce.UpdateState();
        }
    }
}
