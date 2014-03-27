using System;
using System.Configuration;
using log4net;

namespace CdnLink
{
    public static class Helpers
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        public static string GetSetting(string name, string defaultOnNotFound = null)
        {
            Log.DebugFormat("GetSetting: '{0}'.", name);

            var isEnvironmentFirst = (bool)Settings.Default["ENVIRONMENT_FIRST"];
           
            var setting = Environment.GetEnvironmentVariable(name);
            if (setting != null && isEnvironmentFirst)
            {
                Log.DebugFormat("GotSetting: '{0}' from system environment.", setting);
                return setting;
            }

            try
            {
                setting = Settings.Default[name] as string;
                Log.DebugFormat("GotSetting: '{0}' from application settings.", setting);
                return setting;
            }
            catch (SettingsPropertyNotFoundException)
            {
                if (defaultOnNotFound != null)
                    return defaultOnNotFound;
                throw;
            }
        }
    }
}
