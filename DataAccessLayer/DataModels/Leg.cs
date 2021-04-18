using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataAccessLayer.DataModels
{
    public class Leg
    {
        [JsonProperty("steps")]
        public List<Step> Steps { get; set; }

        [JsonProperty("start_address")]
        public string StartAddress { get; set; }

        [JsonProperty("end_address")]
        public string EndAddress { get; set; }
    }
}
