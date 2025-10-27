namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public class DeleteBuildingCommand
    {
        public readonly int BuildingId;
        
        public DeleteBuildingCommand(int buildingId)
        {
            BuildingId = buildingId;
        }
    }
}