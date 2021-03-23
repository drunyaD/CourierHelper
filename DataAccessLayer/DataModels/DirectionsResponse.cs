using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace DataAccessLayer.DataModels
{
    public class DirectionsResponse
    {
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public DirectionsStatusCode Status { get; set; }

        [JsonProperty("routes")]
        public List<Route> Routes { get; set; }
    }
}
