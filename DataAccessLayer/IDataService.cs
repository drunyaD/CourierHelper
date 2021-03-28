using DataAccessLayer.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDataService
    {
        Task<GeocodeResponse> GetGeocodeResponseAsync(string address);
        Task<DirectionsResponse> GetDirectionsResponseByAddressesAsync(List<string> addresses, TravelMode travelMode);
        Task<DirectionsResponse> GetDirectionsResponseByPlacesIdAsync(List<string> placeIds, TravelMode travelMode);
        Task<DirectionsResponse> GetDirectionsResponseByLocationsAsync(List<Location> coordinates, TravelMode travelMode);
    }
}
