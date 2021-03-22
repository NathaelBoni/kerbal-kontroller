using KerbalKontroller.Config;
using Microsoft.Extensions.Configuration;
using System;

namespace KerbalKontroller
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}/Config")
                .AddJsonFile("appsettings.json", false)
                .Build();

            var pinConfig = config.GetSection(nameof(PinConfiguration)).Get<PinConfiguration>();

            var krpc = new KRPCClient();
            var driver = new ArduinoFacade(pinConfig);

            var vessel = krpc.GetActiveVessel();

            while (true)
            {
                var leftJoystick = driver.ReadLeftJoystick();
                vessel.Control.Yaw = leftJoystick.XValue;
                vessel.Control.Pitch = leftJoystick.YValue;
            }
        }
    }
}
