using System.Collections.Generic;

namespace BusinessLogic.DataModels
{
    public class OptimizedRoute
    {
        public List<Segment> Segments { get; set; }

        public string Polyline { get; set; }
    }
}
