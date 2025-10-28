using UnityEngine;

namespace CityBuilder.Domain.Gameplay.Models.Grid
{
    public class GridSystem
    {
        public GridSettings Settings { get; }

        public GridSystem(GridSettings settings)
        {
            Settings = settings;
        }

        public bool IsPositionValid(GridPosition position)
        {
            return position.X >= 0 && position.X < Settings.Width &&
                   position.Y >= 0 && position.Y < Settings.Height;
        }

        public Vector3 GetWorldPosition(GridPosition gridPosition)
        {
            var x = gridPosition.X * Settings.CellSize + Settings.CellSize * .5f;
            var y = gridPosition.Y * Settings.CellSize + Settings.CellSize * .5f;
            return new Vector3(x, y, 0f);
        }

        public GridPosition WorldToGridPosition(Vector3 worldPosition)
        {
            var x = Mathf.FloorToInt(worldPosition.x / Settings.CellSize);
            var y = Mathf.FloorToInt(worldPosition.y / Settings.CellSize);
            return new GridPosition(x, y);
        }
    }
}