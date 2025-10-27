using UnityEngine;

namespace CityBuilder.Domain.Gameplay.Models
{
    public readonly struct Cost
    {
        public readonly int Gold;

        public Cost(int gold)
        {
            if (gold < 0)
            {
                Debug.LogError($"[Cost.Cost] Negative value for cost: {gold}");
            }

            Gold = gold;
        }
    }
}
