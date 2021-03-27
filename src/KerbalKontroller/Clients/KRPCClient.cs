using KRPC.Client;
using KRPC.Client.Services.SpaceCenter;

namespace KerbalKontroller.Clients
{
    public class KRPCClient
    {
        private readonly Service Service;

        public KRPCClient()
        {
            var client = new Connection("ksp");
            Service = client.SpaceCenter();
        }

        public Vessel GetActiveVessel()
        {
            return Service.ActiveVessel;
        }
    }
}
