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
        public GridPosition GridPosition { get; private set; }
        public int Rotation { get; private set; }

        public Building(int id, BuildingType buildingType, BuildingLevel buildingLevel, GridPosition gridPosition,
            int rotation = 0)
        {
            Id = id;
            BuildingType = buildingType;
            BuildingLevel = buildingLevel;
            GridPosition = gridPosition;
            Rotation = rotation % 360;
        }
        
        public void SetPosition(GridPosition newPosition)
        {
            GridPosition = newPosition;
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