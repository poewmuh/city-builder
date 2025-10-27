using System;
using CityBuilder.Domain.Gameplay.MessagesDTO;
using CityBuilder.Utilities;
using MessagePipe;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace CityBuilder.Infastructure.Input
{
    public class InputService : IInitializable, IDisposable
    {
        private readonly IPublisher<BuildingPrefabSelectedMessage> _prefabSelectedPublisher;
        private readonly IPublisher<RotateBuildingMessage> _rotatePublisher;
        private readonly IPublisher<DeleteBuildingMessage> _deletePublisher;
        
        private CityBuilderInputActions _inputActions;
        private readonly CompositeDisposable _disposable = new();
        
        public Vector2 CameraMovement => _inputActions.Gameplay.CameraMove.ReadValue<Vector2>();
        public float Zoom => _inputActions.Gameplay.CameraZoom.ReadValue<float>();
        public Vector2 MousePosition => _inputActions.Gameplay.MousePosition.ReadValue<Vector2>();
        
        public InputService(
            IPublisher<BuildingPrefabSelectedMessage> prefabSelectedPublisher,
            IPublisher<RotateBuildingMessage> rotatePublisher,
            IPublisher<DeleteBuildingMessage> deletePublisher)
        
        {
            _prefabSelectedPublisher = prefabSelectedPublisher;
            _rotatePublisher = rotatePublisher;
            _deletePublisher = deletePublisher;
        }
        
        public void Initialize()
        {
            _inputActions = new CityBuilderInputActions();
            _inputActions.Enable();
            _inputActions.Gameplay.SelectPrefab1.PerformedAsObservable()
                .Subscribe(_ => _prefabSelectedPublisher.Publish(new BuildingPrefabSelectedMessage(0)))
                .AddTo(_disposable);

            _inputActions.Gameplay.SelectPrefab2.PerformedAsObservable()
                .Subscribe(_ => _prefabSelectedPublisher.Publish(new BuildingPrefabSelectedMessage(1)))
                .AddTo(_disposable);

            _inputActions.Gameplay.SelectPrefab3.PerformedAsObservable()
                .Subscribe(_ => _prefabSelectedPublisher.Publish(new BuildingPrefabSelectedMessage(2)))
                .AddTo(_disposable);

            _inputActions.Gameplay.Rotate.PerformedAsObservable()
                .Subscribe(_ => _rotatePublisher.Publish(new RotateBuildingMessage()))
                .AddTo(_disposable);

            _inputActions.Gameplay.Delete.PerformedAsObservable()
                .Subscribe(_ => _deletePublisher.Publish(new DeleteBuildingMessage()))
                .AddTo(_disposable);
        }
        
        public void Dispose()
        {
            _inputActions?.Disable();
            _disposable?.Dispose();
        }
    }
}