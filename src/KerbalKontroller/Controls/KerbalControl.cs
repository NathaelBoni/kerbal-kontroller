using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using Serilog.Core;
using System;

namespace KerbalKontroller.Controls
{
    public class KerbalControl : IControl
    {
        private readonly KRPCClient krpcClient;
        private readonly IHardwareClient hardwareClient;
        private readonly Logger logger;

        public KerbalControl(KRPCClient krpcClient, IHardwareClient hardwareClient, Logger logger)
        {
            this.krpcClient = krpcClient;
            this.hardwareClient = hardwareClient;
            this.logger = logger;
        }

        public ControlType ControlType => ControlType.Kerbal;

        public void ControlLoop()
        {
            throw new NotImplementedException();
        }
    }
}
