using System.Threading.Tasks;

namespace IpLookupApi.Models
{
    public class IpTask<T> where T : class
    {
        public string ServiceName { get; set; }
        public Task<T> Task { get; set; }

        public IpTask(string name, Task<T> task)
        {
            ServiceName = name;
            Task = task;
        }
    }
}
