using BusinessLogic.DataModels;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourierHelper.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteOptimizationController : ControllerBase
    {
        private IRouteService _routeService;
        public RouteOptimizationController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpPost]
        public async Task<IActionResult> OptimizeRoute(List<LocationInfo> locations)
        {
            var locationInfo = await _routeService.GetOptimizedRouteAsync(locations);
            return Ok(locationInfo);
        }
    }
}
