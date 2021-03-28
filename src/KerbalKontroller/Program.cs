using KerbalKontroller.Controls;
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

            var gameControl = serviceProvider.GetService<GameControl>();

            gameControl.Start();
        }
    }
}
