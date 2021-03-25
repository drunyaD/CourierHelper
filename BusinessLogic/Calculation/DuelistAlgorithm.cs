﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BusinessLogic.Calculation
{
    public class DuelistAlgorithm
    {
        private List<Point> _allSkills;      
        private Dictionary<List<int>, double> _duelistsPoolToStrength = new();
        private readonly Random _random = new();
        private int _n;
        private int _duelistsCount;
        private int _championCount;
        private double _luckCoef;

        public DuelistAlgorithm(List<Point> allSkills)
        {
            _allSkills = allSkills;
            _n = allSkills.Count;
            _duelistsCount = (int) Math.Sqrt(_n) + 2;
            _championCount = _duelistsCount / 5;
            if ((_duelistsCount - _championCount) % 2 != 0)
            {
                _championCount++;
            }
            _luckCoef = 0.2;
        }

        public Dictionary<List<int>, double> Run()
        {
            Registration();
            CalculateStrength();
            for (int i = 0; i < 50; i++)
            {
                Duel();
                CalculateStrength();
                CloneChampions();
                RemoveWorstDuelists();
            }

            return _duelistsPoolToStrength;
        }

        private void Registration()
        {
            for (int i = 0; i < _duelistsCount; i++)
            {
                var duelist = new List<int>();
                var remainingSkills = Enumerable.Range(0, _n).ToList();
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
                    distance += CalculateDistance(_allSkills[duelist[i]], _allSkills[duelist[i+1]]);
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
            var firstSkillIndex = _random.Next(_n);
            var firstSkill = newWinner[firstSkillIndex];

            availableSkills.RemoveAt(firstSkillIndex);

            var secondSkillIndex = _random.Next(_n - 1);
            var secondSkill = newWinner[secondSkillIndex];

            newWinner[firstSkillIndex] = secondSkill;
            newWinner[secondSkillIndex] = firstSkill;

            return newWinner;
        }

        private List<int> ImproveLooser(List<int> winner, List<int> looser)
        {
            // Looser is taking the second part of the skillset from the order in winner
            // https://cad.kpi.ua/attachments/141_2012_004s.pdf (orderly crosses)

            int firstPartCount = _n / 2;
            var newLooser = looser.Take(firstPartCount).ToList();

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

            return newLooser;
        }

        private double CalculateDistance(Point point1, Point point2)
        {
            var distX = point2.X - point1.X;
            var distY = point2.Y - point1.Y;

            var result = Math.Sqrt(distX * distX + distY * distY);

            return result;
        }
    }
}