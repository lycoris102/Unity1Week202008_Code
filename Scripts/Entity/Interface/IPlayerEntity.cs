using System.Collections.Generic;
using Photon.Realtime;

namespace Lycoris102.Unity1Week202008.Entity.Interface
{
    public interface IPlayerEntity
    {
        List<Player> PlayerList { get; }
        int PlayerOwnerIndex { get; }
        void SetPlayerList(List<Player> playerList);
        void SetPlayerOwnerIndex(int index);
        void SetPlayerCorrectIndex(int index);
        int GetIndexByPlayer(Player player);
        bool IsOwner(int index);
        bool IsOwner(Player player);
    }
}