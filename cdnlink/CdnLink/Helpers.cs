using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using log4net;

namespace CdnLink
{
    public static class Helpers
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        public static Dictionary<string, string> GetDictionarySetting(string name)
        {
            var section = (NameValueCollection)ConfigurationManager.GetSection(name);
            return section != null
                ? section.AllKeys.ToDictionary(k => k, k => section[k])
                : null;
        }

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
