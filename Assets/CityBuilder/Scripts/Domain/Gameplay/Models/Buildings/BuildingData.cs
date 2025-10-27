using CityBuilder.Domain.Gameplay.Models.Buildings.Base;

namespace CityBuilder.Domain.Gameplay.Models.Buildings
{
    /// <summary>
    /// Данные о здании
    /// </summary>
    public class BuildingData
    {
        public string Name;
        
        public BuildingType BuildingType { get; }
        public BuildingLevel BuildingLevel { get; }
        public Cost BuildCost { get; }
        public Income BuildIncome { get; }

        public BuildingData(BuildingType type, BuildingLevel level, Cost buildCost, Income income, string name)
        {
            BuildingType = type;
            BuildingLevel = level;
            BuildCost = buildCost;
            BuildIncome = income;
            Name = name;
        }
    }
}