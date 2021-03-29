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
        
        public void PauseGame() => krpc.Paused = true;
        
        public void UnpauseGame() => krpc.Paused = false;
        
        public void SwitchCameras()
        {
            var currentCamera = spaceCenter.Camera.Mode;
            spaceCenter.Camera.Mode = (CameraMode)((int)currentCamera++ % 7);
        }
        
        public void IncreaseTimeWarp()
        {
            if (spaceCenter.RailsWarpFactor < 7) spaceCenter.RailsWarpFactor++;
        }

        public void DecreaseTimeWarp()
        {
            if (spaceCenter.RailsWarpFactor > 0) spaceCenter.RailsWarpFactor--;
        }

        public void NextVessel()
        {
            var activeVessel = GetActiveVessel();
            var vessels = spaceCenter.Vessels.Where(_ => GetDistance(activeVessel, _) < 100);

            if (!vessels.Any()) return;

            spaceCenter.ActiveVessel = vessels.First();
        }

        public void PreviousVessel()
        {
            var activeVessel = GetActiveVessel();
            var vessels = spaceCenter.Vessels.Where(_ => GetDistance(activeVessel, _) < 100);

            if (!vessels.Any()) return;

            spaceCenter.ActiveVessel = vessels.Last();
        }

        public void QuickSave() => spaceCenter.Quicksave();

        public void QuickLoad() => spaceCenter.Quickload();

        private double GetDistance(Vessel vessel1, Vessel vessel2)
        {
            var referenceFrame = vessel1.ReferenceFrame;
            var vessel1Position = vessel1.Position(referenceFrame);
            var vessel2Position = vessel2.Position(referenceFrame);

            return Math.Sqrt(
                Math.Pow(vessel1Position.Item1 - vessel2Position.Item1, 2) +
                Math.Pow(vessel1Position.Item2 - vessel2Position.Item2, 2) +
                Math.Pow(vessel1Position.Item3 - vessel2Position.Item3, 2)
            );
        }
    }
}
