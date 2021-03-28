using KRPC.Client.Services.SpaceCenter;
using System.Linq;

namespace KerbalKontroller.Resources.Helpers
{
    public static class ActiveControlHelper
    {
        private static readonly VesselType[] spaceShipControlTypes = { VesselType.Lander, VesselType.Probe, VesselType.Relay, VesselType.Ship, VesselType.Station };
        private static readonly VesselType[] planeControlTypes = { VesselType.Plane };
        private static readonly VesselType[] roverControlTypes = { VesselType.Rover };

        public static ControlType SelectControlType(Vessel vessel)
        {
            if (vessel.Parts.Root.Name.StartsWith("kerbalEVA")) return ControlType.Kerbal;
            if (spaceShipControlTypes.Contains(vessel.Type)) return ControlType.SpaceShip;
            if (planeControlTypes.Contains(vessel.Type)) return ControlType.Plane;
            if (roverControlTypes.Contains(vessel.Type)) return ControlType.Rover;

            return ControlType.None;
        }
    }
}
