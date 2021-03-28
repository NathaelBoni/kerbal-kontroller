using KRPC.Client;
using KRPC.Client.Services.KRPC;
using KRPC.Client.Services.SpaceCenter;
using Serilog.Core;

namespace KerbalKontroller.Clients
{
    public class KRPCClient
    {
        private readonly KRPC.Client.Services.SpaceCenter.Service spaceCenter;
        private readonly KRPC.Client.Services.KRPC.Service krpc;
        private readonly Logger logger;

        public KRPCClient(Logger logger)
        {
            var client = new Connection("ksp");
            krpc = client.KRPC();
            spaceCenter = client.SpaceCenter();

            this.logger = logger;
        }

        public Vessel GetActiveVessel()
        {
            try
            {
                return spaceCenter.ActiveVessel;
            }
            catch
            {
                return null;
            }
        }

        public bool IsGamePaused() => krpc.Paused;

        public void PauseGame() => krpc.Paused = true;

        public void UnpauseGame() => krpc.Paused = false;
    }
}
