using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using weaponsandwizardry.Services;

namespace weaponsandwizardry
{
    class Program
    {
        public static readonly string CONFIGURATION_FILENAME = "_configuration.json";

        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            IConfigurationRoot configuration = BuildConfiguration();
            ServiceCollection services = BuildServiceCollection(configuration);
            await StartServices(services);

            // Delay the main thread of execution forever.
            await Task.Delay(-1);
        }

        private IConfigurationRoot BuildConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(Program.CONFIGURATION_FILENAME);
            IConfigurationRoot configuration = builder.Build();
            return configuration;
        }

        private ServiceCollection BuildServiceCollection(IConfigurationRoot configuration)
        {
            ServiceCollection services = new ServiceCollection();

            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {                                       
                LogLevel = LogSeverity.Verbose,     
                MessageCacheSize = 1000             
            }))
            .AddSingleton(new CommandService(new CommandServiceConfig
            {                                       
                LogLevel = LogSeverity.Verbose,     
                DefaultRunMode = RunMode.Async,     
                CaseSensitiveCommands = false
            }))
            .AddSingleton<StartupService>()
            .AddSingleton<LoggingService>()
            .AddSingleton(configuration);

            return services;
        }

        private async Task StartServices(ServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();
            await provider.GetRequiredService<LoggingService>();
            await provider.GetRequiredService<StartupService>().StartAsync();
        }
    }
}
