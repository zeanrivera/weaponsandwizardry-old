using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;

namespace weaponsandwizardry.Services
{
    public class LoggingService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        private string LOG_FOLDER_NAME => "logs";
        private string LOG_BASE_DIRECTORY => AppContext.BaseDirectory;
        private string LOG_FILE_NAME_BEGINNING => $"{DateTime.UtcNow.ToString("yyyy-MM-dd")}";
        private string LOG_FILE_NAME_ENDING => "log";
        private string LOG_FILE_NAME => $"{LOG_FILE_NAME_BEGINNING}.{LOG_FILE_NAME_ENDING}";
        private string _logDirectory => Path.Combine(LOG_BASE_DIRECTORY, LOG_FOLDER_NAME);
        private string _logFile => Path.Combine(_logDirectory, LOG_FILE_NAME);

        public LoggingService(DiscordSocketClient discord, CommandService commands)
        {
            _client = discord;
            _commands = commands;

            _client.Log += OnLogAsync;
            _commands.Log += OnLogAsync;
        }

        private Task OnLogAsync(LogMessage msg)
        {
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }

            if (!File.Exists(_logFile))
            {
                File.Create(_logFile).Dispose();
            }
                

            string logText = $"{DateTime.UtcNow.ToString("hh:mm:ss")} [{msg.Severity}] {msg.Source}: {msg.Exception?.ToString() ?? msg.Message}";
            File.AppendAllText(_logFile, logText + "\n");     // Write the log text to a file

            return Console.Out.WriteLineAsync(logText);       // Write the log text to the console
        }
    }
};