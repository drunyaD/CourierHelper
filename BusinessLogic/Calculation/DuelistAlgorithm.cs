using BusinessLogic.DataModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BusinessLogic.Calculation
{
    public class DuelistAlgorithm<TPoint> where TPoint : IDistancable<TPoint>
    {
        private int _fixedPoints;
        private List<TPoint> _allSkills;      
        private Dictionary<List<int>, double> _duelistsPoolToStrength = new();
        private readonly Random _random = new();
        private int _n;
        private int _duelistsCount;
        private int _championCount;
        private double _luckCoef;
        private int _maxIterationCount;
        private int _maxIterationStrengthNotChanged;


        public DuelistAlgorithm(List<TPoint> allSkills, AlgorithmConfig config)
        {
            _allSkills = allSkills;
            _n = allSkills.Count;
            _duelistsCount = config.DuelistsCount;
            _championCount = config.ChamptionCount;
            _luckCoef = config.LuckCoef;
            _maxIterationCount = config.MaxIterationCount;
            _maxIterationStrengthNotChanged = config.MaxStrengthNotChangedIterationCount;
            _fixedPoints = config.FixedPoints;
        }

        public Dictionary<List<int>, double> Run()
        {
            int iterationStrengthNotChanged = 0;
            double maxStrength = 0;
            Registration();
            CalculateStrength();
            for (int i = 0; i < _maxIterationCount; i++)
            {
                Duel();
                CalculateStrength();
                CloneChampions();
                RemoveWorstDuelists();
                double currentMaxStrength = _duelistsPoolToStrength.Values.First();
                if (maxStrength == currentMaxStrength)
                {
                    iterationStrengthNotChanged++;
                    if (iterationStrengthNotChanged == _maxIterationStrengthNotChanged)
                    {
                        break;
                    }
                }
                else
                {
                    iterationStrengthNotChanged = 0;
                    maxStrength = currentMaxStrength;
                }

            }

            return _duelistsPoolToStrength;
        }

        private void Registration()
        {
            for (int i = 0; i < _duelistsCount; i++)
            {
                var duelist = new List<int>();
                // First point should not be changed
                duelist.Add(0);
                var remainingSkills = Enumerable.Range(1, _n - 1).ToList();
                while (remainingSkills.Count != 0)
                {
                    int skillNumber = _random.Next(remainingSkills.Count);
                    duelist.Add(remainingSkills[skillNumber]);
                    remainingSkills.RemoveAt(skillNumber);
                }
                _duelistsPoolToStrength.Add(duelist, 0);
            }
        }
        
        private void CalculateStrength()
        {
            foreach (var duelistToStrength in _duelistsPoolToStrength)
            {
                var duelist = duelistToStrength.Key;
                double distance = 0;
                for (int i = 0; i < duelist.Count - 1; i++)
                {
                    distance += _allSkills[duelist[i]].GetDistance(_allSkills[duelist[i+1]]);
                }

                _duelistsPoolToStrength[duelist] = 1 / distance;
            }

            SortDuelistsByValue();
        }

        private void SortDuelistsByValue()
        {
            var _duelistsPoolToStrengthList = _duelistsPoolToStrength.ToList();
            _duelistsPoolToStrengthList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            _duelistsPoolToStrength = _duelistsPoolToStrengthList.ToDictionary(x => x.Key, x => x.Value);
        }

        private void Duel()
        {
            var participants = _duelistsPoolToStrength
                .Select(x => x.Key)
                .Skip(_championCount)
                .ToList();

            while (participants.Count != 0)
            {
                var duelists = _duelistsPoolToStrength.Keys.ToList();

                var firstDuelistNumber = _random.Next(participants.Count);              
                var firstDuelist = participants[firstDuelistNumber];
                participants.RemoveAt(firstDuelistNumber);

                var secondDuelistNumber = _random.Next(participants.Count);
                var secondDuelist = participants[secondDuelistNumber];
                participants.RemoveAt(secondDuelistNumber);

                var firstDuelistStrength = _duelistsPoolToStrength[firstDuelist];
                var secondDuelistStrength = _duelistsPoolToStrength[secondDuelist];
                var firstDuelistLuck = _random.Next(2) * _luckCoef * firstDuelistStrength;
                var secondDuelistLuck = _random.Next(2) * _luckCoef * secondDuelistStrength;

                List<int> winner;
                List<int> looser;

                if (firstDuelistStrength + firstDuelistLuck > secondDuelistStrength + secondDuelistLuck)
                {
                    winner = firstDuelist;
                    looser = secondDuelist;
                }
                else
                {
                    winner = secondDuelist;
                    looser = firstDuelist;
                }

                var newWinner = ImproveWinner(winner);
                var newLooser = ImproveLooser(winner, looser);

                _duelistsPoolToStrength.Remove(winner);
                _duelistsPoolToStrength.Remove(looser);
                _duelistsPoolToStrength.Add(newWinner, 0);
                _duelistsPoolToStrength.Add(newLooser, 0);

            }
        }

        private void CloneChampions()
        {
            var duelists = _duelistsPoolToStrength.Keys.ToList();

            for (int i = 0; i < _championCount; i++)
            {
                var champion = duelists[i];
                var championClone = new List<int>(champion);
                _duelistsPoolToStrength.Add(championClone, _duelistsPoolToStrength[champion]);
            }

            SortDuelistsByValue();
        }

        private void RemoveWorstDuelists()
        {
            var duelists = _duelistsPoolToStrength.Keys.ToList();

            for (int i = _duelistsCount; i < _duelistsCount + _championCount; i++)
            {
                _duelistsPoolToStrength.Remove(duelists[i]);
            }
        }

        private List<int> ImproveWinner(List<int> winner)
        {
            var newWinner = new List<int>(winner); 
            // Winner is trying new thing (swap two skills)
            var availableSkills = new List<int>(newWinner);
            var firstSkillIndex = _random.Next(1, _n);
            var firstSkill = availableSkills[firstSkillIndex];

            availableSkills.RemoveAt(firstSkillIndex);

            var secondSkillIndex = _random.Next(1, _n - 1);
            var secondSkill = availableSkills[secondSkillIndex];

            newWinner[winner.IndexOf(firstSkill)] = secondSkill;
            newWinner[winner.IndexOf(secondSkill)] = firstSkill;

            return newWinner;
        }

        private List<int> ImproveLooser(List<int> winner, List<int> looser)
        {
            // Looser is taking the second part of the skillset from the order in winner
            // https://cad.kpi.ua/attachments/141_2012_004s.pdf (orderly crosses)

            int partToModify = _random.Next(2);
            int firstPartCount = _n / 2;
            var newLooser = new List<int>();

            if (partToModify == 0)
            {
                newLooser.Add(0);
                int secondPartCount = _n - firstPartCount;              
                var secondPart = looser.Skip(firstPartCount).Take(secondPartCount);

                var indexInWinnerToSkill = new SortedDictionary<int, int>();
                for (int i = 1; i < firstPartCount; i++)
                {
                    var skill = looser[i];
                    var indexInWinner = winner.IndexOf(skill);
                    indexInWinnerToSkill.Add(indexInWinner, skill);
                }
                foreach (var indexToSkill in indexInWinnerToSkill)
                {
                    newLooser.Add(indexToSkill.Value);
                }
                newLooser.AddRange(secondPart);
            }
            else
            {               
                newLooser.AddRange(looser.Take(firstPartCount));

                var indexInWinnerToSkill = new SortedDictionary<int, int>();
                for (int i = firstPartCount; i < _n; i++)
                {
                    var skill = looser[i];
                    var indexInWinner = winner.IndexOf(skill);
                    indexInWinnerToSkill.Add(indexInWinner, skill);
                }

                foreach (var indexToSkill in indexInWinnerToSkill)
                {
                    newLooser.Add(indexToSkill.Value);
                }             
            }

            return newLooser;
        }
    }
}