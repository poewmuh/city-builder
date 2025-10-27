namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public class BuildingOperationFailedMessage
    {
        public readonly BuildingOperationType OperationType;
        public readonly string ReasonMessage;
        public readonly int BuildingId;
        
        public BuildingOperationFailedMessage(BuildingOperationType operationType, string reasonMessage, int buildingId = 0)
        {
            OperationType = operationType;
            ReasonMessage = reasonMessage;
            BuildingId = buildingId;
        }
    }
}
