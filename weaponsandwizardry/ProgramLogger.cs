using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace weaponsandwizardry
{
    public static class ProgramLogger
    {
        public static async Task LogAsync(LogMessage log)
        {
            await LogAsync(log.ToString());
        }

        public static async Task LogAsync(Exception e)
        {
            await LogAsync(e.ToString());
        }

        public static async Task LogAsync(string log)
        {
            await Task.Run(() => Console.WriteLine(log.ToString()));
        }
    }
}
