using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using KerbalKontroller.Resources.Helpers;
using KRPC.Client.Services.SpaceCenter;
using Serilog;
using Serilog.Core;
using System.Collections.Generic;
using System.Linq;

namespace KerbalKontroller.Controls
{
    public class GameControl
    {
        private readonly IEnumerable<IControl> controls;
        private readonly KRPCClient kRPCClient;
        private readonly Logger logger;

        public GameControl(IEnumerable<IControl> controls, KRPCClient kRPCClient, Logger logger)
        {
            this.controls = controls;
            this.kRPCClient = kRPCClient;
            this.logger = logger;
        }

        public void Start()
        {
            Vessel activeVessel;
            ControlType activeControl = ControlType.None;

            while (true)
            {
                if (kRPCClient.IsGamePaused())
                    activeControl = ControlType.None;
                else
                {
                    activeVessel = kRPCClient.GetActiveVessel();
                    activeControl = ActiveControlHelper.SelectControlType(activeVessel);
                }

                try
                {
                    controls.First(_ => _.ControlType == activeControl).ControlLoop();
                }
                catch { }
            }
        }
    }
}
