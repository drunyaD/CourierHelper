using BusinessLogic.DataModels;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface ILocationService
    {
        public Task<LocationInfo> GetLocationInfoAsync(string searchString);
    }
}
