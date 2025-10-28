namespace CityBuilder.Domain.Gameplay.MessagesDTO
{
    public readonly struct BuildingPrefabSelectedInputMessage
    {
        public readonly int PrefabIndex;

        public BuildingPrefabSelectedInputMessage(int prefabIndex)
        {
            PrefabIndex = prefabIndex;
        }
    }
    
    public readonly struct RotateBuildingInputMessage { }
    
    public readonly struct DeleteBuildingInputMessage { }
    
    public readonly struct ClickActionInputMessage { }
}
