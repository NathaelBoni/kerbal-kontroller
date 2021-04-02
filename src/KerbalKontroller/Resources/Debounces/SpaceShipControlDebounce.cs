namespace KerbalKontroller.Resources.Debounces
{
    public class SpaceShipControlDebounce
    {
        private bool lastStageButtonState, currentStageButtonState;
        private bool lastAbortButtonState, currentAbortButtonState;

        public bool GetStageButtonState(bool buttonState)
        {
            currentStageButtonState = buttonState;
            return currentStageButtonState && !lastStageButtonState;
        }

        public bool GetAbortButtonState(bool buttonState)
        {
            currentAbortButtonState = buttonState;
            return currentAbortButtonState && !lastAbortButtonState;
        }

        public void UpdateState()
        {
            lastStageButtonState = currentStageButtonState;
            lastAbortButtonState = currentAbortButtonState;
        }
    }
}
