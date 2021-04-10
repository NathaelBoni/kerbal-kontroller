using ArduinoUploader.Hardware;
using KerbalKontroller.Clients;
using KerbalKontroller.Config;
using KerbalKontroller.Controls;
using KerbalKontroller.Interfaces;
using KerbalKontroller.Resources;
using KerbalKontroller.Resources.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;

namespace KerbalKontroller
{
    public static class ServiceConfigurator
    {
        public static ServiceProvider Configure(IServiceCollection services)
        {
            services.AddScoped(_ =>
            {
                return new LoggerConfiguration()
                    .WriteTo.Logger(__ =>
                        __.WriteTo.Console()
                        .Filter.ByIncludingOnly(log => log.Level != LogEventLevel.Error))
                    .WriteTo.Logger(__ =>
                        __.WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}/Log/error.txt", rollOnFileSizeLimit: true, fileSizeLimitBytes: 10000000)
                        .Filter.ByIncludingOnly(log => log.Level == LogEventLevel.Error))
                    .CreateLogger();
            });

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
                var logger = _.GetService<Logger>();

                return new ArduinoClient(pinConfiguration, appSettings, GetArduinoInfo(appSettings.ArduinoModel), logger);
            });

            services.AddSingleton<KeyboardInputClient>();

            services.AddSingleton<ControlFactory>();

            services.AddSingleton<IControl, SpaceShipControl>();
            services.AddSingleton<IControl, PlaneControl>();
            services.AddSingleton<IControl, RoverControl>();
            services.AddSingleton<IControl, KerbalControl>();
            services.AddSingleton<GameControl>();

            return services.BuildServiceProvider();
        }

        private static ArduinoInfo GetArduinoInfo(string model)
        {
            switch (model)
            {
                case "leonardo":
                    return new ArduinoInfo { ArduinoModel = ArduinoModel.Leonardo, NumberOfDigitalPorts = 14 };
                case "uno":
                    return new ArduinoInfo { ArduinoModel = ArduinoModel.UnoR3, NumberOfDigitalPorts = 14 };
                case "mega":
                    return new ArduinoInfo { ArduinoModel = ArduinoModel.Mega2560, NumberOfDigitalPorts = 54 };
                default:
                    throw new NotSupportedException("Arduino model not accepted. Possible choices are: leonardo | uno | mega");
            }
        }
    }
}
