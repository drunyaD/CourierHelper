using DataAccessLayer.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDataService
    {
        Task<GeocodeResponse> GetGeocodeResponseAsync(string address);
        Task<DirectionsResponse> GetDirectionsResponseAsync(List<string> addresses, TravelMode travelMode);
        Task<DirectionsResponse> GetDirectionsResponseAsync(List<Location> coordinates, TravelMode travelMode);
    }
}
