using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace weaponsandwizardry
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            IServiceProvider services = ConfigureServices();
            DiscordSocketClient client = services.GetRequiredService<DiscordSocketClient>();

            // Register Logging
            client.Log += ProgramLogger.LogAsync;
            services.GetRequiredService<CommandService>().Log += ProgramLogger.LogAsync;

            // Login Bot.
            await client.LoginAsync(TokenType.Bot, ProgramConfigurations.GetProgramConfiguration("DISCORD_BOT_TOKEN"));
            await client.StartAsync();

            // Set game status message.
            await client.SetGameAsync(ProgramConfigurations.GetProgramConfiguration("GAME_STATUS"));



            // Sleep the main thread of execution forever.
            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .BuildServiceProvider();
        }
    }
}
