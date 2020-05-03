using IpLookup.Common;

namespace IpLookup.Services
{
    public class BaseService : IBaseService
    {
        protected readonly ILogHelper _logHelper;
        protected readonly IConfigurationHelper _configurationHelper;

        public BaseService(ILogHelper logHelper, IConfigurationHelper configurationHelper)
        {
            _logHelper = logHelper;
            _configurationHelper = configurationHelper;
        }
    }
}
