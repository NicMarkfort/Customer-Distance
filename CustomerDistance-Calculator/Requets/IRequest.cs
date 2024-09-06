using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.Requets
{
    public interface IRequest
    {
        Task<string> Get(string endpoint);
        Task<string> Post(string endpoint, string body);
    }
}
