using KRPC.Client.Services.SpaceCenter;

namespace KerbalKontroller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connection = new KRPC.Client.Connection("ksp");
            var vessel = connection.SpaceCenter().ActiveVessel;

            vessel.Control.Pitch = 0.5f;
        }
    }
}
