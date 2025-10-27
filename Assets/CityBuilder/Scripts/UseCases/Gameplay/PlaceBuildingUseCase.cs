using System;
using CityBuilder.Domain.Gameplay.MessagesDTO;
using CityBuilder.Domain.Gameplay.MessagesDTO.Buildings;
using CityBuilder.Domain.Gameplay.Models;
using CityBuilder.Domain.Gameplay.Models.Buildings;
using CityBuilder.Domain.Gameplay.Models.Buildings.Base;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace CityBuilder.UseCases.Gameplay
{
    public class PlaceBuildingUseCase : IInitializable, IDisposable
    {
        private const string OCCUPIED_POSITION_KEY = "Клетка занята другим зданием.";
        private const string NO_DATA_KEY = "Нет данных о таком здании.";
        private const string NO_GOLD = "Нужно больше золота.";
        
        [Inject] private BuildingsController _buildingsController;
        [Inject] private PlayerResources _playerResources;
        [Inject] private IBuildingsDataConfig _buildingDataConfig;

        [Inject] private ISubscriber<PlaceBuildingCommand> _placeBuildingSub;
        [Inject] private IPublisher<BuildingPlacedMessage> _buildingPlacedPub;
        [Inject] private IPublisher<BuildingOperationFailedMessage> _failedPub;
        [Inject] private IPublisher<ResourcesChangedMessage> _resourcesChangedPub;
        
        private IDisposable _subscription;
        
        public void Initialize()
        {
            _subscription = _placeBuildingSub.Subscribe(PlaceBuildingProcess);
        }

        private void PlaceBuildingProcess(PlaceBuildingCommand command)
        {
            if (_buildingsController.IsPosOccupied(command.Position))
            {
                PublishFailedEvent(BuildingOperationType.PlaceBuilding, OCCUPIED_POSITION_KEY);
                return;
            }

            var buildingData = _buildingDataConfig.GetBuildingData(command.BuildingType, new BuildingLevel(1));
            if (buildingData == null)
            {
                PublishFailedEvent(BuildingOperationType.PlaceBuilding, NO_DATA_KEY);
                return;
            }

            if (!_playerResources.TrySpendGold(buildingData.BuildCost))
            {
                PublishFailedEvent(BuildingOperationType.PlaceBuilding, NO_GOLD);
            }

            var newBuilding = _buildingsController.AddNewBuilding(command.BuildingType, buildingData.BuildingLevel,
                command.Position, command.Rotation);

            _buildingPlacedPub.Publish(new BuildingPlacedMessage(newBuilding.Id, newBuilding.BuildingType,
                newBuilding.Position, newBuilding.Rotation));
        }

        private void PublishFailedEvent(BuildingOperationType type, string reason)
        {
            _failedPub.Publish(new BuildingOperationFailedMessage(type, reason));
        }
        
        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}