using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.DTOs
{
    public class AzureRouteSummaryDto
    {
        public int LengthInMeters { get; set; }
        public int TravelTimeInSeconds { get; set; }
        public int TrafficDelayInSeconds { get; set; }
        public int TrafficLengthInMeters { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}