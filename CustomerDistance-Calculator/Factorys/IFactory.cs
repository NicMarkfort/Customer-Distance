using CustomerDistance_Calculator.Requets;
using CustomerDistance_Calculator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.Factorys
{
    public interface IFactory
    {
        public IDistanceService DistanceService { get; }
        public ILocationService LocationService { get; }
        public IRequest Request {  get; }
    }
}
