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
            Resources.LedState sasLedState;

            while (true)
            {
                var leftJoystick = driver.ReadLeftJoyStick();
                var sasSwitch = driver.ReadSASSwitch();

                vessel.Control.Yaw = leftJoystick.XValue;
                vessel.Control.Pitch = leftJoystick.YValue;

                vessel.Control.SAS = sasSwitch.Active;

                sasLedState = sasSwitch.Active ? Resources.LedState.On : Resources.LedState.Off;
                driver.WriteSASLed(sasLedState);
                log.Information(sasLedState.ToString());
            }
        }
    }
}
