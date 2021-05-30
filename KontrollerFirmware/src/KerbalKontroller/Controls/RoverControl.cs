using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using KerbalKontroller.Resources.Debounces;
using KerbalKontroller.Resources.Helpers;
using Serilog.Core;

namespace KerbalKontroller.Controls
{
    public class RoverControl : IControl
    {
        private readonly IKSPClient kspClient;
        private readonly IHardwareClient hardwareClient;
        private readonly VesselControlDebounce debounce;
        private readonly Logger logger;

        public RoverControl(IKSPClient kspClient, IHardwareClient hardwareClient, Logger logger)
        {
            this.kspClient = kspClient;
            this.hardwareClient = hardwareClient;
            debounce = new VesselControlDebounce(hardwareClient);
            this.logger = logger;
        }

        public VesselTypes ControlType => VesselTypes.Rover;

        public void ControlLoop()
        {
            var leftJoystick = hardwareClient.ReadLeftJoystick();

            kspClient.SetRoverMovement(leftJoystick);
            kspClient.SetBrakes(hardwareClient.ReadBrakesButton());

            ControlHelper.SetSASMode(hardwareClient, kspClient);
            ControlHelper.SetToggleSwitches(hardwareClient, kspClient);
            ControlHelper.ActionGroup(debounce, kspClient);

            if (debounce.GetStageButtonState()) kspClient.Stage();

            debounce.UpdateState();
        }
    }
}
