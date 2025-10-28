using CityBuilder.Domain.Gameplay.Models.Buildings;
using CityBuilder.Domain.Gameplay.Models.Buildings.Base;
using TriInspector;
using UnityEngine;

namespace CityBuilder.Repositories.Gameplay
{
    [CreateAssetMenu(fileName = "BuildingConfig", menuName = "Data/BuildingConfig")]
    public class BuildingConfig : ScriptableObject
    {
        [Title("Build Info")]
        [SerializeField] private BuildingType buildingType;
        [SerializeField, Min(1)] private int level = 1;
        [SerializeField] private string displayName;
        
        [Title("Economics")]
        [SerializeField, Min(0)] private int buildCost = 10;
        [SerializeField, Min(0)] private int income = 10;
        
        [Title("Visuals")]
        [SerializeField] private GameObject prefab;
        [SerializeField, PreviewObject] private Sprite icon;
        
        public BuildingType BuildingType => buildingType;
        public int BuildingLevel => level;
        public string Name => displayName;
        public int BuildCost => buildCost;
        public int BuildIncome => income;
        public GameObject Prefab => prefab;
        public Sprite Icon => icon;
        
        public BuildingData ToDomainModel()
        {
            return new BuildingData(
                type: buildingType,
                level: new BuildingLevel(level),
                buildCost: new Cost(buildCost),
                income: new Income(income),
                name: displayName
            );
        }
    }
}