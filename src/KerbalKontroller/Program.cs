using KerbalKontroller.Clients;
using KerbalKontroller.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;

namespace KerbalKontroller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            var serviceProvider = ServiceConfigurator.Configure(serviceCollection);

            var log = serviceProvider.GetService<Logger>();

            var krpc = serviceProvider.GetService<KRPCClient>();
            var driver = serviceProvider.GetService<IHardwareClient>();

            var vessel = krpc.GetActiveVessel();

            while (true)
            {
                var leftJoystick = driver.ReadLeftJoyStick();
                var sasSwitch = driver.ReadSASSwitch();

                vessel.Control.Yaw = leftJoystick.XValue;
                vessel.Control.Pitch = leftJoystick.YValue;

                vessel.Control.SAS = sasSwitch.Active;

                driver.WriteSASLed(sasSwitch.Active);
                log.Information(sasSwitch.Active.ToString());
            }
        }
    }
}
