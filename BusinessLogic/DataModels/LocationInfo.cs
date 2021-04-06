namespace BusinessLogic.DataModels
{
    public class LocationInfo : IDistancable<LocationInfo>
    {
        public string FormattedAddress { get; set; }

        public Coordinate Coordinate { get; set; }

        public string PlaceId { get; set; }

        public double GetDistance(LocationInfo secondPoint)
        {
            return Coordinate.GetDistance(secondPoint.Coordinate);
        }
    }
}
