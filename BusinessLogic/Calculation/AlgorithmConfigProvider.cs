using System;

namespace BusinessLogic.Calculation
{
    public class AlgorithmConfigProvider : IAlgorithmConfigProvider
    {
        public AlgorithmConfig ProvideConfig(int pointsCount)
        {
            int duelistsCount = (int)Math.Sqrt(pointsCount) + 2;
            int championCount = duelistsCount / 5;
            if ((duelistsCount - championCount) % 2 != 0)
            {
                championCount++;
            }
            double luckCoef = 0.2;
            int iterationCount = 1000;
            int strengthNotChangeIterCount = 100;

            return new AlgorithmConfig
            {
                DuelistsCount = (int)Math.Sqrt(pointsCount) + 2,
                ChamptionCount = championCount,
                LuckCoef = luckCoef,
                MaxIterationCount = iterationCount,
                MaxStrengthNotChangedIterationCount = strengthNotChangeIterCount
            };
        }
    }
}
