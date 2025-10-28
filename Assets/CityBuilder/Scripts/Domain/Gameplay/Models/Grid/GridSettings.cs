namespace CityBuilder.Domain.Gameplay.Models.Grid
{
    public readonly struct GridSettings
    {
        public readonly int Width;
        public readonly int Height;
        public readonly float CellSize;

        public GridSettings(int width, int height, float cellSize = 1f)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
        }
    }
}
