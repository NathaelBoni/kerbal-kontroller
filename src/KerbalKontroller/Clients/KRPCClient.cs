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

        public KRPCClient(Logger logger)
        {
            this.logger = logger;
            
            this.logger.Information($"Connecting to KRPC");

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

        public bool IsGamePaused() => krpc.Paused;

        public void PauseGame() => krpc.Paused = true;

        public void UnpauseGame() => krpc.Paused = false;
    }
}
