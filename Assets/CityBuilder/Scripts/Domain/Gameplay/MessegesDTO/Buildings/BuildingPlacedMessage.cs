using UnityEngine;

namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public readonly struct BuildingPlacedMessage
    {
        public readonly int BuildingId;
        public readonly Vector3Int GridPosition;

        public BuildingPlacedMessage(int buildingId, Vector3Int gridPosition)
        {
            BuildingId = buildingId;
            GridPosition = gridPosition;
        }
    }
}
