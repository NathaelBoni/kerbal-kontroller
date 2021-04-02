using KerbalKontroller.Resources;
using KRPC.Client.Services.SpaceCenter;

namespace KerbalKontroller.Interfaces
{
    public interface IControl
    {
        ControlType ControlType { get; }
        void ControlLoop();
    }
}
