using KerbalKontroller.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace KerbalKontroller
{
    public class Program
    {
        public static void Main()
        {
            var serviceCollection = new ServiceCollection();
            var serviceProvider = ServiceConfigurator.Configure(serviceCollection);

            var gameControl = serviceProvider.GetService<GameControl>();

            gameControl.Start();
        }
    }
}
