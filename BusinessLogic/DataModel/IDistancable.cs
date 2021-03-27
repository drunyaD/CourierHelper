namespace BusinessLogic.DataModel
{
    public interface IDistancable<TPoint>
    {
        public double GetDistance(TPoint secondPoint);
    }
}
