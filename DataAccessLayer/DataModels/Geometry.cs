using Newtonsoft.Json;

namespace DataAccessLayer.DataModels
{
    public class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
    }
}
