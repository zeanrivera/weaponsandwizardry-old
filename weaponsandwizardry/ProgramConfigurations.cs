using System;

namespace weaponsandwizardry
{
    public static class ProgramConfigurations
    {
        /// List of Configurations in this Program:
        /// 
        /// DISCORD_BOT_TOKEN
        /// GAME_STATUS
        /// DATABASE_CONNECTION_STRING
        /// PREFIXES
        /// 

        public static string GetProgramConfiguration(string variable)
        {
            return Environment.GetEnvironmentVariable(variable);
        }
    }
}
