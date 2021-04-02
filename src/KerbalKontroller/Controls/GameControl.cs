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

            Vessel activeVessel = null;
            ControlType? activeControl = null;
            IControl control;

            bool currentIncreaseTimeWarpButtonState, lastIncreaseTimeWarpButtonState = false;
            bool currentDecreaseTimeWarpButtonState, lastDecreaseTimeWarpButtonState = false;
            bool currentNextVesselButtonState, lastNextVesselButtonState = false;
            bool currentPreviousVesselButtonState, lastPreviousVesselButtonState = false;
            bool currentCameraCycleButtonState, lastCameraCycleButtonState = false;
            bool currentOrbitalViewButtonState, lastOrbitalViewButtonState = false;
            bool currentPauseButtonState, lastPauseButtonState = false;
            bool currentQuickSaveButtonState, lastQuickSaveButtonState = false;
            bool currentQuickLoadButtonState, lastQuickLoadButtonState = false;

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
                    currentIncreaseTimeWarpButtonState = hardwareClient.ReadIncreaseTimeWarpButton().Active;
                    currentDecreaseTimeWarpButtonState = hardwareClient.ReadDecreaseTimeWarpButton().Active;
                    currentNextVesselButtonState = hardwareClient.ReadNextVesselButton().Active;
                    currentPreviousVesselButtonState = hardwareClient.ReadPreviousVesselButton().Active;
                    currentCameraCycleButtonState = hardwareClient.ReadCameraCycleButton().Active;
                    currentOrbitalViewButtonState = hardwareClient.ReadOrbitalViewButton().Active;
                    currentPauseButtonState = hardwareClient.ReadPauseButton().Active;
                    currentQuickSaveButtonState = hardwareClient.ReadQuickSaveButton().Active;
                    currentQuickLoadButtonState = hardwareClient.ReadQuickLoadButton().Active;

                    if (currentIncreaseTimeWarpButtonState && !lastIncreaseTimeWarpButtonState) keyboardInputClient.IncreaseTimeWarp();
                    if (currentDecreaseTimeWarpButtonState && !lastDecreaseTimeWarpButtonState) keyboardInputClient.DecreaseTimeWarp();
                    if (currentNextVesselButtonState && !lastNextVesselButtonState) keyboardInputClient.NextVessel();
                    if (currentPreviousVesselButtonState && !lastPreviousVesselButtonState) keyboardInputClient.PreviousVessel();
                    if (currentCameraCycleButtonState && !lastCameraCycleButtonState) keyboardInputClient.CameraCycle();
                    if (currentOrbitalViewButtonState && !lastOrbitalViewButtonState) keyboardInputClient.SetOrbitalView();
                    if (currentPauseButtonState && !lastPauseButtonState) kRPCClient.SetPaused();
                    if (currentQuickSaveButtonState && !lastQuickSaveButtonState) kRPCClient.QuickSave();
                    if (currentQuickLoadButtonState && !lastQuickLoadButtonState) kRPCClient.QuickLoad();

                    lastIncreaseTimeWarpButtonState = currentIncreaseTimeWarpButtonState;
                    lastDecreaseTimeWarpButtonState = currentDecreaseTimeWarpButtonState;
                    lastNextVesselButtonState = currentNextVesselButtonState;
                    lastPreviousVesselButtonState = currentPreviousVesselButtonState;
                    lastCameraCycleButtonState = currentCameraCycleButtonState;
                    lastOrbitalViewButtonState = currentOrbitalViewButtonState;
                    lastPauseButtonState = currentPauseButtonState;
                    lastQuickSaveButtonState = currentQuickSaveButtonState;
                    lastQuickLoadButtonState = currentQuickLoadButtonState;
                }
            }
        }
    }
}
