using System.Collections.Generic;

namespace BusinessLogic.DataModels
{
    public class Segment
    {
        public List<Coordinate> Coordinates { get; set; }

        public string FromAddress { get; set; }

        public string ToAddress { get; set; }
    }
}