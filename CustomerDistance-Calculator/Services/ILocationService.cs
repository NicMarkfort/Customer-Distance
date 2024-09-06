using CustomerDistance_Calculator.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.Services
{
    public interface ILocationService
    {
        Task<List<AzureLocationResultDto>> GetLocations(string query);
        Task<DistanceDto> GetDistance(AzurePositionDto origin, AzurePositionDto destination);
    }
}
