using KerbalKontroller.Interfaces;

namespace KerbalKontroller.Resources.Debounces
{
    public class KerbalControlDebounce
    {
        private bool lastKerbalUseButtonState, currentKerbalUseButtonState;
        private bool lastKerbalJumpButtonState, currentKerbalJumpButtonState;
        private bool lastKerbalRunButtonState, currentKerbalRunButtonState;
        private bool lastKerbalBoardButtonState, currentKerbalBoardButtonState;
        private bool lastKerbalLetGoButtonState, currentKerbalLetGoButtonState;
        private bool lastKerbalParachuteButtonState, currentKerbalParachuteButtonState;
        private bool lastKerbalJetPackButtonState, currentKerbalJetPackButtonState;
        private bool lastKerbalConstructionButtonState, currentKerbalConstructionButtonState;

        private readonly IHardwareClient hardwareClient;

        public KerbalControlDebounce(IHardwareClient hardwareClient)
        {
            this.hardwareClient = hardwareClient;
        }

        public bool GetKerbalUseButtonState()
        {
            currentKerbalUseButtonState = hardwareClient.ReadKerbalUseButton().Active;
            return currentKerbalUseButtonState && !lastKerbalUseButtonState;
        }

        public bool GetKerbalJumpButtonState()
        {
            currentKerbalJumpButtonState = hardwareClient.ReadKerbalJumpButton().Active;
            return currentKerbalJumpButtonState && !lastKerbalJumpButtonState;
        }

        public bool GetKerbalRunButtonState()
        {
               currentKerbalRunButtonState = hardwareClient.ReadKerbalRunButton().Active;
            return currentKerbalRunButtonState && !lastKerbalRunButtonState;
        }

        public bool GetKerbalBoardButtonState()
        {
            currentKerbalBoardButtonState = hardwareClient.ReadKerbalBoardButton().Active;
            return currentKerbalBoardButtonState && !lastKerbalBoardButtonState;
        }

        public bool GetKerbalLetGoButtonState()
        {
            currentKerbalLetGoButtonState = hardwareClient.ReadKerbalLetGoButton().Active;
            return currentKerbalLetGoButtonState && !lastKerbalLetGoButtonState;
        }

        public bool GetKerbalParachuteButtonState()
        {
            currentKerbalParachuteButtonState = hardwareClient.ReadKerbalParachuteButton().Active;
            return currentKerbalParachuteButtonState && !lastKerbalParachuteButtonState;
        }

        public bool GetKerbalJetPackButtonState()
        {
            currentKerbalJetPackButtonState = hardwareClient.ReadKerbalJetPackButton().Active;
            return currentKerbalJetPackButtonState && !lastKerbalJetPackButtonState;
        }

        public bool GetKerbalConstructionButtonState()
        {
            currentKerbalConstructionButtonState = hardwareClient.ReadKerbalConstructionButton().Active;
            return currentKerbalConstructionButtonState && !lastKerbalConstructionButtonState;
        }

        public void UpdateState()
        {
            lastKerbalUseButtonState = currentKerbalUseButtonState;
            lastKerbalJumpButtonState = currentKerbalJumpButtonState;
            lastKerbalRunButtonState = currentKerbalRunButtonState;
            lastKerbalBoardButtonState = currentKerbalBoardButtonState;
            lastKerbalLetGoButtonState = currentKerbalLetGoButtonState;
            lastKerbalParachuteButtonState = currentKerbalParachuteButtonState;
            lastKerbalJetPackButtonState = currentKerbalJetPackButtonState;
            lastKerbalConstructionButtonState = currentKerbalConstructionButtonState;
        }
    }
}
