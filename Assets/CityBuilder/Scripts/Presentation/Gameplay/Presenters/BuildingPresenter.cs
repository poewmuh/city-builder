using System;
using System.Collections.Generic;
using CityBuilder.Domain.Gameplay.MessagesDTO.Buildings;
using CityBuilder.Domain.Gameplay.Models.Buildings;
using CityBuilder.Domain.Gameplay.Models.Buildings.Base;
using CityBuilder.Domain.Gameplay.Models.Grid;
using CityBuilder.Presentation.Gameplay.Views;
using CityBuilder.Repositories.Gameplay;
using MessagePipe;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CityBuilder.Presentation.Gameplay.Presenters
{
    public class BuildingPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly GridSystem _gridSystem;
        [Inject] private readonly IBuildingsDataConfig _buildingsConfig;

        [Inject] private readonly ISubscriber<BuildingPlacedMessage> _buildingPlacedSub;
        [Inject] private readonly ISubscriber<BuildingMovedMessage> _buildingMovedSub;
        [Inject] private readonly ISubscriber<BuildingDeletedMessage> _buildingDeletedSub;
        [Inject] private readonly ISubscriber<BuildingUpgradedMessage> _buildingUpgradedSub;

        private readonly Dictionary<int, BuildingView> _buildingViews = new();
        private Transform _buildingsParent;

        public void Initialize()
        {
            _buildingsParent = new GameObject("Buildings").transform;

            _buildingPlacedSub.Subscribe(OnBuildingPlaced).AddTo(_disposables);
            _buildingMovedSub.Subscribe(OnBuildingMoved).AddTo(_disposables);
            _buildingDeletedSub.Subscribe(OnBuildingDeleted).AddTo(_disposables);
            _buildingUpgradedSub.Subscribe(OnBuildingUpgraded).AddTo(_disposables);
        }

        private readonly CompositeDisposable _disposables = new();

        private void OnBuildingPlaced(BuildingPlacedMessage msg)
        {
            var buildingData = _buildingsConfig.GetBuildingData(msg.BuildingType, new BuildingLevel(1));
            if (buildingData == null)
            {
                Debug.LogError($"[BuildingPresenter.OnBuildingPlaced] BuildingData not found for {msg.BuildingType}");
                return;
            }

            var buildingGO = new GameObject($"Building_{msg.BuildingId}_{msg.BuildingType}");
            buildingGO.transform.SetParent(_buildingsParent);

            var view = buildingGO.AddComponent<BuildingView>();

            var worldPos = _gridSystem.GetWorldPosition(msg.Position);

            var sprite = _buildingsConfig.GetBuildingIcon(buildingData.BuildingType, buildingData.BuildingLevel.Level);
            view.Initialize(msg.BuildingId, msg.BuildingType, sprite, worldPos, _gridSystem.Settings.CellSize);

            _buildingViews[msg.BuildingId] = view;
        }

        private void OnBuildingMoved(BuildingMovedMessage msg)
        {
            if (_buildingViews.TryGetValue(msg.BuildingId, out var view))
            {
                var newWorldPos = _gridSystem.GetWorldPosition(msg.ToPosition);
                view.UpdatePosition(newWorldPos);
            }
        }

        private void OnBuildingDeleted(BuildingDeletedMessage msg)
        {
            if (_buildingViews.TryGetValue(msg.BuildingId, out var view))
            {
                UnityEngine.Object.Destroy(view.gameObject);
                _buildingViews.Remove(msg.BuildingId);
            }
        }
        
        private void OnBuildingUpgraded(BuildingUpgradedMessage msg)
        {
            if (!_buildingViews.TryGetValue(msg.BuildingId, out var view))
            {
                Debug.LogError($"[BuildingPresenter.OnBuildingUpgraded] BuildingView not found for ID {msg.BuildingId}");
                return;
            }

            view.UpdateSprite(_buildingsConfig.GetBuildingIcon(view.BuildingType, msg.NewLevel.Level));
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}