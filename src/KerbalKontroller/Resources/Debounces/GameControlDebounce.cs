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

        public bool GetIncreaseTimeWarpButtonState(bool buttonState)
        {
            currentIncreaseTimeWarpButtonState = buttonState;
            return currentIncreaseTimeWarpButtonState && !lastIncreaseTimeWarpButtonState;
        }

        public bool GetDecreaseTimeWarpButtonState(bool buttonState)
        {
            currentDecreaseTimeWarpButtonState = buttonState;
            return currentDecreaseTimeWarpButtonState && !lastDecreaseTimeWarpButtonState;
        }

        public bool GetNextVesselButtonState(bool buttonState)
        {
            currentNextVesselButtonState = buttonState;
            return currentNextVesselButtonState && !lastNextVesselButtonState;
        }

        public bool GetPreviousVesselButtonState(bool buttonState)
        {
            currentPreviousVesselButtonState = buttonState;
            return currentPreviousVesselButtonState && !lastPreviousVesselButtonState;
        }

        public bool GetCameraCycleButtonState(bool buttonState)
        {
            currentCameraCycleButtonState = buttonState;
            return currentCameraCycleButtonState && !lastCameraCycleButtonState;
        }

        public bool GetOrbitalViewButtonState(bool buttonState)
        {
            currentOrbitalViewButtonState = buttonState;
            return currentOrbitalViewButtonState && !lastOrbitalViewButtonState;
        }

        public bool GetPauseButtonState(bool buttonState)
        {
            currentPauseButtonState = buttonState;
            return currentPauseButtonState && !lastPauseButtonState;
        }

        public bool GetQuickSaveButtonState(bool buttonState)
        {
            currentQuickSaveButtonState = buttonState;
            return currentQuickSaveButtonState && !lastQuickSaveButtonState;
        }

        public bool GetQuickLoadButtonState(bool buttonState)
        {
            currentQuickLoadButtonState = buttonState;
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
