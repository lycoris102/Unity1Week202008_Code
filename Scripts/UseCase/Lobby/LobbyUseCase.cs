using Lycoris102.Unity1Week202008.UseCase.Lobby.Interface;
using Lycoris102.Unity1Week202008.View.Common;
using Lycoris102.Unity1Week202008.View.Common.Interface;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.UseCase.Lobby
{
    public class LobbyUseCase : IInitializable, ILobbyUseCase
    {
        private IAudioPlayer AudioPlayer { get; }

        public LobbyUseCase(IAudioPlayer audioPlayer)
        {
            AudioPlayer = audioPlayer;
        }

        public void Initialize()
        {
            AudioPlayer.Play(AudioType.Lobby);
        }
    }
}