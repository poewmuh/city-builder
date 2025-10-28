using System;
using CityBuilder.Domain.Gameplay.MessagesDTO.Buildings;
using CityBuilder.Domain.Gameplay.Models.Buildings;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace CityBuilder.UseCases.Gameplay
{
    public class DeleteBuildingUseCase : IInitializable, IDisposable
    {
        [Inject] private BuildingsSystem _buildingsSystem;
        [Inject] private readonly ISubscriber<DeleteBuildingCommand> _deleteCommandSub;
        [Inject] private readonly IPublisher<BuildingDeletedMessage> _buildingDeletedPub;
        [Inject] private readonly IPublisher<BuildingOperationFailedMessage> _failedPub;
        
        private IDisposable _subscription;
        
        public void Initialize()
        {
            _subscription = _deleteCommandSub.Subscribe(DeleteProcess);
        }
        
        private void DeleteProcess(DeleteBuildingCommand command)
        {
            var building = _buildingsSystem.GetBuilding(command.BuildingId);
            if (building == null)
            {
                PublishFailedEvent("Здание не найдено.");
                return;
            }
            
            var position = building.Position;
            _buildingsSystem.RemoveBuilding(command.BuildingId);

            _buildingDeletedPub.Publish(new BuildingDeletedMessage(command.BuildingId, position));
        }
        
        private void PublishFailedEvent(string reason)
        {
            _failedPub.Publish(new BuildingOperationFailedMessage(BuildingOperationType.DeleteBuilding, reason));
        }
        
        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}
