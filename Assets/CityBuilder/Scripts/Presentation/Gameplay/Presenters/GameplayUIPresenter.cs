using System;
using CityBuilder.Domain.Gameplay.MessagesDTO;
using CityBuilder.Domain.Gameplay.Models;
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
        [Inject] private readonly IPublisher<BuildingPrefabSelectedInputMessage> _buildingSelectedPub;
        
        private readonly CompositeDisposable _disposables = new();
        private int _selectedBuildingIndex = -1;
        
        public void Initialize()
        {
            _resourcesChangedSub.Subscribe(OnResourcesChanged).AddTo(_disposables);

            _uiView.OnHouseButtonClicked.Subscribe(_ => OnBuildingButtonClicked(1))
                .AddTo(_disposables);
            _uiView.OnFarmButtonClicked.Subscribe(_ => OnBuildingButtonClicked(2))
                .AddTo(_disposables);
            _uiView.OnMineButtonClicked.Subscribe(_ => OnBuildingButtonClicked(3))
                .AddTo(_disposables);

            _uiView.UpdateGold(_playerResources.Gold);
        }
        
        private void OnResourcesChanged(ResourcesChangedMessage msg)
        {
            _uiView.UpdateGold(msg.NewGold);
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