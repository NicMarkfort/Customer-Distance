using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.DTOs
{
    public interface IAzurePointDto
    {
        public string Type { get; }
        public List<List<double>> Coordinates { get; }
    }
}
