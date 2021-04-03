using KerbalKontroller.Interfaces;

namespace KerbalKontroller.Resources.Debounces
{
    public class VesselControlDebounce
    {
        private bool lastStageButtonState, currentStageButtonState;
        private bool lastAbortButtonState, currentAbortButtonState;
        private bool lastAction1ButtonState, currentAction1ButtonState;
        private bool lastAction2ButtonState, currentAction2ButtonState;
        private bool lastAction3ButtonState, currentAction3ButtonState;
        private bool lastAction4ButtonState, currentAction4ButtonState;
        private bool lastAction5ButtonState, currentAction5ButtonState;
        private bool lastAction6ButtonState, currentAction6ButtonState;
        private bool lastAction7ButtonState, currentAction7ButtonState;
        private bool lastAction8ButtonState, currentAction8ButtonState;
        private bool lastAction9ButtonState, currentAction9ButtonState;
        private bool lastAction10ButtonState, currentAction10ButtonState;


        private readonly IHardwareClient hardwareClient;

        public VesselControlDebounce(IHardwareClient hardwareClient)
        {
            this.hardwareClient = hardwareClient;
        }

        public bool GetStageButtonState()
        {
            currentStageButtonState = hardwareClient.ReadStageButton().Active;
            return currentStageButtonState && !lastStageButtonState;
        }

        public bool GetAbortButtonState()
        {
            currentAbortButtonState = hardwareClient.ReadAbortButton().Active;
            return currentAbortButtonState && !lastAbortButtonState;
        }

        public bool GetAction1ButtonState()
        {
            currentAction1ButtonState = hardwareClient.ReadAction1Button().Active;
            return currentAction1ButtonState && !lastAction1ButtonState;
        }

        public bool GetAction2ButtonState()
        {
            currentAction2ButtonState = hardwareClient.ReadAction2Button().Active;
            return currentAction2ButtonState && !lastAction2ButtonState;
        }

        public bool GetAction3ButtonState()
        {
            currentAction3ButtonState = hardwareClient.ReadAction3Button().Active;
            return currentAction3ButtonState && !lastAction3ButtonState;
        }

        public bool GetAction4ButtonState()
        {
            currentAction4ButtonState = hardwareClient.ReadAction4Button().Active;
            return currentAction4ButtonState && !lastAction4ButtonState;
        }

        public bool GetAction5ButtonState()
        {
            currentAction5ButtonState = hardwareClient.ReadAction5Button().Active;
            return currentAction5ButtonState && !lastAction5ButtonState;
        }

        public bool GetAction6ButtonState()
        {
            currentAction6ButtonState = hardwareClient.ReadAction6Button().Active;
            return currentAction6ButtonState && !lastAction6ButtonState;
        }

        public bool GetAction7ButtonState()
        {
            currentAction7ButtonState = hardwareClient.ReadAction7Button().Active;
            return currentAction7ButtonState && !lastAction7ButtonState;
        }

        public bool GetAction8ButtonState()
        {
            currentAction8ButtonState = hardwareClient.ReadAction8Button().Active;
            return currentAction8ButtonState && !lastAction8ButtonState;
        }

        public bool GetAction9ButtonState()
        {
            currentAction9ButtonState = hardwareClient.ReadAction9Button().Active;
            return currentAction9ButtonState && !lastAction9ButtonState;
        }

        public bool GetAction10ButtonState()
        {
            currentAction10ButtonState = hardwareClient.ReadAction10Button().Active;
            return currentAction10ButtonState && !lastAction10ButtonState;
        }

        public void UpdateState()
        {
            lastStageButtonState = currentStageButtonState;
            lastAbortButtonState = currentAbortButtonState;
            lastAction1ButtonState = currentAction1ButtonState;
            lastAction2ButtonState = currentAction2ButtonState;
            lastAction3ButtonState = currentAction3ButtonState;
            lastAction4ButtonState = currentAction4ButtonState;
            lastAction5ButtonState = currentAction5ButtonState;
            lastAction6ButtonState = currentAction6ButtonState;
            lastAction7ButtonState = currentAction7ButtonState;
            lastAction8ButtonState = currentAction8ButtonState;
            lastAction9ButtonState = currentAction9ButtonState;
            lastAction10ButtonState = currentAction10ButtonState;
        }
    }
}
