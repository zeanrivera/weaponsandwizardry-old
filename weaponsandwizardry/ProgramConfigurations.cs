using System;

namespace weaponsandwizardry
{
    public static class ProgramConfigurations
    {
        public static string GetProgramConfiguration(string variable)
        {
            return Environment.GetEnvironmentVariable(variable);
        }
    }
}
