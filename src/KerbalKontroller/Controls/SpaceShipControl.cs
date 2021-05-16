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
        private readonly IKSPClient kspClient;
        private readonly IHardwareClient hardwareClient;
        private VesselControlDebounce debounce;
        private readonly Logger logger;

        public SpaceShipControl(IKSPClient kspClient, IHardwareClient hardwareClient, Logger logger)
        {
            this.kspClient = kspClient;
            this.hardwareClient = hardwareClient;
            debounce = new VesselControlDebounce(hardwareClient);
            this.logger = logger;
        }

        public VesselTypes ControlType => VesselTypes.SpaceShip;

        public void ControlLoop()
        {
            var leftJoystick = hardwareClient.ReadLeftJoystick(yAxisInverted: true);
            var extraLeftJoystick = hardwareClient.ReadExtraLeftJoystick();

            var rightJoystick = hardwareClient.ReadRightJoystick();
            var extraRightJoystick = hardwareClient.ReadExtraRightJoystick();
            var throttleAxis = hardwareClient.ReadAnalogThrottle();

            kspClient.SetVesselRotation(leftJoystick, extraLeftJoystick);
            kspClient.SetVesselTranslation(rightJoystick, extraRightJoystick);
            kspClient.SetThrottle(throttleAxis);

            ControlHelper.SetSASMode(hardwareClient, kspClient).Invoke();
            ControlHelper.SetToggleSwitches(hardwareClient, kspClient);
            ControlHelper.ActionGroup(debounce, kspClient);

            if (debounce.GetAbortButtonState()) kspClient.Abort();
            if (debounce.GetStageButtonState()) kspClient.Stage();

            debounce.UpdateState();
        }
    }
}
