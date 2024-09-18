using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.Services
{
    public interface IConfigService
    {
        Task<string?> GetSubscriptionKey();
        Task SaveSubscriptionKey(string key);
    }
}
