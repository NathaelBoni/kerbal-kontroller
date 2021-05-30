using KerbalKontroller.Interfaces;

namespace KerbalKontroller.Resources.Debounces
{
    public class GameControlDebounce
    {
        private bool currentIncreaseTimeWarpButtonState, lastIncreaseTimeWarpButtonState;
        private bool currentDecreaseTimeWarpButtonState, lastDecreaseTimeWarpButtonState;
        private bool currentNextVesselButtonState, lastNextVesselButtonState;
        private bool currentPreviousVesselButtonState, lastPreviousVesselButtonState;
        private bool currentCameraCycleButtonState, lastCameraCycleButtonState;
        private bool currentOrbitalViewButtonState, lastOrbitalViewButtonState;
        private bool currentPauseButtonState, lastPauseButtonState;
        private bool currentQuickSaveButtonState, lastQuickSaveButtonState;
        private bool currentQuickLoadButtonState, lastQuickLoadButtonState;

        private readonly IHardwareClient hardwareClient;

        public GameControlDebounce(IHardwareClient hardwareClient)
        {
            this.hardwareClient = hardwareClient;
        }

        public bool GetIncreaseTimeWarpButtonState()
        {
            currentIncreaseTimeWarpButtonState = hardwareClient.ReadIncreaseTimeWarpButton().Active;
            return currentIncreaseTimeWarpButtonState && !lastIncreaseTimeWarpButtonState;
        }

        public bool GetDecreaseTimeWarpButtonState()
        {
            currentDecreaseTimeWarpButtonState = hardwareClient.ReadDecreaseTimeWarpButton().Active;
            return currentDecreaseTimeWarpButtonState && !lastDecreaseTimeWarpButtonState;
        }

        public bool GetNextVesselButtonState()
        {
            currentNextVesselButtonState = hardwareClient.ReadNextVesselButton().Active;
            return currentNextVesselButtonState && !lastNextVesselButtonState;
        }

        public bool GetPreviousVesselButtonState()
        {
            currentPreviousVesselButtonState = hardwareClient.ReadPreviousVesselButton().Active;
            return currentPreviousVesselButtonState && !lastPreviousVesselButtonState;
        }

        public bool GetCameraCycleButtonState()
        {
            currentCameraCycleButtonState = hardwareClient.ReadCameraCycleButton().Active;
            return currentCameraCycleButtonState && !lastCameraCycleButtonState;
        }

        public bool GetOrbitalViewButtonState()
        {
            currentOrbitalViewButtonState = hardwareClient.ReadOrbitalViewButton().Active;
            return currentOrbitalViewButtonState && !lastOrbitalViewButtonState;
        }

        public bool GetPauseButtonState()
        {
            currentPauseButtonState = hardwareClient.ReadPauseButton().Active;
            return currentPauseButtonState && !lastPauseButtonState;
        }

        public bool GetQuickSaveButtonState()
        {
            currentQuickSaveButtonState = hardwareClient.ReadQuickSaveButton().Active;
            return currentQuickSaveButtonState && !lastQuickSaveButtonState;
        }

        public bool GetQuickLoadButtonState()
        {
            currentQuickLoadButtonState = hardwareClient.ReadQuickLoadButton().Active;
            return currentQuickLoadButtonState && !lastQuickLoadButtonState;
        }

        public void UpdateState()
        {
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
