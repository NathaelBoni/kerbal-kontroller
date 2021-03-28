using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using Serilog.Core;
using System;

namespace KerbalKontroller.Controls
{
    public class SpaceShipControl : IControl
    {
        private readonly KRPCClient krpcClient;
        private readonly IHardwareClient hardwareClient;
        private readonly Logger logger;

        public SpaceShipControl(KRPCClient krpcClient, IHardwareClient hardwareClient, Logger logger)
        {
            this.krpcClient = krpcClient;
            this.hardwareClient = hardwareClient;
            this.logger = logger;
        }

        public ControlType ControlType => ControlType.SpaceShip;

        public void ControlLoop()
        {
            throw new NotImplementedException();
        }
    }
}
