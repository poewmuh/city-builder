namespace CityBuilder.Domain.Gameplay.MessagesDTO
{
    public readonly struct BuildingPrefabSelectedMessage
    {
        public readonly int PrefabIndex;

        public BuildingPrefabSelectedMessage(int prefabIndex)
        {
            PrefabIndex = prefabIndex;
        }
    }
    
    public readonly struct RotateBuildingMessage { }
    
    public readonly struct DeleteBuildingMessage { }
}
