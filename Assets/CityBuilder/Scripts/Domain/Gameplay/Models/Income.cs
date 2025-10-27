using UnityEngine;

namespace CityBuilder.Domain.Gameplay.Models
{
    public readonly struct Income
    {
        public readonly int GoldPerMinute;

        public Income(int incomeValue)
        {
            if (incomeValue < 0)
            {
                Debug.LogError($"[Income.Income] Negative value for income: {incomeValue}");
            }

            GoldPerMinute = incomeValue;
        }
    }
}