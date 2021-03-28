using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using Serilog.Core;
using System;

namespace KerbalKontroller.Controls
{
    public class RoverControl : IControl
    {
        private readonly IHardwareClient hardwareClient;
        private readonly Logger logger;

        public RoverControl(IHardwareClient hardwareClient, Logger logger)
        {
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
