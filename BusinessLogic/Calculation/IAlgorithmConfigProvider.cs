namespace BusinessLogic.Calculation
{
    public interface IAlgorithmConfigProvider
    {
        public AlgorithmConfig ProvideConfig(int pointsCount);
    }
}
