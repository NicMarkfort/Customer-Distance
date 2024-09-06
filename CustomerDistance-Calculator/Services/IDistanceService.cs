using CustomerDistance_Calculator.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.Services
{
    public interface IDistanceService
    {
        Task<DistanceDto> GetDistance(string origin, string destination);
    }
}
