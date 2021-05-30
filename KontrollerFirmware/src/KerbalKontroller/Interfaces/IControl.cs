using KerbalKontroller.Resources;

namespace KerbalKontroller.Interfaces
{
    public interface IControl
    {
        VesselTypes ControlType { get; }
        void ControlLoop();
    }
}
