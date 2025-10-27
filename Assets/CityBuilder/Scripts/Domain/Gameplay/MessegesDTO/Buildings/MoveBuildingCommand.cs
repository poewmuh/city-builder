using CityBuilder.Domain.Gameplay.Models;

namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public class MoveBuildingCommand
    {
        public readonly int BuildingId;
        public readonly GridPosition NewPosition;
        
        public MoveBuildingCommand(int buildingId, GridPosition newPosition)
        {
            BuildingId = buildingId;
            NewPosition = newPosition;
        }
    }
}
