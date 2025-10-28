using System;
using CityBuilder.Domain.Gameplay.MessagesDTO;
using CityBuilder.Utilities;
using MessagePipe;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CityBuilder.Infastructure.Input
{
    public class InputService : IInitializable, IDisposable
    {
        [Inject] private readonly IPublisher<BuildingPrefabSelectedInputMessage> _prefabSelectedPublisher;
        [Inject] private readonly IPublisher<RotateBuildingInputMessage> _rotatePublisher;
        [Inject] private readonly IPublisher<DeleteBuildingInputMessage> _deletePublisher;
        [Inject] private readonly IPublisher<ClickActionInputMessage> _clickedPublisher;
        
        private CityBuilderInputActions _inputActions;
        private readonly CompositeDisposable _disposable = new();
        
        public Vector2 CameraMovement => _inputActions.Gameplay.CameraMove.ReadValue<Vector2>();
        public float Zoom => _inputActions.Gameplay.CameraZoom.ReadValue<float>();
        public Vector2 MousePosition => _inputActions.Gameplay.MousePosition.ReadValue<Vector2>();
        
        public void Initialize()
        {
            _inputActions = new CityBuilderInputActions();
            _inputActions.Enable();
            _inputActions.Gameplay.SelectPrefab1.PerformedAsObservable()
                .Subscribe(_ => _prefabSelectedPublisher.Publish(new BuildingPrefabSelectedInputMessage(0)))
                .AddTo(_disposable);

            _inputActions.Gameplay.SelectPrefab2.PerformedAsObservable()
                .Subscribe(_ => _prefabSelectedPublisher.Publish(new BuildingPrefabSelectedInputMessage(1)))
                .AddTo(_disposable);

            _inputActions.Gameplay.SelectPrefab3.PerformedAsObservable()
                .Subscribe(_ => _prefabSelectedPublisher.Publish(new BuildingPrefabSelectedInputMessage(2)))
                .AddTo(_disposable);

            _inputActions.Gameplay.Rotate.PerformedAsObservable()
                .Subscribe(_ => _rotatePublisher.Publish(new RotateBuildingInputMessage()))
                .AddTo(_disposable);

            _inputActions.Gameplay.Delete.PerformedAsObservable()
                .Subscribe(_ => _deletePublisher.Publish(new DeleteBuildingInputMessage()))
                .AddTo(_disposable);
            
            _inputActions.Gameplay.Click.PerformedAsObservable()
                .Subscribe(_ => _clickedPublisher.Publish(new ClickActionInputMessage()))
                .AddTo(_disposable);
        }
        
        public void Dispose()
        {
            _inputActions?.Disable();
            _disposable?.Dispose();
        }
    }
}