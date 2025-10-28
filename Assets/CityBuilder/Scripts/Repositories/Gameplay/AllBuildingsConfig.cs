using System.Collections.Generic;
using CityBuilder.Domain.Gameplay.Models.Buildings;
using CityBuilder.Domain.Gameplay.Models.Buildings.Base;
using UnityEngine;

namespace CityBuilder.Repositories.Gameplay
{
    [CreateAssetMenu(fileName = "AllBuildingsConfig", menuName = "Data/AllBuildingsConfig")]
    public class AllBuildingsConfig : ScriptableObject, IBuildingsDataConfig
    {
        [SerializeField] private List<BuildingConfig> _allBuildingsConfig;
        
        private Dictionary<(BuildingType, int), BuildingData> _dataCache;

        public BuildingData GetBuildingData(BuildingType type, BuildingLevel level)
        {
            InitializeCache();

            if (_dataCache.TryGetValue((type, level.Level), out var data))
                return data;

            Debug.LogWarning($"BuildingData not found for {type} Level {level}");
            return null;
        }
        
        public bool HasNextLevel(BuildingType type, BuildingLevel currentLevel)
        {
            InitializeCache();
            return _dataCache.ContainsKey((type, currentLevel.Level + 1));
        }
        
        public List<BuildingType> GetTypesForFirstBuild()
        {
            var types = new List<BuildingType>();

            foreach (var building in _allBuildingsConfig)
            {
                if (building.BuildingLevel == 1)
                {
                    types.Add(building.BuildingType);
                }
            }

            return types;
        }
        
        public Sprite GetBuildingIcon(BuildingType type, int level)
        {
            foreach (var config in _allBuildingsConfig)
            {
                if (config != null && config.BuildingType == type && config.BuildingLevel == level)
                {
                    return config.Icon;
                }
            }

            return null;
        }

        private void InitializeCache()
        {
            if (_dataCache != null) return;

            _dataCache = new ();

            foreach (var config in _allBuildingsConfig)
            {
                if (config == null)
                {
                    Debug.LogError("[AllBuildingsConfig.InitializeCache] Null BuildingConfig found in _allBuildingsConfig");
                    continue;
                }

                var key = (config.BuildingType, config.BuildingLevel);
                _dataCache[key] = config.ToDomainModel();
            }
        }
    }
}
