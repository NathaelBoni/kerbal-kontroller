using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources.Factories;
using KRPC.Client.Services.SpaceCenter;
using Serilog.Core;
using System;

namespace KerbalKontroller.Controls
{
    public class GameControl
    {
        private readonly KRPCClient kRPCClient;
        private readonly IHardwareClient hardwareClient;
        private readonly KeyboardInputClient keyboardInputClient;
        private readonly ControlFactory controlFactory;
        private readonly Logger logger;

        public GameControl(KRPCClient kRPCClient, IHardwareClient hardwareClient, KeyboardInputClient keyboardInputClient, ControlFactory controlFactory, Logger logger)
        {
            this.kRPCClient = kRPCClient;
            this.hardwareClient = hardwareClient;
            this.keyboardInputClient = keyboardInputClient;
            this.controlFactory = controlFactory;
            this.logger = logger;
        }

        public void Start()
        {
            logger.Information("Controls added - starting KerbalKontroller");

            Vessel activeVessel = null;
            Action controlAction;

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
                if (kRPCClient.IsInFlight() && !kRPCClient.IsGamePaused())
                {
                    activeVessel = kRPCClient.GetActiveVessel();

                    try
                    {
                        controlAction = controlFactory.GetControlAction(activeVessel);
                        controlAction.Invoke();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, $"Fatal error - control action threw an exception");
                        throw;
                    }
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
