using System.Collections.Generic;
using CityBuilder.Domain.Gameplay.Models.Buildings.Base;
using UnityEngine;

namespace CityBuilder.Domain.Gameplay.Models.Buildings
{
    public interface IBuildingsDataConfig // TODO: По идее Contracts Interfaces пока тут
    {
        BuildingData GetBuildingData(BuildingType type, BuildingLevel level);
        bool HasNextLevel(BuildingType type, BuildingLevel currentLevel);
        List<BuildingType> GetTypesForFirstBuild();
        public Sprite GetBuildingIcon(BuildingType type, int level);
    }
}