using UnityEngine;

namespace CityBuilder.Domain.Gameplay.Models
{
    public readonly struct BuildingLevel
    {
        public readonly int Level;

        public BuildingLevel(int newLevel)
        {
            if (newLevel <= 0)
            {
                Debug.LogError($"[BuildingLevel.BuildingLevel] Invalid level: {newLevel}");
            }
            
            Level = newLevel;
        }
        
        public BuildingLevel LevelUp() => new (Level + 1);
    }
}