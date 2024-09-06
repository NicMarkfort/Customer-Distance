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
    public class DefaultFactory : IFactory
    {
        public IDistanceService DistanceService => new DistanceService(LocationService);

        public ILocationService LocationService => new LocationService("CIEmeX16Xxr5LmuG9RsgsuzUGD4xuK9fNwfTEA18f1CwREU0oKYpJQQJ99AIAC5RqLJQ00MCAAAgAZMPfr5h", Request);

        public IRequest Request => new Request(new HttpClient());
    }
}
