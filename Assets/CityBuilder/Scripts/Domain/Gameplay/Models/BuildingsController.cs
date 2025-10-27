using System.Collections.Generic;
using CityBuilder.Domain.Gameplay.Models.Buildings;
using CityBuilder.Domain.Gameplay.Models.Buildings.Base;

namespace CityBuilder.Domain.Gameplay.Models
{
    public class BuildingsController
    {
        private readonly Dictionary<int, Building> _buildings = new();
        
        public IReadOnlyCollection<Building> AllBuildings => _buildings.Values;
        
        public Building AddNewBuilding(BuildingType type, BuildingLevel level, GridPosition position, int rotation = 0)
        {
            int newId = _buildings.Count;
            var newBuilding = new Building(newId, type, level, position, rotation);
            _buildings[newBuilding.Id] = newBuilding;
            
            return newBuilding;
        }
        
        public bool RemoveBuilding(int buildingId)
        {
            return _buildings.Remove(buildingId);
        }
        
        public Building GetBuilding(int buildingId)
        {
            return _buildings.GetValueOrDefault(buildingId);
        }
        
        public bool TryGetBuildingAt(GridPosition position, out Building building)
        {
            building = null;
            
            foreach (var build in AllBuildings)
            {
                if (build.Position == position)
                {
                    building = build;
                    return true;
                }
            }

            return false;
        }
        
        public bool IsPosOccupied(GridPosition position)
        {
            foreach (var building in AllBuildings)
            {
                if (building.Position == position) return true;
            }

            return false;
        }

        public int GetTotalIncome(IBuildingsDataConfig dataConfig)
        {
            int total = 0;
            foreach (var building in AllBuildings)
            {
                var data = dataConfig.GetBuildingData(building.BuildingType, building.BuildingLevel);
                if (data != null)
                {
                    total += data.BuildIncome.GoldPerMinute;
                }
            }

            return total;
        }

        public void Clear()
        {
            _buildings.Clear();
        }
    }
}