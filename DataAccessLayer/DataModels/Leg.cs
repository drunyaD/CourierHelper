using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataAccessLayer.DataModels
{
    public class Leg
    {
        [JsonProperty("steps")]
        public List<Step> Steps { get; set; }
    }
}
