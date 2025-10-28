using System;
using CityBuilder.Domain.Gameplay.MessagesDTO;
using CityBuilder.Domain.Gameplay.MessagesDTO.Buildings;
using CityBuilder.Domain.Gameplay.MessagesDTO.Grid;
using CityBuilder.Domain.Gameplay.Models;
using CityBuilder.Domain.Gameplay.Models.Buildings;
using CityBuilder.Presentation.Gameplay.Views;
using MessagePipe;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace CityBuilder.Presentation.Gameplay.Presenters
{
    public class GameplayUIPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly GameplayUIView _uiView;
        [Inject] private readonly PlayerResources _playerResources;
        
        [Inject] private readonly ISubscriber<ResourcesChangedMessage> _resourcesChangedSub;
        [Inject] private readonly ISubscriber<BuildingSelectedMessage> _buildingSelectedSub;
        [Inject] private readonly ISubscriber<GridClickedMessage> _gridClickedSub;
        
        [Inject] private readonly IPublisher<BuildingPrefabSelectedInputMessage> _buildingSelectedPub;
        [Inject] private readonly IPublisher<UpgradeBuildingCommand> _upgradeCommandPub;
        [Inject] private readonly IPublisher<MoveBuildingCommand> _moveCommandPub;
        [Inject] private readonly IPublisher<DeleteBuildingCommand> _deleteCommandPub;
        
        
        [Inject] private readonly BuildingsSystem _buildingsSystem;
        [Inject] private readonly IBuildingsDataConfig _buildingDataConfig;
        
        
        
        private readonly CompositeDisposable _disposables = new();
        private int _selectedBuildingIndex = -1;
        private int _selectedBuildingId = -1;
        
        public void Initialize()
        {
            _resourcesChangedSub.Subscribe(OnResourcesChanged).AddTo(_disposables);
            _buildingSelectedSub.Subscribe(OnBuildingSelected).AddTo(_disposables);
            _gridClickedSub.Subscribe(OnClickGrid).AddTo(_disposables);

            _uiView.OnHouseButtonClicked.Subscribe(_ => OnBuildingButtonClicked(1))
                .AddTo(_disposables);
            _uiView.OnFarmButtonClicked.Subscribe(_ => OnBuildingButtonClicked(2))
                .AddTo(_disposables);
            _uiView.OnMineButtonClicked.Subscribe(_ => OnBuildingButtonClicked(3))
                .AddTo(_disposables);
            
            _uiView.OnUpgradeButtonClicked.Subscribe(_ => OnUpgradeClicked())
                .AddTo(_disposables);
            _uiView.OnMoveButtonClicked.Subscribe(_ => OnMoveClicked())
                .AddTo(_disposables);
            _uiView.OnDeleteButtonClicked.Subscribe(_ => OnDeleteClicked())
                .AddTo(_disposables);

            _uiView.UpdateGold(_playerResources.Gold);
        }
        
        private void OnBuildingSelected(BuildingSelectedMessage msg)
        {
            _selectedBuildingId = msg.BuildingId;

            var building = _buildingsSystem.GetBuilding(msg.BuildingId);
            var buildingData = _buildingDataConfig.GetBuildingData(building.BuildingType, building.BuildingLevel);
            if (buildingData == null)
            {
                _uiView.HideBuildingInfo();
                return;
            }
            
            var canUpgrade = _buildingDataConfig.HasNextLevel(building.BuildingType, building.BuildingLevel);
            _uiView.ShowBuildingInfo(canUpgrade);
            _selectedBuildingIndex = -1;
        }
        
        private void OnClickGrid(GridClickedMessage msg)
        {
            _uiView.HideBuildingInfo();
            _selectedBuildingId = -1;
        }
        
        private void OnUpgradeClicked()
        {
            if (_selectedBuildingId < 0) return;

            _upgradeCommandPub.Publish(new UpgradeBuildingCommand(_selectedBuildingId));
        }
        
        private void OnMoveClicked()
        {
            if (_selectedBuildingId < 0) return;
        }
        
        private void OnResourcesChanged(ResourcesChangedMessage msg)
        {
            _uiView.UpdateGold(msg.NewGold);
        }
        
        private void OnDeleteClicked()
        {
            if (_selectedBuildingId < 0) return;

            _deleteCommandPub.Publish(new DeleteBuildingCommand(_selectedBuildingId));
            _uiView.HideBuildingInfo();
            _selectedBuildingId = -1;
        }

        private void OnBuildingButtonClicked(int buttonIndex)
        {
            if (_selectedBuildingIndex == buttonIndex)
            {
                return;
            }

            _selectedBuildingIndex = buttonIndex;
            _buildingSelectedPub.Publish(new BuildingPrefabSelectedInputMessage(buttonIndex));
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}