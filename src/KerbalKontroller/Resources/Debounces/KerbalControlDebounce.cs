using KerbalKontroller.Interfaces;

namespace KerbalKontroller.Resources.Debounces
{
    public class KerbalControlDebounce
    {
        private bool lastKerbalUseButtonState, currentKerbalUseButtonState;
        private bool lastKerbalJumpButtonState, currentKerbalJumpButtonState;
        private bool lastKerbalBoardButtonState, currentKerbalBoardButtonState;
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

        public bool GetKerbalBoardButtonState()
        {
            currentKerbalBoardButtonState = hardwareClient.ReadKerbalBoardButton().Active;
            return currentKerbalBoardButtonState && !lastKerbalBoardButtonState;
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
            lastKerbalBoardButtonState = currentKerbalBoardButtonState;
            lastKerbalParachuteButtonState = currentKerbalParachuteButtonState;
            lastKerbalJetPackButtonState = currentKerbalJetPackButtonState;
            lastKerbalConstructionButtonState = currentKerbalConstructionButtonState;
        }
    }
}
