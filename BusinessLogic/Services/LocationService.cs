using BusinessLogic.DataModels;
using BusinessLogic.Exceptions;
using DataAccessLayer;
using DataAccessLayer.DataModels;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class LocationService : ILocationService
    {
        private IDataService _dataService;

        public LocationService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<LocationInfo> GetLocationInfoAsync(string searchString)
        {
            if (searchString == null)
            {
                throw new ValidationException("Search string should be provided");
            }

            var response = await _dataService.GetGeocodeResponseAsync(searchString);
            if (response.Status != GeocodeStatusCode.OK)
            {
                throw new BllException("Can't get location info: status code " + response.Status);
            }

            var result = response.Results.First();

            return new LocationInfo
            {
                PlaceId = result.PlaceId,
                FormattedAddress = result.FormattedAddress,
                Coordinate = new Coordinate
                {
                    Latitude = result.Geometry.Location.Latitude,
                    Longitude = result.Geometry.Location.Longitude
                }
            };
        }
    }
}
