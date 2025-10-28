namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public class UpgradeBuildingCommand
    {
        public readonly int BuildingId;

        public UpgradeBuildingCommand(int buildingId)
        {
            BuildingId = buildingId;
        }
    }
}