using System;
using CityBuilder.Domain.Gameplay.MessagesDTO.Buildings;
using CityBuilder.Domain.Gameplay.Models.Buildings;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace CityBuilder.UseCases.Gameplay
{
    public class MoveBuildingUseCase : IInitializable, IDisposable
    {
        [Inject] private BuildingsSystem _buildingsSystem;
        [Inject] private readonly ISubscriber<MoveBuildingCommand> _moveCommandSub;
        [Inject] private readonly IPublisher<BuildingMovedMessage> _buildingMovedPub;
        [Inject] private readonly IPublisher<BuildingOperationFailedMessage> _failedPub;
        
        private IDisposable _subscription;
        
        public void Initialize()
        {
            _subscription = _moveCommandSub.Subscribe(BuildMoveProcess);
        }
        
        private void BuildMoveProcess(MoveBuildingCommand command)
        {
            var building = _buildingsSystem.GetBuilding(command.BuildingId);
            if (building == null)
            {
                PublishFailedEvent("Нет данных о таком здании.");
                return;
            }
            
            if (_buildingsSystem.TryGetBuildingAt(command.NewPosition, out var existBuild) && existBuild.Id != command.BuildingId)
            {
                PublishFailedEvent("Клетка занята другим зданием.");
                return;
            }
            
            var oldPosition = building.Position;
            building.SetPosition(command.NewPosition);
            _buildingMovedPub.Publish(new BuildingMovedMessage(
                command.BuildingId, oldPosition, command.NewPosition));
        }
        
        private void PublishFailedEvent(string reason)
        {
            _failedPub.Publish(new BuildingOperationFailedMessage(BuildingOperationType.MoveBuilding, reason));
        }
        
        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}
