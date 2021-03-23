using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace DataAccessLayer.DataModels
{
    public class GeocodeResponse
    {
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GeocodeStatusCode Status { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }
}
