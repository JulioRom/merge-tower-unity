using VContainer;
using VContainer.Unity;

namespace MergeTower.DI
{
    public sealed class ProjectLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<CustomUpdateManager>(Lifetime.Singleton);
        }
    }
}
