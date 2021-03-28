using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using System;

namespace KerbalKontroller.Controls
{
    public class PlaneControl : IControl
    {
        private readonly IHardwareClient hardwareClient;

        public PlaneControl(IHardwareClient hardwareClient)
        {
            this.hardwareClient = hardwareClient;
        }

        public ControlType ControlType => ControlType.Plane;

        public void ControlLoop()
        {
            throw new NotImplementedException();
        }
    }
}
