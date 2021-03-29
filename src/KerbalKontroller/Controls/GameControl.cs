using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using KerbalKontroller.Resources.Helpers;
using KRPC.Client.Services.SpaceCenter;
using Serilog.Core;
using System.Collections.Generic;
using System.Linq;

namespace KerbalKontroller.Controls
{
    public class GameControl
    {
        private readonly IEnumerable<IControl> controls;
        private readonly KRPCClient kRPCClient;
        private readonly IHardwareClient hardwareClient;
        private readonly KeyboardInputClient keyboardInputClient;
        private readonly Logger logger;

        public GameControl(IEnumerable<IControl> controls, KRPCClient kRPCClient, IHardwareClient hardwareClient, KeyboardInputClient keyboardInputClient, Logger logger)
        {
            this.controls = controls;
            this.kRPCClient = kRPCClient;
            this.hardwareClient = hardwareClient;
            this.keyboardInputClient = keyboardInputClient;
            this.logger = logger;
        }

        public void Start()
        {
            logger.Information("Controls added - starting KerbalKontroller");

            Vessel activeVessel;
            ControlType? activeControl = null;
            IControl control;

            while (true)
            {
                if (!kRPCClient.IsInFlight() || kRPCClient.IsGamePaused()) activeControl = null;
                else
                {
                    activeVessel = kRPCClient.GetActiveVessel();
                    activeControl = ActiveControlHelper.SelectControlType(activeVessel);
                }

                try
                {
                    control = controls.FirstOrDefault(_ => _.ControlType == activeControl);
                    if (control != null) control.ControlLoop();
                }
                catch (System.Exception ex)
                {
                    logger.Error(ex, $"Fatal error - {activeControl} control type failed");
                    throw;
                }

                if (kRPCClient.IsInFlight())
                {
                    if (hardwareClient.ReadIncreaseTimeWarpButton().Active) keyboardInputClient.IncreaseTimeWarp();
                    if (hardwareClient.ReadDecreaseTimeWarpButton().Active) keyboardInputClient.DecreaseTimeWarp();
                    if (hardwareClient.ReadNextVesselButton().Active) keyboardInputClient.NextVessel();
                    if (hardwareClient.ReadPreviousVesselButton().Active) keyboardInputClient.PreviousVessel();
                    if (hardwareClient.ReadOrbitalViewButton().Active) keyboardInputClient.SetOrbitalView();
                    if (hardwareClient.ReadPauseButton().Active) kRPCClient.PauseGame();
                    if (hardwareClient.ReadUnpauseButton().Active) kRPCClient.UnpauseGame();
                    if (hardwareClient.ReadQuickSaveButton().Active) kRPCClient.QuickSave();
                    if (hardwareClient.ReadQuickLoadButton().Active) kRPCClient.QuickLoad();
                }
            }
        }
    }
}
