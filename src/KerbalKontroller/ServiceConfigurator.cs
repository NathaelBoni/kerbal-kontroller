using ArduinoUploader.Hardware;
using KerbalKontroller.Clients;
using KerbalKontroller.Config;
using KerbalKontroller.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KerbalKontroller
{
    public static class ServiceConfigurator
    {
        public static ServiceProvider Configure(IServiceCollection services)
        {
            services.AddSingleton(_ =>
            {
                return new ConfigurationBuilder()
                    .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}/Config")
                    .AddJsonFile("appSettings.json", false)
                    .AddJsonFile("pinConfiguration.json", false)
                    .Build();
            });

            services.AddSingleton(_ =>
            {
                var config = _.GetService<IConfigurationRoot>();
                return config.Get<PinConfiguration>();
            });

            services.AddSingleton(_ =>
            {
                var config = _.GetService<IConfigurationRoot>();
                return config.Get<AppSettings>();
            });

            services.AddSingleton<KRPCClient>();

            services.AddSingleton<IHardwareClient>(_ =>
            {
                var pinConfiguration = _.GetService<PinConfiguration>();
                var appSettings = _.GetService<AppSettings>();

                return new ArduinoClient(pinConfiguration, appSettings, GetArduinoModel(appSettings.ArduinoModel));
            });

            return services.BuildServiceProvider();
        }

        private static ArduinoModel GetArduinoModel(string model)
        {
            switch (model)
            {
                case "leonardo":
                    return ArduinoModel.Leonardo;
                case "uno":
                    return ArduinoModel.UnoR3;
                case "mega1284":
                    return ArduinoModel.Mega1284;
                case "mega2560":
                    return ArduinoModel.Mega2560;
                default:
                    throw new NotSupportedException("Arduino model not accepted. Possible choices are: leonardo, uno, mega1284 or mega2560");
            }
        }
    }
}
