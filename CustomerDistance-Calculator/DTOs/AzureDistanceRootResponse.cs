using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.DTOs
{
    public class AzureDistanceRootResponse
    {
        public List<List<AzureMatrixEntryDto>> Matrix { get; set; } = new();
    }
}
