using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.DTOs
{
    public class AzureDistanceRequestDto(AzureMultipointDto origins, AzureMultipointDto destinations)
    {
        public AzureMultipointDto Origins { get; set; } = origins;
        public AzureMultipointDto Destinations { get; set; } = destinations;
    }
}
