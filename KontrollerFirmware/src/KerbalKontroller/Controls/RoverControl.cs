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

            if (debounce.GetAction1ButtonState()) kspClient.ActivateAction(1);
            if (debounce.GetAction2ButtonState()) kspClient.ActivateAction(2);
            if (debounce.GetAction3ButtonState()) kspClient.ActivateAction(3);
            if (debounce.GetAction4ButtonState()) kspClient.ActivateAction(4);
            if (debounce.GetAction5ButtonState()) kspClient.ActivateAction(5);
            if (debounce.GetAction6ButtonState()) kspClient.ActivateAction(6);
            if (debounce.GetAction7ButtonState()) kspClient.ActivateAction(7);
            if (debounce.GetAction8ButtonState()) kspClient.ActivateAction(8);
            if (debounce.GetAction9ButtonState()) kspClient.ActivateAction(9);
            if (debounce.GetAction10ButtonState()) kspClient.ActivateAction(10);

            if (debounce.GetStageButtonState()) kspClient.Stage();

            debounce.UpdateState();
        }
    }
}
