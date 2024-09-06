using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.DTOs
{
    public class AzureMultipointDto : IAzurePointDto
    {
        private readonly List<List<double>> _coordinates = [];

        public AzureMultipointDto() { }

        public AzureMultipointDto(List<List<double>> coordinates) {
            _coordinates = coordinates;
        }

        public AzureMultipointDto(double lat, double lng)
        {
            _coordinates = [[lng, lat]];
        }

        public string Type => "MultiPoint";

        public List<List<double>> Coordinates => _coordinates;
    }
}
