using Lycoris102.Unity1Week202008.Entity;
using Lycoris102.Unity1Week202008.UseCase.Lobby;
using Lycoris102.Unity1Week202008.View.Lobby;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.Installer
{
    public class LobbyInstaller : LifetimeScope
    {
        // Name
        [SerializeField] private RenameSubmitButton RenameSubmitButton = default;
        [SerializeField] private NameInputField NameInputField = default;
        [SerializeField] private NameLobby NameLobby = default;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LobbyUseCase>(Lifetime.Scoped).AsImplementedInterfaces();

            // Name
            builder.Register<NameEntity>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<NameUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterInstance(RenameSubmitButton).AsImplementedInterfaces();
            builder.RegisterInstance(NameInputField).AsImplementedInterfaces();
            builder.RegisterInstance(NameLobby).AsImplementedInterfaces();
        }
    }
}