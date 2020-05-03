using System;

namespace IpLookup.Common
{
    public interface ILogHelper
    {
        void LogError(string message);

        void LogError(Exception exception, string message);

        void LogInfo(string message);
    }
}
