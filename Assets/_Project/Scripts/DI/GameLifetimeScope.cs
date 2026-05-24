using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MergeTower.DI
{
    public sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameConfig _gameConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameConfig);
            builder.Register<SaveSystem>(Lifetime.Singleton);
            builder.Register<EconomySystem>(Lifetime.Singleton);
            builder.RegisterEntryPoint<EconomySystem>();
            builder.Register<TowerProgressionSystem>(Lifetime.Singleton);
            builder.Register<MergeGridSystem>(Lifetime.Singleton);
            builder.Register<SpawnSystem>(Lifetime.Singleton);
            builder.RegisterEntryPoint<SpawnSystem>();
            builder.Register<DailyGoalSystem>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<AdsService>();
            builder.RegisterComponentInHierarchy<TenjinService>();
            builder.RegisterComponentInHierarchy<IAPService>();
        }
    }
}
