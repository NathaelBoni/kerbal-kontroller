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
        private readonly IKSPClient kspClient;
        private readonly KeyboardInputClient keyboardInputClient;
        private readonly ControlFactory controlFactory;
        private readonly GameControlDebounce debounce;
        private readonly Logger logger;

        public GameControl(IKSPClient kspClient, KeyboardInputClient keyboardInputClient, IHardwareClient hardwareClient, ControlFactory controlFactory, Logger logger)
        {
            this.kspClient = kspClient;
            this.keyboardInputClient = keyboardInputClient;
            this.controlFactory = controlFactory;
            debounce = new GameControlDebounce(hardwareClient);
            this.logger = logger;
        }

        public void Start()
        {
            logger.Information("Controls added - starting KerbalKontroller");

            while (true)
            {
                if (kspClient.IsGamePaused() || !kspClient.IsInFlight())
                    continue;

                try
                {
                    var controlAction = controlFactory.GetControlAction((Vessel)kspClient.GetActiveVessel());
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
                if (debounce.GetPauseButtonState()) kspClient.SetPaused();
                if (debounce.GetQuickSaveButtonState()) kspClient.QuickSave();
                if (debounce.GetQuickLoadButtonState()) kspClient.QuickLoad();
            }
        }
    }
}
