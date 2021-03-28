using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using Serilog.Core;
using System;

namespace KerbalKontroller.Controls
{
    public class RoverControl : IControl
    {
        private readonly KRPCClient krpcClient;
        private readonly IHardwareClient hardwareClient;
        private readonly Logger logger;

        public RoverControl(KRPCClient krpcClient, IHardwareClient hardwareClient, Logger logger)
        {
            this.krpcClient = krpcClient;
            this.hardwareClient = hardwareClient;
            this.logger = logger;
        }

        public ControlType ControlType => ControlType.Rover;

        public void ControlLoop()
        {
            throw new NotImplementedException();
        }
    }
}
