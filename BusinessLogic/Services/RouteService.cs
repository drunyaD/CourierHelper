using BusinessLogic.Calculation;
using BusinessLogic.DataModels;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class RouteService : IRouteService
    {
        private IDataService _dataService;
        private IAlgorithmConfigProvider _configProvider;

        public RouteService(IDataService dataService, IAlgorithmConfigProvider configProvider)
        {
            _dataService = dataService;
            _configProvider = configProvider;
        }

        public async Task<OptimizedRoute> GetOptimizedRouteAsync(LocationInfo startLocation, List<LocationInfo> locationsToVisit)
        {
            var locationInfos = new List<LocationInfo> { startLocation };
            locationInfos.AddRange(locationsToVisit);
            locationInfos.Add(startLocation);

            var config = _configProvider.ProvideConfig(locationInfos.Count);
            var duelistAlgo = new DuelistAlgorithm<LocationInfo>(locationInfos, config);

            var optimizedSequence = duelistAlgo.Run().Keys.First();

            var placesIds = optimizedSequence.Select(x => locationInfos[x].PlaceId).ToList();
            var directionResponse =  await _dataService.GetDirectionsResponseByPlacesIdAsync(placesIds);
            var directionRoute = directionResponse.Routes.First();

            var segments = new List<Segment>();

            foreach (var l in directionRoute.Legs)
            {
                var coordinates = new List<Coordinate>();
                coordinates.Add(new Coordinate 
                {
                    Latitude = l.Steps.First().StartLocation.Latitude,
                    Longitude = l.Steps.First().StartLocation.Longitude
                });

                foreach (var s in l.Steps)
                {
                    coordinates.Add(new Coordinate
                    {
                        Latitude = s.EndLocation.Latitude,
                        Longitude = s.EndLocation.Longitude
                    });
                }
                segments.Add(new Segment
                {
                    Coordinates = coordinates,
                    FromAddress = l.StartAddress,
                    ToAddress = l.EndAddress
                });
            }

            return new OptimizedRoute
            {
                Polyline = directionRoute.OverviewPolyline.Points,
                Segments = segments
            };
        }
    }
}
