using CityBuilder.Domain.Gameplay.Models.Buildings.Base;
using UnityEngine;

namespace CityBuilder.Presentation.Gameplay.Views
{
    public class BuildingView : MonoBehaviour
    {
        public int BuildingId { get; private set; }
        public BuildingType BuildingType { get; private set; }
        private SpriteRenderer _spriteRenderer;

        public void Initialize(int buildingId, BuildingType buildingType, Sprite sprite, Vector3 position, float cellSize)
        {
            BuildingId = buildingId;
            BuildingType = buildingType;

            if (!_spriteRenderer)
            {
                _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            }

            _spriteRenderer.sprite = sprite;
            transform.position = position;
            transform.localScale = new Vector3(cellSize / 2, cellSize / 2, 1f);
        }

        public void UpdatePosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }
        
        public void UpdateSprite(Sprite newSprite)
        {
            _spriteRenderer.sprite = newSprite;
        }

        public void SetHighlight(bool highlighted)
        {
            _spriteRenderer.color = highlighted ? Color.yellow : Color.white;
        }
    }
}
