using CityBuilder.Domain.Gameplay.Models;

namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public readonly struct BuildingMovedMessage
    {
        public readonly int BuildingId;
        public readonly GridPosition FromPosition;
        public readonly GridPosition ToPosition;

        public BuildingMovedMessage(int buildingId, GridPosition fromPosition, GridPosition toPosition)
        {
            BuildingId = buildingId;
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }
    }
}
