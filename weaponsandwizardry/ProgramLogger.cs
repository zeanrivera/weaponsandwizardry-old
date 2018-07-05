using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace weaponsandwizardry
{
    public static class ProgramLogger
    {
        public static Task Log(LogMessage log)
        {
            return Log(log.ToString());
        }

        public static Task Log(Exception e)
        {
            return Log(e.ToString());
        }

        public static Task Log(string log)
        {
            Task.Run(() => Console.WriteLine(log.ToString()));
            return Task.CompletedTask;
        }
    }
}
