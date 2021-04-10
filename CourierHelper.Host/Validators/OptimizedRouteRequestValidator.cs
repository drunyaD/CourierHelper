using BusinessLogic.DataModels;
using BusinessLogic.Exceptions;
using CourierHelper.Host.Models;
using System.Collections.Generic;

namespace CourierHelper.Host.Validators
{
    public class OptimizedRouteRequestValidator : IOptimizedRouteRequestValidator
    {
        public void Validate(OptimizedRouteRequest optimizedRouteRequest)
        {
            var startLoc = optimizedRouteRequest.StartLocation;
            var locToVisit = optimizedRouteRequest.LocationsToVisit;

            if (startLoc == null)
            {
                throw new ValidationException("Start locataion should be provided");
            }

            if (locToVisit == null)
            {
                throw new ValidationException("Locations to visit should be provided");
            }

            var allLocInfos = new List<LocationInfo> { startLoc };
            allLocInfos.AddRange(locToVisit);

            foreach (var locationInfo in allLocInfos)
            {
                if (locationInfo.PlaceId == null)
                {
                    throw new ValidationException("All place ids should be provided");
                }

                if (locationInfo.Coordinate.Longitude == null)
                {
                    throw new ValidationException("All longitudes in coordinates should be provided");
                }

                if (locationInfo.Coordinate.Latitude == null)
                {
                    throw new ValidationException("All latitudes in coordinates should be provided");
                }
            }
        }
    }
}
