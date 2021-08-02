using WindowsInput;
using WindowsInput.Native;

namespace KerbalKontroller.Clients
{
    public class KeyboardInputClient
    {
        private readonly InputSimulator inputSimulator;

        public KeyboardInputClient()
        {
            inputSimulator = new InputSimulator();
        }

        public void KerbalWalkForward()
        {
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_W)) return;
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_S))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_S);
            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_W);
        }

        public void KerbalWalkBackward()
        {
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_S)) return;
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_W))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_W);
            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_S);
        }

        public void KerbalWalkLeft()
        {
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_A)) return;
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_D))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_D);
            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_A);
        }

        public void KerbalWalkRight()
        {
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_D)) return;
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_A))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_A);
            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.VK_D);
        }

        public void KerbalStopLateralMovement()
        {
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_A))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_A);
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_D))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_D);
        }

        public void KerbalStopForwardMovement()
        {
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_W))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_W);
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_S))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_S);
        }

        public void KerbalJetPackUp()
        {
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.LSHIFT)) return;
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.LCONTROL))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LSHIFT);
        }

        public void KerbalJetPackDown()
        {
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.LCONTROL)) return;
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.LSHIFT))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LSHIFT);
            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
        }

        public void KerbalStopVerticalMovement()
        {
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_W))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_W);
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_S))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.VK_S);
        }

        public void KerbalRun()
        {
            if (inputSimulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.LSHIFT))
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LSHIFT);
            else if (inputSimulator.InputDeviceState.IsHardwareKeyUp(VirtualKeyCode.LSHIFT))
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LSHIFT);
        }

        public void KerbalUse()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_F);
        }

        public void KerbalJump()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        }

        public void KerbalBoard()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_B);
        }

        public void KerbalLetGo()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        }

        public void KerbalParachuteDeploy()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_P);
        }

        public void KerbalJetpackToggle()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_R);
        }

        public void KerbalConstructionModeToggle()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_I);
        }

        public void IncreaseTimeWarp()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.OEM_PERIOD);
        }

        public void DecreaseTimeWarp()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.OEM_COMMA);
        }

        public void NextVessel()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.OEM_PLUS);
        }

        public void PreviousVessel()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.OEM_1);
        }

        public void CameraCycle()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
        }

        public void SetOrbitalView()
        {
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_M);
        }
    }
}
