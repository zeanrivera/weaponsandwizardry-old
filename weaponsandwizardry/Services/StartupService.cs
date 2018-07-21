using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace weaponsandwizardry.Services
{
    public class StartupService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;

        // DiscordSocketClient, CommandService, and IConfigurationRoot are injected automatically from the IServiceProvider
        public StartupService(
            DiscordSocketClient client,
            CommandService commands,
            IConfigurationRoot config)
        {
            _client = client;
            _commands = commands;
            _config = config;
        }

        public async Task StartAsync()
        {
            string discordToken = _config["tokens:discord"];     
            if (string.IsNullOrWhiteSpace(discordToken))
            {
                throw new Exception($"Please enter your bot's token into the `{Program.CONFIGURATION_FILENAME}` file found in the applications root directory.");
            }

            await _client.LoginAsync(TokenType.Bot, discordToken);     
            await _client.StartAsync();                               

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());    
        }
    }
}