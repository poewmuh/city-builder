using CityBuilder.Repositories.Gameplay;
using UnityEngine;

namespace CityBuilder.Presentation.Gameplay.Views
{
    public class GridView : MonoBehaviour
    {
        private GridConfig _config;
        private GameObject _highlight;
        private SpriteRenderer _highlightRenderer;
        
        public void Initialize(GridConfig config)
        {
            _config = config;
            
            if (_config.HighlightPrefab != null)
            {
                _highlight = Instantiate(_config.HighlightPrefab, transform);
                _highlight.transform.localScale = Vector3.one * _config.CellSize;
                _highlightRenderer = _highlight.GetComponent<SpriteRenderer>();
                _highlight.SetActive(false);
            }
        }

        public void DrawGrid()
        {
            
        }

        public void ShowHighlight(Vector3 position, bool isValid)
        {
            if (!_highlight) return;
            
            _highlight.transform.position = position;
            _highlightRenderer.color = isValid ? _config.ValidColor : _config.InvalidColor;
            _highlight.SetActive(true);
        }
        
        public void HideHighlight()
        {
            if (!_highlight) return;
            
            _highlight.SetActive(false);
        }
        
        private void OnDrawGizmos()
        {
            if (!_config) return;
            
            Gizmos.color = _config.GridColor * 0.3f;

            for (int x = 0; x <= _config.GridWidth; x++)
            {
                Vector3 start = new Vector3(x * _config.CellSize, 0, 0);
                Vector3 end = new Vector3(x * _config.CellSize, _config.GridHeight * _config.CellSize, 0);
                Gizmos.DrawLine(start, end);
            }

            for (int y = 0; y <= _config.GridHeight; y++)
            {
                Vector3 start = new Vector3(0, y * _config.CellSize, 0);
                Vector3 end = new Vector3(_config.GridWidth * _config.CellSize, y * _config.CellSize, 0);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}