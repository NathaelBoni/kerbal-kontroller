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
