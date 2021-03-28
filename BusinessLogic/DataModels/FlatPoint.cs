using System;

namespace BusinessLogic.DataModels
{
    public class FlatPoint : IDistancable<FlatPoint>
    {
        public int X { get; set; }

        public int Y { get; set; }

        public double GetDistance(FlatPoint secondPoint)
        {
            var distX = secondPoint.X - X;
            var distY = secondPoint.Y - Y;

            var result = Math.Sqrt(distX * distX + distY * distY);

            return result;
        }
    }
}
