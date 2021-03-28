using KRPC.Client;
using KRPC.Client.Services.SpaceCenter;
using Serilog.Core;

namespace KerbalKontroller.Clients
{
    public class KRPCClient
    {
        private readonly Service service;
        private readonly Logger logger;

        public KRPCClient(Logger logger)
        {
            var client = new Connection("ksp");
            service = client.SpaceCenter();
            this.logger = logger;
        }

        public Vessel GetActiveVessel()
        {
            return service.ActiveVessel;
        }
    }
}
