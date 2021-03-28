using KerbalKontroller.Resources;

namespace KerbalKontroller.Interfaces
{
    public interface IControl
    {
        ControlType ControlType { get; }
        void ControlLoop();
    }
}
