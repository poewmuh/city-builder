using CityBuilder.Domain.Gameplay.Models;
using CityBuilder.Domain.Gameplay.Models.Buildings;
using CityBuilder.Domain.Gameplay.Models.Grid;
using CityBuilder.Infastructure.Input;
using CityBuilder.Presentation.Gameplay.Presenters;
using CityBuilder.Presentation.Gameplay.Views;
using CityBuilder.Repositories.Gameplay;
using CityBuilder.UseCases.Gameplay;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CityBuilder.Installers.Gameplay
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [Header("Configs")]
        [SerializeField] private GridConfig _gridConfig;
        [SerializeField] private AllBuildingsConfig _allBuildingsConfig;
        [Header("Links")]
        [SerializeField] private GridView _gridView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            var messagePipeOptions = builder.RegisterMessagePipe();
            builder.RegisterMessageBroker<object>(messagePipeOptions);
            
            // Domain
            var gridSettings = new GridSettings(_gridConfig.GridWidth, _gridConfig.GridHeight, _gridConfig.CellSize);
            builder.RegisterInstance(gridSettings);
            builder.Register<GridSystem>(Lifetime.Singleton);
            builder.Register<BuildingsSystem>(Lifetime.Singleton);
            builder.RegisterInstance(new PlayerResources(100));
            
            // Repositories
            builder.RegisterInstance(_gridConfig);
            builder.RegisterInstance<IBuildingsDataConfig>(_allBuildingsConfig);
            
            // Infrastructure
            builder.Register<InputService>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            // UseCases
            builder.RegisterEntryPoint<PlaceBuildingUseCase>();
            builder.RegisterEntryPoint<MoveBuildingUseCase>();
            builder.RegisterEntryPoint<DeleteBuildingUseCase>();
            builder.RegisterEntryPoint<UpgradeBuildingUseCase>();
            
            // Presentation
            builder.RegisterInstance(_gridView);
            builder.RegisterEntryPoint<GridPresenter>();
            builder.RegisterEntryPoint<BuildingPresenter>();
            builder.RegisterEntryPoint<BuildingPlacementPresenter>();
            
            
            //builder.RegisterEntryPoint<GameplayEntryPoint>();
        }
    }
}