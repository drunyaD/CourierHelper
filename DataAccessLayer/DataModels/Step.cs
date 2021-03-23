using Newtonsoft.Json;

namespace DataAccessLayer.DataModels
{
    public class Step
    {
        [JsonProperty("start_location")]
        public Location StartLocation { get; set; }

        [JsonProperty("end_location")]
        public Location EndLocation { get; set; }
    }
}