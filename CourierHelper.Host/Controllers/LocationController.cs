using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourierHelper.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInfo(string searchString)
        {
            var locationInfo = await _locationService.GetLocationInfoAsync(searchString);
            return Ok(locationInfo);
        }
    }
}
