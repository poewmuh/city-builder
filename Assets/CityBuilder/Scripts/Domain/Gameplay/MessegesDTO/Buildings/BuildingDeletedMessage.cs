using CityBuilder.Domain.Gameplay.Models;

namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public readonly struct BuildingDeletedMessage
    {
        public readonly int BuildingId;
        public readonly GridPosition GridPosition;

        public BuildingDeletedMessage(int buildingId, GridPosition gridPosition)
        {
            BuildingId = buildingId;
            GridPosition = gridPosition;
        }
    }
}