using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.DTOs
{
    public class AzureMatrixEntryDto
    {
        public int StatusCode { get; set; }
        public AzureDistanceResponseDto? Response { get; set; }
    }
}
