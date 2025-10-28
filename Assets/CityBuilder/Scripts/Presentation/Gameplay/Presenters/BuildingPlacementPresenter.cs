using System;
using CityBuilder.Domain.Gameplay.MessagesDTO.Buildings;
using CityBuilder.Domain.Gameplay.MessagesDTO.Grid;
using CityBuilder.Domain.Gameplay.Models.Buildings.Base;
using MessagePipe;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace CityBuilder.Presentation.Gameplay.Presenters
{
    public class BuildingPlacementPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly ISubscriber<GridClickedMessage> _gridClickedSub;
        [Inject] private readonly IPublisher<PlaceBuildingCommand> _placeBuildingPub;

        private readonly CompositeDisposable _disposables = new();
        private BuildingType _selectedBuildingType = BuildingType.House;

        public void Initialize()
        {
            _gridClickedSub.Subscribe(OnGridClicked).AddTo(_disposables);
        }

        private void OnGridClicked(GridClickedMessage msg)
        {
            _placeBuildingPub.Publish(new PlaceBuildingCommand(_selectedBuildingType, msg.Position, 0));
        }

        public void SelectBuilding(BuildingType type)
        {
            _selectedBuildingType = type;
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
