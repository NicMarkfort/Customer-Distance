using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.DTOs
{
    public class AzureSearchResponseDto
    {
        public AzureSummaryResultDto Summary { get; set; }
        public List<AzureLocationResultDto> Results { get; set; } = [];
    }
}
