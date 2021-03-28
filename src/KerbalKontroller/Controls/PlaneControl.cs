using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using Serilog.Core;
using System;

namespace KerbalKontroller.Controls
{
    public class PlaneControl : IControl
    {
        private readonly KRPCClient krpcClient;
        private readonly IHardwareClient hardwareClient;
        private readonly Logger logger;

        public PlaneControl(KRPCClient krpcClient, IHardwareClient hardwareClient, Logger logger)
        {
            this.krpcClient = krpcClient;
            this.hardwareClient = hardwareClient;
            this.logger = logger;
        }

        public ControlType ControlType => ControlType.Plane;

        public void ControlLoop()
        {
            throw new NotImplementedException();
        }
    }
}
