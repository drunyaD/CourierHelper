using BusinessLogic.DataModels;
using BusinessLogic.Services;
using CourierHelper.Host.Models;
using CourierHelper.Host.Validators;
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
        private IOptimizedRouteRequestValidator _optimizedRouteRequestValidator;
        public RouteOptimizationController(IRouteService routeService, IOptimizedRouteRequestValidator optimizedRouteRequestValidator)
        {
            _routeService = routeService;
            _optimizedRouteRequestValidator = optimizedRouteRequestValidator;
        }

        [HttpPost]
        public async Task<IActionResult> OptimizeRoute(OptimizedRouteRequest request)
        {
            _optimizedRouteRequestValidator.Validate(request);
            var locationInfo = await _routeService.GetOptimizedRouteAsync(request.StartLocation, request.LocationsToVisit);
            return Ok(locationInfo);
        }
    }
}
