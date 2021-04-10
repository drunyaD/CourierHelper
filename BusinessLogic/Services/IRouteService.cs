using BusinessLogic.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IRouteService
    {
        public Task<OptimizedRoute> GetOptimizedRouteAsync(LocationInfo startLocation, List<LocationInfo> locationsToVisit);
    }
}
