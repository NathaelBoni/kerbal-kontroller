using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using System;

namespace KerbalKontroller.Controls
{
    public class PlaneControl : IControl
    {
        private readonly KRPCClient krpcClient;
        private readonly IHardwareClient hardwareClient;

        public PlaneControl(KRPCClient krpcClient, IHardwareClient hardwareClient)
        {
            this.krpcClient = krpcClient;
            this.hardwareClient = hardwareClient;
        }

        public ControlType ControlType => ControlType.Plane;

        public void ControlLoop()
        {
            throw new NotImplementedException();
        }
    }
}
