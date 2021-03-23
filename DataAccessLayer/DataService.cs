using DataAccessLayer.Credentials;
using DataAccessLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataService : IDataService
    {        
        private readonly IDataRetriver _dataRetriever;
        private readonly IMapApiKeyProvider _apiKeyProvider;

        public DataService(IMapApiKeyProvider apiKeyProvider, IDataRetriver dataRetriver)
        {
            _apiKeyProvider = apiKeyProvider;
            _dataRetriever = dataRetriver;
        }

        public Task<DirectionsResponse> GetDirectionsResponseAsync(List<string> addresses, TravelMode travelMode = TravelMode.Walking)
        {
            if (addresses == null || addresses.Count < 2)
            {
                throw new ArgumentException("Addresses count should be >= 2");
            }

            var url = UrlConstructionTools.GetDirectionsUrl(addresses, travelMode, _apiKeyProvider.GetKey());

            return _dataRetriever.GetDataAsync<DirectionsResponse>(url);
        }

        public Task<DirectionsResponse> GetDirectionsResponseAsync(List<Location> coordinates, TravelMode travelMode = TravelMode.Walking)
        {
            if (coordinates == null || coordinates.Count < 2)
            {
                throw new ArgumentException("Coordinates count should be >= 2");
            }

            throw new NotImplementedException();
        }

        public Task<GeocodeResponse> GetGeocodeResponseAsync(string address)
        {
            var url = UrlConstructionTools.GetGeocodeUrl(address, _apiKeyProvider.GetKey());

            return _dataRetriever.GetDataAsync<GeocodeResponse>(url);
        }
    }
}
