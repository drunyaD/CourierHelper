using DataAccessLayer.Exceptions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataRetriever : IDataRetriver
    {
        public async Task<TResponse> GetDataAsync<TResponse>(string uri)
        {
            var request = WebRequest.Create(uri);
            var response = (HttpWebResponse) await request.GetResponseAsync();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new DalException($"Cannot get response from '{uri}'. Status code: '{response.StatusCode}'.");
            }

            TResponse result;
            using (var stream = response.GetResponseStream())
            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                try
                {
                    result = serializer.Deserialize<TResponse>(jsonTextReader);
                }
                catch (Exception e)
                {
                    throw new DalException($"Error while deserializing: {e.Message}");
                }
            }

            return result;
        }
    }
}
