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
        private readonly KeyboardInputClient keyboardInputClient;
        private readonly ControlFactory controlFactory;
        private readonly GameControlDebounce debounce;
        private readonly Logger logger;

        public GameControl(KRPCClient kRPCClient, KeyboardInputClient keyboardInputClient, ControlFactory controlFactory, IHardwareClient hardwareClient, Logger logger)
        {
            this.kRPCClient = kRPCClient;
            this.keyboardInputClient = keyboardInputClient;
            this.controlFactory = controlFactory;
            debounce = new GameControlDebounce(hardwareClient);
            this.logger = logger;
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

                if (debounce.GetIncreaseTimeWarpButtonState()) keyboardInputClient.IncreaseTimeWarp();
                if (debounce.GetDecreaseTimeWarpButtonState()) keyboardInputClient.DecreaseTimeWarp();
                if (debounce.GetNextVesselButtonState()) keyboardInputClient.NextVessel();
                if (debounce.GetPreviousVesselButtonState()) keyboardInputClient.PreviousVessel();
                if (debounce.GetCameraCycleButtonState()) keyboardInputClient.CameraCycle();
                if (debounce.GetOrbitalViewButtonState()) keyboardInputClient.SetOrbitalView();
                if (debounce.GetPauseButtonState()) kRPCClient.SetPaused();
                if (debounce.GetQuickSaveButtonState()) kRPCClient.QuickSave();
                if (debounce.GetQuickLoadButtonState()) kRPCClient.QuickLoad();
            }
        }
    }
}
