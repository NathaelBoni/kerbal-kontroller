using KerbalKontroller.Interfaces;
using KRPC.Client.Services.SpaceCenter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KerbalKontroller.Resources.Factories
{
    public class ControlFactory
    {
        private readonly IControl spaceShipControl;
        private readonly IControl planeControl;
        private readonly IControl roverControl;
        private readonly IControl kerbalControl;

        private static readonly VesselType[] spaceShipControlTypes = { VesselType.Lander, VesselType.Probe, VesselType.Relay, VesselType.Ship, VesselType.Station };
        private static readonly VesselType[] planeControlTypes = { VesselType.Plane };
        private static readonly VesselType[] roverControlTypes = { VesselType.Rover };

        public ControlFactory(IEnumerable<IControl> controls)
        {
            spaceShipControl = controls.FirstOrDefault(_ => _.ControlType == ControlType.SpaceShip);
            planeControl = controls.FirstOrDefault(_ => _.ControlType == ControlType.Plane);
            roverControl = controls.FirstOrDefault(_ => _.ControlType == ControlType.Rover);
            kerbalControl = controls.FirstOrDefault(_ => _.ControlType == ControlType.Kerbal);
        }

        public Action GetControlAction(Vessel vessel)
        {
            if (vessel == null) return null;
            if (vessel.Parts.Root.Name.StartsWith("kerbalEVA")) return kerbalControl.ControlLoop;
            if (spaceShipControlTypes.Contains(vessel.Type)) return spaceShipControl.ControlLoop;
            if (planeControlTypes.Contains(vessel.Type)) return planeControl.ControlLoop;
            if (roverControlTypes.Contains(vessel.Type)) return roverControl.ControlLoop;

            return null;
        }
    }
}
