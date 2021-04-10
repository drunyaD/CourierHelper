using BusinessLogic.DataModels;
using System.Collections.Generic;

namespace CourierHelper.Host.Models
{
    public class OptimizedRouteRequest
    {
        public LocationInfo StartLocation { get; set; }

        public List<LocationInfo> LocationsToVisit { get; set; }
    }
}
