using System;
using log4net;

namespace CdnLink
{
    public static class Helpers
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        public static string GetSetting(string name)
        {
            _log.DebugFormat("GetSetting: '{0}'.", name);
            var setting = Environment.GetEnvironmentVariable(name);
            if (setting != null)
            {
                _log.DebugFormat("GotSetting: '{0}' from system environment.", setting);
                return setting;
            }

            setting = Settings.Default[name] as string;
            if (setting != null)
            {
                _log.DebugFormat("GotSetting: '{0}' from application settings.", setting);
                return setting;
            }

            _log.DebugFormat("GetSetting: '{0}' was not found.", name);
            return null;
        }
    }
}
