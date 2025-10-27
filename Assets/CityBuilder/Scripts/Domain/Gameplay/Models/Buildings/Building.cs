using CityBuilder.Domain.Gameplay.Models.Buildings.Base;

namespace CityBuilder.Domain.Gameplay.Models.Buildings
{
    /// <summary>
    /// Здание в игре
    /// </summary>
    public class Building
    {
        public int Id { get; }
        
        public BuildingType BuildingType { get; }
        public BuildingLevel BuildingLevel { get; private set; }
        public GridPosition Position { get; private set; }
        public int Rotation { get; private set; }

        public Building(int id, BuildingType buildingType, BuildingLevel buildingLevel, GridPosition position,
            int rotation = 0)
        {
            Id = id;
            BuildingType = buildingType;
            BuildingLevel = buildingLevel;
            Position = position;
            Rotation = rotation % 360;
        }
        
        public void SetPosition(GridPosition newPosition)
        {
            Position = newPosition;
        }
        
        public void LevelUp()
        {
            BuildingLevel = BuildingLevel.LevelUp();
        }
        
        public void SetRotation(int newRotation)
        {
            Rotation = newRotation % 360;
        }
    }
}