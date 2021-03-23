using Newtonsoft.Json;

namespace DataAccessLayer.DataModels
{
    public class OverviewPolyline
    {
        [JsonProperty("points")]
        public string Points { get; set; }
    }
}
