using System.Collections.Generic;
using Photon.Realtime;

namespace Lycoris102.Unity1Week202008.View.Main.Interface
{
    public interface IStageReadyPlayerListRenderer
    {
        void Render(List<Player> playerList, int playerOwnerIndex, int localPlayerIndex);
    }
}