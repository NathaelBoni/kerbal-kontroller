using KerbalKontroller.Resources;

namespace KerbalKontroller.Interfaces
{
    public interface IHardwareClient
    {
        JoystickAxis ReadLeftJoystick();
        JoystickAxis ReadRightJoystick();
    }
}
