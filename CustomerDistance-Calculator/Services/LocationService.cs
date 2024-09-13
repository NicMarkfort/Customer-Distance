using CustomerDistance_Calculator.DTOs;
using CustomerDistance_Calculator.Requets;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace CustomerDistance_Calculator.Services
{
    public class LocationService(string _subscriptionKey, IRequest _request) : ILocationService
    {

        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        private static readonly string _uri = "https://atlas.microsoft.com/";

        public async Task<DistanceDto> GetDistance(AzurePositionDto origin, AzurePositionDto destination)
        {
            UriBuilder uriBuilder = new(_uri + "route/matrix/json");
            var queryParams = GetDefaultQueryParams();
            queryParams["waitForResults"] = "true";
            queryParams["routeType"] = "fastest";
            queryParams["traffic"] = "true";
            uriBuilder.Query = queryParams.ToString();

            var requestBody = new AzureDistanceRequestDto(new AzureMultipointDto(origin.Lat, origin.Lon), new AzureMultipointDto(destination.Lat, destination.Lon));
            string responseBody = await _request.Post(uriBuilder.ToString(), JsonSerializer.Serialize(requestBody, _options));
            AzureDistanceRootResponse response = JsonSerializer.Deserialize<AzureDistanceRootResponse>(responseBody, _options) ?? throw new Exception();
            if (response.Matrix.Count == 0 || response.Matrix[0].Count == 0 || response.Matrix[0][0].Response == null || response.Matrix[0][0].Response?.RouteSummary == null)
                throw new Exception();
            var summary = response.Matrix[0][0].Response?.RouteSummary;
            if (summary == null)
                throw new Exception();

            return new DistanceDto
            {
                DistanceInMeters = summary.LengthInMeters,
                DurationInSeconds = summary.TravelTimeInSeconds
            };
        }

        public async Task<List<AzureLocationResultDto>> GetLocations(string query)
        {
            UriBuilder uriBuilder = new(_uri + "search/address/json");
            var queryParams = GetDefaultQueryParams();
            queryParams["language"] = "de-DE";
            queryParams["query"] = query;
            queryParams["countrySet"] = "DE";
            uriBuilder.Query = queryParams.ToString();

            string responseBody = await _request.Get(uriBuilder.ToString());
            AzureSearchResponseDto response = JsonSerializer.Deserialize<AzureSearchResponseDto>(responseBody, _options) ?? throw new Exception();
            if (response.Results == null || response.Results.Count == 0)
                throw new Exception();

            return response.Results;
        }

        private NameValueCollection GetDefaultQueryParams()
        {
            var uriQuery = HttpUtility.ParseQueryString("");
            uriQuery["subscription-key"] = _subscriptionKey;
            uriQuery["api-version"] = "1.0";
            return uriQuery;
        }
    }
}
