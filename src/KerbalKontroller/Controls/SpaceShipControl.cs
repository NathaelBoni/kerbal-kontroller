using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using Serilog.Core;

namespace KerbalKontroller.Controls
{
    public class SpaceShipControl : IControl
    {
        private readonly IHardwareClient hardwareClient;
        private readonly Logger logger;

        public SpaceShipControl(IHardwareClient hardwareClient, Logger logger)
        {
            this.hardwareClient = hardwareClient;
            this.logger = logger;
        }

        public ControlType ControlType => ControlType.SpaceShip;

        public void ControlLoop()
        {
            throw new System.NotImplementedException();
        }
    }
}
