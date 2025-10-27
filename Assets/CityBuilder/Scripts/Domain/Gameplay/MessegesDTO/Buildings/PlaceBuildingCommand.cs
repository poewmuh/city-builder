using CityBuilder.Domain.Gameplay.Models;
using CityBuilder.Domain.Gameplay.Models.Buildings.Base;

namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public class PlaceBuildingCommand
    {
        public readonly BuildingType BuildingType;
        public readonly GridPosition Position;
        public readonly int Rotation;

        public PlaceBuildingCommand(BuildingType buildingType, GridPosition position, int rotation)
        {
            BuildingType = buildingType;
            Position = position;
            Rotation = rotation;
        }
    }
}