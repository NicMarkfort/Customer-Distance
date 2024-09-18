using CustomerDistance_Calculator.Requets;
using CustomerDistance_Calculator.Services;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDistance_Calculator.Factorys
{
    public class DefaultFactory(string _subscriptionKey) : IFactory
    {
        public IDistanceService DistanceService => new DistanceService(LocationService);

        public ILocationService LocationService => new LocationService(_subscriptionKey, Request);

        public IRequest Request => new Request(new HttpClient());
    }
}
