namespace BusinessLogic.DataModels
{
    public interface IDistancable<TPoint>
    {
        public double GetDistance(TPoint secondPoint);
    }
}
