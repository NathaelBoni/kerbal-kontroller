using KerbalKontroller.Resources;
using KRPC.Client;
using KRPC.Client.Services.KRPC;
using KRPC.Client.Services.SpaceCenter;
using Polly;
using Serilog.Core;
using System;

namespace KerbalKontroller.Clients
{
    public class KRPCClient
    {
        private readonly KRPC.Client.Services.SpaceCenter.Service spaceCenter;
        private readonly KRPC.Client.Services.KRPC.Service krpc;
        private readonly Logger logger;
        private Vessel ActiveVessel;

        public KRPCClient(Logger logger)
        {
            this.logger = logger;

            this.logger.Information($"Connecting to KRPC...");

            var retryPolicy = CreateRetryPolicy();
            var client = retryPolicy.Execute(() =>
            {
                return new Connection("ksp");
            });
            krpc = client.KRPC();
            spaceCenter = client.SpaceCenter();

            logger.Information("KRPC connected!");
        }

        private Policy CreateRetryPolicy()
        {
            var retryPolicy = Policy
                .Handle<System.Net.Sockets.SocketException>()
                .WaitAndRetry(10, _ =>
                {
                    logger.Information($"Try #{_}...");
                    return TimeSpan.FromSeconds(5);
                });

            var circuitBreakerPolicy = Policy
                .Handle<System.Net.Sockets.SocketException>()
                .CircuitBreaker(1, TimeSpan.FromSeconds(5), (ex, t) =>
                {
                    logger.Information(ex, "Fatal error - unable to connect to KRPC");
                }, null);

            return circuitBreakerPolicy.Wrap(retryPolicy);
        }

        public Vessel GetActiveVessel()
        {
            try
            {
                ActiveVessel = spaceCenter.ActiveVessel;
                return ActiveVessel;
            }
            catch
            {
                return null;
            }
        }

        public bool IsInFlight() => krpc.CurrentGameScene == GameScene.Flight;
        
        public bool IsGamePaused() => krpc.Paused;
        
        public void SetPaused() => krpc.Paused = !krpc.Paused;

        public void QuickSave() => spaceCenter.Quicksave();
        
        public void QuickLoad() => spaceCenter.Quickload();

        public void SetVesselRotation(JoystickAxis joystickAxis, JoystickAxis joystickAxisExtra)
        {
            ActiveVessel.Control.Pitch = joystickAxis.YValue;
            ActiveVessel.Control.Yaw = joystickAxis.XValue;
            ActiveVessel.Control.Roll = joystickAxisExtra.XValue;
        }

        public void SetVesselTranslation(JoystickAxis joystickAxis, JoystickAxis joystickAxisExtra)
        {
            ActiveVessel.Control.Up = joystickAxis.YValue;
            ActiveVessel.Control.Right = joystickAxis.XValue;
            ActiveVessel.Control.Forward = joystickAxisExtra.XValue;
        }

        public void SetPlaneRotation(JoystickAxis joystickAxis, JoystickAxis joystickAxisExtra)
        {
            ActiveVessel.Control.Pitch = joystickAxis.YValue;
            ActiveVessel.Control.Yaw = joystickAxisExtra.XValue;
            ActiveVessel.Control.Roll = joystickAxis.XValue;
        }

        public void SetPlaneTranslation(JoystickAxis joystickAxis, JoystickAxis joystickAxisExtra)
        {
            SetVesselTranslation(joystickAxis, joystickAxisExtra);
        }

        public void SetThrottle(JoystickAxis joystickAxis)
        {
            ActiveVessel.Control.Throttle = joystickAxis.YValue;
        }

        public void SetLandingGear(DigitalState digitalState)
        {
            ActiveVessel.Control.Gear = digitalState.Active;
        }

        public void SetBrakes(DigitalState digitalState)
        {
            ActiveVessel.Control.Brakes = digitalState.Active;
        }

        public void SetLights(DigitalState digitalState)
        {
            ActiveVessel.Control.Lights = digitalState.Active;
        }

        public void SetSASMode(DigitalState digitalState)
        {
            ActiveVessel.Control.SAS = digitalState.Active;
        }

        public void SetRCSMode(DigitalState digitalState)
        {
            ActiveVessel.Control.RCS = digitalState.Active;
        }

        public void Abort()
        {
            ActiveVessel.Control.Abort = true;
        }

        public void Stage()
        {
            ActiveVessel.Control.ActivateNextStage();
        }

        public void SetSASModeFree() => SetSASMode(SASMode.StabilityAssist);
        public void SetSASModeManeuver() => SetSASMode(SASMode.Maneuver);
        public void SetSASModePrograde() => SetSASMode(SASMode.Prograde);
        public void SetSASModeRetrograde() => SetSASMode(SASMode.Retrograde);
        public void SetSASModeNormal() => SetSASMode(SASMode.Normal);
        public void SetSASModeAntiNormal() => SetSASMode(SASMode.AntiNormal);
        public void SetSASModeRadialIn() => SetSASMode(SASMode.Radial);
        public void SetSASModeRadialOut() => SetSASMode(SASMode.AntiRadial);
        public void SetSASModeTarget() => SetSASMode(SASMode.Target);
        public void SetSASModeAntiTarget() => SetSASMode(SASMode.AntiTarget);

        private void SetSASMode(SASMode sasMode)
        {
            var vessel = GetActiveVessel();
            vessel.Control.SASMode = sasMode;
        }
    }
}
