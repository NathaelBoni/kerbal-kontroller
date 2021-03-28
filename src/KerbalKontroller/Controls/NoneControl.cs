using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using Serilog.Core;
using System;

namespace KerbalKontroller.Controls
{
    public class NoneControl : IControl
    {
        private readonly IHardwareClient hardwareClient;
        private readonly Logger logger;

        public NoneControl(IHardwareClient hardwareClient, Logger logger)
        {
            this.hardwareClient = hardwareClient;
            this.logger = logger;
        }

        public ControlType ControlType => ControlType.None;

        public void ControlLoop()
        {
            throw new NotImplementedException();
        }
    }
}
