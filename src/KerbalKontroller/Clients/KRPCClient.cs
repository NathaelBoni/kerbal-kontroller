using KerbalKontroller.Resources;
using KRPC.Client;
using KRPC.Client.Services.KRPC;
using KRPC.Client.Services.SpaceCenter;
using Polly;
using Serilog.Core;
using System;
using System.Linq;

namespace KerbalKontroller.Clients
{
    public class KRPCClient
    {
        private readonly KRPC.Client.Services.SpaceCenter.Service spaceCenter;
        private readonly KRPC.Client.Services.KRPC.Service krpc;
        private readonly Logger logger;

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
                }, () => { });

            return circuitBreakerPolicy.Wrap(retryPolicy);
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

        public bool IsInFlight() => krpc.CurrentGameScene == GameScene.Flight;
        
        public bool IsGamePaused() => krpc.Paused;
        
        public void SetPaused() => krpc.Paused = !krpc.Paused;

        public void QuickSave() => spaceCenter.Quicksave();
        
        public void QuickLoad() => spaceCenter.Quickload();

        public void SwitchCameras()
        {
            var currentCamera = spaceCenter.Camera.Mode;
            spaceCenter.Camera.Mode = (CameraMode)((int)currentCamera++ % 7);
        }
        
        public void SetVesselRotation(JoystickAxis joystickAxis, JoystickAxis joystickAxisExtra)
        {
            var vessel = GetActiveVessel();
            vessel.Control.Pitch = joystickAxis.YValue;
            vessel.Control.Yaw = joystickAxis.XValue;
            vessel.Control.Roll = joystickAxisExtra.XValue;
        }

        public void SetVesselTranslation(JoystickAxis joystickAxis, JoystickAxis joystickAxisExtra)
        {
            var vessel = GetActiveVessel();
            vessel.Control.Up = joystickAxis.YValue;
            vessel.Control.Right = joystickAxis.XValue;
            vessel.Control.Forward = joystickAxisExtra.XValue;
        }

        public void SetThrottle(JoystickAxis joystickAxis)
        {
            var vessel = GetActiveVessel();
            vessel.Control.Throttle = joystickAxis.YValue;
        }

        public void SetLandingGear(DigitalState digitalState)
        {
            var vessel = GetActiveVessel();
            vessel.Control.Gear = digitalState.Active;
        }

        public void SetBrakes(DigitalState digitalState)
        {
            var vessel = GetActiveVessel();
            vessel.Control.Brakes = digitalState.Active;
        }

        public void SetLights(DigitalState digitalState)
        {
            var vessel = GetActiveVessel();
            vessel.Control.Lights = digitalState.Active;
        }

        public void SetSASMode(DigitalState digitalState)
        {
            var vessel = GetActiveVessel();
            vessel.Control.SAS = digitalState.Active;
        }

        public void SetRCSMode(DigitalState digitalState)
        {
            var vessel = GetActiveVessel();
            vessel.Control.RCS = digitalState.Active;
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
