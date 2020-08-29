using Lycoris102.Unity1Week202008.View.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.Installer
{
    public class RootInstaller : LifetimeScope
    {
        [SerializeField] private AudioPlayer AudioPlayer = default;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(AudioPlayer, Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}