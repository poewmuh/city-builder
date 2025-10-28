using System;
using CityBuilder.Domain.Gameplay.MessagesDTO;
using CityBuilder.Domain.Gameplay.MessagesDTO.Buildings;
using CityBuilder.Domain.Gameplay.Models;
using CityBuilder.Domain.Gameplay.Models.Buildings;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace CityBuilder.UseCases.Gameplay
{
    public class UpgradeBuildingUseCase : IInitializable, IDisposable
    {
        [Inject] private BuildingsSystem _buildingsSystem;
        [Inject] private IBuildingsDataConfig _buildingDataConfig;
        [Inject] private PlayerResources _playerResources;
        
        [Inject] private readonly ISubscriber<UpgradeBuildingCommand> _upgradeCommandSub;
        [Inject] private readonly IPublisher<BuildingUpgradedMessage> _buildingUpgradePub;
        [Inject] private readonly IPublisher<BuildingOperationFailedMessage> _failedPub;
        [Inject] private IPublisher<ResourcesChangedMessage> _resourcesChangedPub;
        
        private IDisposable _subscription;
        
        public void Initialize()
        {
            _subscription = _upgradeCommandSub.Subscribe(UpgradeProcess);
        }

        private void UpgradeProcess(UpgradeBuildingCommand command)
        {
            var building = _buildingsSystem.GetBuilding(command.BuildingId);
            if (building == null)
            {
                PublishFailedEvent("Здание не найдено");
                return;
            }

            var nextLevel = building.BuildingLevel.LevelUp();
            if (!_buildingDataConfig.HasNextLevel(building.BuildingType, building.BuildingLevel))
            {
                PublishFailedEvent("Достигнут максимальный уровень здания");
                return;
            }

            var nextLevelData = _buildingDataConfig.GetBuildingData(building.BuildingType, nextLevel);
            if (nextLevelData == null)
            {
                PublishFailedEvent("Нет данных для следующего уровня здания");
                return;
            }

            if (!_playerResources.TrySpendGold(nextLevelData.BuildCost))
            {
                PublishFailedEvent("Нужно больше золота");
                return;
            }

            building.LevelUp();

            _buildingUpgradePub.Publish(new BuildingUpgradedMessage(command.BuildingId, nextLevel));
            _resourcesChangedPub.Publish(new ResourcesChangedMessage(_playerResources.Gold));
        }

        private void PublishFailedEvent(string reason)
        {
            _failedPub.Publish(new BuildingOperationFailedMessage(BuildingOperationType.UpgradeBuilding, reason));
        }
        
        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}