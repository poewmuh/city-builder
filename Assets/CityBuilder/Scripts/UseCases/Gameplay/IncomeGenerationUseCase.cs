using System;
using CityBuilder.Domain.Gameplay.MessagesDTO;
using CityBuilder.Domain.Gameplay.Models;
using CityBuilder.Domain.Gameplay.Models.Buildings;
using MessagePipe;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace CityBuilder.UseCases.Gameplay
{
    public class IncomeGenerationUseCase : IInitializable, IDisposable
    {
        private const float INCOME_INTERVAL = 5f;
        
        [Inject] private readonly BuildingsSystem _buildingsSystem;
        [Inject] private readonly PlayerResources _playerResources;
        [Inject] private readonly IBuildingsDataConfig _buildingDataConfig;
        [Inject] private readonly IPublisher<ResourcesChangedMessage> _resourcesChangedPub;

        private readonly CompositeDisposable _disposables = new();

        public void Initialize()
        {
            Observable.Interval(TimeSpan.FromSeconds(INCOME_INTERVAL))
                .Subscribe(_ => GenerateIncome())
                .AddTo(_disposables);
        }

        private void GenerateIncome()
        {
            int totalIncome = _buildingsSystem.GetTotalIncome(_buildingDataConfig);

            if (totalIncome <= 0)
            {
                return;
            }

            _playerResources.AddGold(totalIncome);

            _resourcesChangedPub.Publish(new ResourcesChangedMessage(_playerResources.Gold));
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}