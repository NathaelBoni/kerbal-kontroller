using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using KerbalKontroller.Resources.Debounces;
using KerbalKontroller.Resources.Helpers;
using Serilog.Core;
using System;

namespace KerbalKontroller.Controls
{
    public class KerbalControl : IControl
    {
        private readonly KRPCClient kRPCClient;
        private readonly IHardwareClient hardwareClient;
        private readonly KeyboardInputClient keyboardInputClient;
        private readonly KerbalControlDebounce debounce;
        private readonly Logger logger;

        public KerbalControl(KRPCClient krpcClient, IHardwareClient hardwareClient, KeyboardInputClient keyboardInputClient, Logger logger)
        {
            this.kRPCClient = krpcClient;
            this.hardwareClient = hardwareClient;
            this.keyboardInputClient = keyboardInputClient;
            debounce = new KerbalControlDebounce(hardwareClient);
            this.logger = logger;
        }

        public ControlType ControlType => ControlType.Kerbal;

        public void ControlLoop()
        {
            var leftJoystick = hardwareClient.ReadLeftJoystick(isAbsolute: true);
            var extraLeftJoystick = hardwareClient.ReadExtraLeftJoystick(isAbsolute: true);

            if (leftJoystick.YValue > 0) keyboardInputClient.KerbalWalkForward();
            else if (leftJoystick.YValue < 0) keyboardInputClient.KerbalWalkBackward();
            else keyboardInputClient.KerbalStopLateralMovement();

            if (leftJoystick.XValue > 0) keyboardInputClient.KerbalWalkRight();
            else if (leftJoystick.XValue < 0) keyboardInputClient.KerbalWalkLeft();
            else keyboardInputClient.KerbalStopForwardMovement();

            if (extraLeftJoystick.YValue > 0) keyboardInputClient.KerbalJetPackUp();
            else if (extraLeftJoystick.YValue < 0) keyboardInputClient.KerbalJetPackDown();
            else keyboardInputClient.KerbalStopVerticalMovement();

            ControlHelper.SetToggleSwitches(hardwareClient, kRPCClient);

            if (debounce.GetKerbalUseButtonState()) keyboardInputClient.KerbalUse();
            if (debounce.GetKerbalJumpButtonState()) keyboardInputClient.KerbalJump();
            if (debounce.GetKerbalBoardButtonState()) keyboardInputClient.KerbalBoard();
            if (debounce.GetKerbalParachuteButtonState()) keyboardInputClient.KerbalParachuteDeploy();
            if (debounce.GetKerbalJetPackButtonState()) keyboardInputClient.KerbalJetpackToggle();
            if (debounce.GetKerbalConstructionButtonState()) keyboardInputClient.KerbalConstructionModeToggle();

            debounce.UpdateState();
        }
    }
}
