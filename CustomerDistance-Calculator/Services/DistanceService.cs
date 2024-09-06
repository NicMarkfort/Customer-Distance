using CustomerDistance_Calculator.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.Services
{
    public class DistanceService(ILocationService _locationService) : IDistanceService
    {
        public async Task<DistanceDto> GetDistance(string origin, string destination)
        {
            AzurePositionDto originLocation = GetPosition(await _locationService.GetLocations(origin)) ?? throw new Exception();
            AzurePositionDto destinationLocation = GetPosition(await _locationService.GetLocations(destination)) ?? throw new Exception();

            return await _locationService.GetDistance(originLocation, destinationLocation);
        }

        private static AzurePositionDto? GetPosition(List<AzureLocationResultDto> locations)
        {
            if(locations == null || locations.Count == 0) return null;
            if (locations.Count == 1)
                return locations[0].Position;
            
            var pointedAddress = locations.Where(location => location.Type == "Point Address").Select(location => location.Position).ToList();
            if (pointedAddress.Count > 0)
                return pointedAddress.First();

            var geographyAddress = locations.Where(location => location.Type == "Geography").Select(location => location.Position).ToList();
            if (geographyAddress.Count > 0)
                return geographyAddress.First();

            return locations[0].Position;
        }
    }
}
