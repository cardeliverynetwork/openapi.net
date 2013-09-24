using System;
using log4net;

namespace CdnLink
{
    public static class Helpers
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        public static string GetSetting(string name)
        {
            Log.DebugFormat("GetSetting: '{0}'.", name);

            var isEnvironmentFirst = (bool)Settings.Default["ENVIRONMENT_FIRST"];
           
            var setting = Environment.GetEnvironmentVariable(name);
            if (setting != null && isEnvironmentFirst)
            {
                Log.DebugFormat("GotSetting: '{0}' from system environment.", setting);
                return setting;
            }

            setting = Settings.Default[name] as string;
            if (setting != null)
            {
                Log.DebugFormat("GotSetting: '{0}' from application settings.", setting);
                return setting;
            }

            Log.DebugFormat("GetSetting: '{0}' was not found.", name);
            return null;
        }
    }
}
