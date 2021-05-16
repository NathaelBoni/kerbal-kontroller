using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using KerbalKontroller.Resources.Debounces;
using KerbalKontroller.Resources.Helpers;
using Serilog.Core;

namespace KerbalKontroller.Controls
{
    public class RoverControl : IControl
    {
        private readonly KRPCClient kRPCClient;
        private readonly IHardwareClient hardwareClient;
        private readonly VesselControlDebounce debounce;
        private readonly Logger logger;

        public RoverControl(KRPCClient krpcClient, IHardwareClient hardwareClient, Logger logger)
        {
            this.kRPCClient = krpcClient;
            this.hardwareClient = hardwareClient;
            debounce = new VesselControlDebounce(hardwareClient);
            this.logger = logger;
        }

        public VesselTypes ControlType => VesselTypes.Rover;

        public void ControlLoop()
        {
            var leftJoystick = hardwareClient.ReadLeftJoystick();

            kRPCClient.SetRoverMovement(leftJoystick);
            kRPCClient.SetBrakes(hardwareClient.ReadBrakesButton());

            ControlHelper.SetSASMode(hardwareClient, kRPCClient);
            ControlHelper.SetToggleSwitches(hardwareClient, kRPCClient);
            ControlHelper.ActionGroup(debounce, kRPCClient);

            if (debounce.GetStageButtonState()) kRPCClient.Stage();

            debounce.UpdateState();
        }
    }
}
