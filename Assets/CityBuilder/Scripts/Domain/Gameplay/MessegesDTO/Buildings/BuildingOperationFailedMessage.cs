using UnityEngine;

namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public class BuildingOperationFailedMessage : MonoBehaviour
    {
        public readonly BuildingOperationType OperationType;
        public readonly string ReasonMessage;
        public readonly int BuildingId;
        
        public BuildingOperationFailedMessage(BuildingOperationType operationType, string reasonMessage, int buildingId)
        {
            OperationType = operationType;
            ReasonMessage = reasonMessage;
            BuildingId = buildingId;
        }
    }
}
