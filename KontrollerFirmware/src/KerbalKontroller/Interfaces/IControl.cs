using KerbalKontroller.Resources;
using KRPC.Client.Services.SpaceCenter;

namespace KerbalKontroller.Interfaces
{
    public interface IControl
    {
        VesselTypes ControlType { get; }
        void ControlLoop();
    }
}
