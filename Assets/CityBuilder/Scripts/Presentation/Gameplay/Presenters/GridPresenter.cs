using System;
using CityBuilder.Domain.Gameplay.MessagesDTO;
using CityBuilder.Domain.Gameplay.MessagesDTO.Grid;
using CityBuilder.Domain.Gameplay.Models;
using CityBuilder.Domain.Gameplay.Models.Buildings;
using CityBuilder.Domain.Gameplay.Models.Grid;
using CityBuilder.Infastructure.Input;
using CityBuilder.Presentation.Gameplay.Views;
using CityBuilder.Repositories.Gameplay;
using MessagePipe;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CityBuilder.Presentation.Gameplay.Presenters
{
    public class GridPresenter : IInitializable, IDisposable, ITickable
    {
        [Inject] private readonly GridConfig _gridConfig;
        [Inject] private readonly GridSystem _gridSystem;
        [Inject] private readonly GridView _gridView;
        [Inject] private readonly BuildingsSystem _buildingsSystem;
        [Inject] private readonly InputService _inputService;

        [Inject] private readonly IPublisher<GridHoveredMessage> _gridHoveredPub;
        [Inject] private readonly IPublisher<GridClickedMessage> _gridClickedPub;
        [Inject] private readonly ISubscriber<ClickActionInputMessage> _clickActionSub;

        private readonly CompositeDisposable _disposables = new();
        private Camera _camera;

        public void Initialize()
        {
            _camera = Camera.main;
            
            _gridView.Initialize(_gridConfig);
            _gridView.DrawGrid();
            
            _clickActionSub.Subscribe(OnClicked).AddTo(_disposables);
        }

        public void Tick()
        {
            UpdateHighlight();
        }
        
        private void UpdateHighlight()
        {
            var gridPos = GetMouseGridPos();

            if (_gridSystem.IsPositionValid(gridPos))
            {
                var isValid = !_buildingsSystem.IsPosOccupied(gridPos);

                var cellWorldPos = _gridSystem.GetWorldPosition(gridPos);
                _gridView.ShowHighlight(cellWorldPos, isValid);
                return;
            }

            _gridView.HideHighlight();
        }
        
        private void OnClicked(ClickActionInputMessage msg)
        {
            var gridPos = GetMouseGridPos();

            if (_gridSystem.IsPositionValid(gridPos))
            {
                _gridClickedPub.Publish(new GridClickedMessage(gridPos));
            }
        }
        
        private Vector3 GetMouseScreenWorldPos()
        {
            var screenPoint = new Vector3(_inputService.MousePosition.x, _inputService.MousePosition.y,
                -_camera.transform.position.z);
            var worldPos = _camera.ScreenToWorldPoint(screenPoint);
            worldPos.z = 0;
            
            return worldPos;
        }

        private GridPosition GetMouseGridPos()
        {
            return _gridSystem.WorldToGridPosition(GetMouseScreenWorldPos());
        }

        public void Dispose()
        {
            _disposables?.Dispose();
            _gridView?.HideHighlight();
        }
    }
}