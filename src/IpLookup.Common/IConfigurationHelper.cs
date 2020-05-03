using System.Collections.Generic;

namespace IpLookup.Common
{
    public interface IConfigurationHelper
    {
        string GetAppSettings(string key);

        int GetAppSettingsAsInt(string key, int defaultValue);

        double GetAppSettingsAsDouble(string key, double defaultValue);

        bool GetAppSettingsAsBoolean(string key, bool defaultValue = false);

        IEnumerable<string> GetAppSettingsList(string key);
    }
}
