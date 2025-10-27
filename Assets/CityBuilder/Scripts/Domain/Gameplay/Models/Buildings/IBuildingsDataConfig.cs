using System.Collections.Generic;
using CityBuilder.Domain.Gameplay.Models.Buildings.Base;

namespace CityBuilder.Domain.Gameplay.Models.Buildings
{
    public interface IBuildingsDataConfig // TODO: По идее Contracts Interfaces пока тут
    {
        BuildingData GetBuildingData(BuildingType type, BuildingLevel level);
        bool HasNextLevel(BuildingType type, BuildingLevel currentLevel);
        List<BuildingType> GetTypesForFirstBuild();
    }
}