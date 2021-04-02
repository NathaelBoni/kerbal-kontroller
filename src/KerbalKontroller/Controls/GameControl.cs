using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources.Debounces;
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
        private readonly GameControlDebounce debounce;

        public GameControl(KRPCClient kRPCClient, IHardwareClient hardwareClient, KeyboardInputClient keyboardInputClient, ControlFactory controlFactory, Logger logger)
        {
            this.kRPCClient = kRPCClient;
            this.hardwareClient = hardwareClient;
            this.keyboardInputClient = keyboardInputClient;
            this.controlFactory = controlFactory;
            this.logger = logger;
            debounce = new GameControlDebounce();
        }

        public void Start()
        {
            logger.Information("Controls added - starting KerbalKontroller");

            Vessel activeVessel;
            Action controlAction;

            while (true)
            {
                if (kRPCClient.IsGamePaused() || !kRPCClient.IsInFlight())
                    continue;

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

                if (debounce.GetIncreaseTimeWarpButtonState(hardwareClient.ReadIncreaseTimeWarpButton().Active)) keyboardInputClient.IncreaseTimeWarp();
                if (debounce.GetDecreaseTimeWarpButtonState(hardwareClient.ReadDecreaseTimeWarpButton().Active)) keyboardInputClient.DecreaseTimeWarp();
                if (debounce.GetNextVesselButtonState(hardwareClient.ReadNextVesselButton().Active)) keyboardInputClient.NextVessel();
                if (debounce.GetPreviousVesselButtonState(hardwareClient.ReadPreviousVesselButton().Active)) keyboardInputClient.PreviousVessel();
                if (debounce.GetCameraCycleButtonState(hardwareClient.ReadCameraCycleButton().Active)) keyboardInputClient.CameraCycle();
                if (debounce.GetOrbitalViewButtonState(hardwareClient.ReadOrbitalViewButton().Active)) keyboardInputClient.SetOrbitalView();
                if (debounce.GetPauseButtonState(hardwareClient.ReadPauseButton().Active)) kRPCClient.SetPaused();
                if (debounce.GetQuickSaveButtonState(hardwareClient.ReadQuickSaveButton().Active)) kRPCClient.QuickSave();
                if (debounce.GetQuickLoadButtonState(hardwareClient.ReadQuickLoadButton().Active)) kRPCClient.QuickLoad();
            }
        }
    }
}
