namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public readonly struct BuildingUpgradedMessage
    {
        public readonly int BuildingId;
        public readonly int NewLevel;

        public BuildingUpgradedMessage(int buildingId, int newLevel)
        {
            BuildingId = buildingId;
            NewLevel = newLevel;
        }
    }
}