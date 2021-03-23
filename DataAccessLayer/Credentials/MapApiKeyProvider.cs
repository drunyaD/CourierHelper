namespace DataAccessLayer.Credentials
{
    public class MapApiKeyProvider : IMapApiKeyProvider
    {
        private readonly string _key;

        public MapApiKeyProvider(string key)
        {
            _key = key;
        }

        public string GetKey()
        {
            return _key;
        }
    }
}
