using TriInspector;
using UnityEngine;

namespace CityBuilder.Repositories.Gameplay
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Data/GridConfig")]
    public class GridConfig : ScriptableObject
    {
        [Title("Grid Settings")]
        [SerializeField, Min(1)] private int gridWidth = 32;
        [SerializeField, Min(1)] private int gridHeight = 32;
        [SerializeField, Min(0.1f)] private float cellSize = 1;
        [SerializeField] private Color gridColor = Color.white;
        [SerializeField] private Color validColor = Color.green;
        [SerializeField] private Color invalidColor = Color.red;
        [SerializeField] private GameObject highlightPrefab;
        
        public int GridWidth => gridWidth;
        public int GridHeight => gridHeight;
        public float CellSize => cellSize;
        public Color GridColor => gridColor;
        public Color ValidColor => validColor;
        public Color InvalidColor => invalidColor;
        public GameObject HighlightPrefab => highlightPrefab;
    }
}