using UnityEngine;

namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public readonly struct BuildingDeletedMessage
    {
        public readonly int BuildingId;
        public readonly Vector3Int GridPosition;

        public BuildingDeletedMessage(int buildingId, Vector3Int gridPosition)
        {
            BuildingId = buildingId;
            GridPosition = gridPosition;
        }
    }
}