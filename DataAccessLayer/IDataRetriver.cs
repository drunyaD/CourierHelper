using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDataRetriver
    {
        public Task<TResponse> GetDataAsync<TResponse>(string uri);
    }
}
