using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using KerbalKontroller.Resources.Debounces;
using Serilog.Core;

namespace KerbalKontroller.Controls
{
    public class KerbalControl : IControl
    {
        private readonly IHardwareClient hardwareClient;
        private readonly KeyboardInputClient keyboardInputClient;
        private readonly KerbalControlDebounce debounce;
        private readonly Logger logger;

        public KerbalControl(IHardwareClient hardwareClient, KeyboardInputClient keyboardInputClient, Logger logger)
        {
            this.hardwareClient = hardwareClient;
            this.keyboardInputClient = keyboardInputClient;
            debounce = new KerbalControlDebounce(hardwareClient);
            this.logger = logger;
        }

        public VesselTypes ControlType => VesselTypes.Kerbal;

        public void ControlLoop()
        {
            var leftJoystick = hardwareClient.ReadLeftJoystick();
            var extraLeftJoystick = hardwareClient.ReadExtraLeftJoystick();

            if (leftJoystick.YValue > 0) keyboardInputClient.KerbalWalkForward();
            else if (leftJoystick.YValue < 0) keyboardInputClient.KerbalWalkBackward();
            else keyboardInputClient.KerbalStopForwardMovement();

            if (leftJoystick.XValue > 0) keyboardInputClient.KerbalWalkRight();
            else if (leftJoystick.XValue < 0) keyboardInputClient.KerbalWalkLeft();
            else keyboardInputClient.KerbalStopLateralMovement();

            if (extraLeftJoystick.YValue > 0) keyboardInputClient.KerbalJetPackUp();
            else if (extraLeftJoystick.YValue < 0) keyboardInputClient.KerbalJetPackDown();
            else keyboardInputClient.KerbalStopVerticalMovement();

            if (debounce.GetKerbalUseButtonState()) keyboardInputClient.KerbalUse();
            if (debounce.GetKerbalJumpButtonState()) keyboardInputClient.KerbalJump();
            if (debounce.GetKerbalRunButtonState()) keyboardInputClient.KerbalRun();
            if (debounce.GetKerbalBoardButtonState()) keyboardInputClient.KerbalBoard();
            if (debounce.GetKerbalLetGoButtonState()) keyboardInputClient.KerbalLetGo();
            if (debounce.GetKerbalParachuteButtonState()) keyboardInputClient.KerbalParachuteDeploy();
            if (debounce.GetKerbalJetPackButtonState()) keyboardInputClient.KerbalJetpackToggle();
            if (debounce.GetKerbalConstructionButtonState()) keyboardInputClient.KerbalConstructionModeToggle();

            debounce.UpdateState();
        }
    }
}
