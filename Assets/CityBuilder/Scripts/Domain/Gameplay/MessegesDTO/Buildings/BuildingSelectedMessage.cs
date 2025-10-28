using CityBuilder.Domain.Gameplay.Models;

namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public readonly struct BuildingSelectedMessage
    {
        public readonly int BuildingId;
        public readonly GridPosition Position;

        public BuildingSelectedMessage(int buildingId, GridPosition position)
        {
            BuildingId = buildingId;
            Position = position;
        }
    }
}
