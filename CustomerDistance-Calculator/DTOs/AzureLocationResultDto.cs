using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.DTOs
{
    public class AzureLocationResultDto
    {
        public string Type { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public double Score { get; set; }
        public AzureAddressDto? Address { get; set; }
        public AzurePositionDto? Position { get; set; }
    }
}
