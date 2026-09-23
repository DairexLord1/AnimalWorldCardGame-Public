using System;
using System.Collections.Generic;

namespace AnimalWorld.Progression
{
    public class PointsService
    {
        private static readonly int[] RewardValues = { 25, 50, 75, 100, 150 };

        public event Action BalanceChanged;

        public int Balance { get; private set; }

        public IReadOnlyList<int> Rewards => RewardValues;

        public int RollRewardIndex() => UnityEngine.Random.Range(0, RewardValues.Length);

        public bool CanAfford(int cost) => cost <= Balance;

        public void Add(int points)
        {
            Balance += points;
            BalanceChanged?.Invoke();
        }

        public bool TrySpend(int cost)
        {
            if (cost > Balance)
            {
                return false;
            }

            Balance -= cost;
            BalanceChanged?.Invoke();
            return true;
        }
    }
}
