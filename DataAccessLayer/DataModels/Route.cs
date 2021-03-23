using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataAccessLayer.DataModels
{
    public class Route
    {
        [JsonProperty("overview_polyline")]
        public OverviewPolyline OverviewPolyline { get; set; }

        [JsonProperty("legs")]
        public List<Leg> Legs { get; set; }
    }
}
