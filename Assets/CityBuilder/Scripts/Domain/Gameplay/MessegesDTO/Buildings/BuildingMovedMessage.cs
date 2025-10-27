using UnityEngine;

namespace CityBuilder.Domain.Gameplay.MessagesDTO.Buildings
{
    public readonly struct BuildingMovedMessage
    {
        public readonly int BuildingId;
        public readonly Vector3Int FromPosition;
        public readonly Vector3Int ToPosition;

        public BuildingMovedMessage(int buildingId, Vector3Int fromPosition, Vector3Int toPosition)
        {
            BuildingId = buildingId;
            FromPosition = fromPosition;
            ToPosition = toPosition;
        }
    }
}
