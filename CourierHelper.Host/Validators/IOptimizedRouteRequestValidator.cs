using CourierHelper.Host.Models;

namespace CourierHelper.Host.Validators
{
    public interface IOptimizedRouteRequestValidator
    {
        public void Validate(OptimizedRouteRequest optimizedRouteRequest);
    }
}
