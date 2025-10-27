using CityBuilder.Domain.Gameplay.Models.Buildings.Base;

namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public readonly struct BuildingUpgradedMessage
    {
        public readonly int BuildingId;
        public readonly BuildingLevel NewLevel;

        public BuildingUpgradedMessage(int buildingId, BuildingLevel newLevel)
        {
            BuildingId = buildingId;
            NewLevel = newLevel;
        }
    }
}