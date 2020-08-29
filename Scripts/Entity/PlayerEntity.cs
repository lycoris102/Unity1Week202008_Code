using System.Collections.Generic;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Photon.Realtime;

namespace Lycoris102.Unity1Week202008.Entity
{
    public class PlayerEntity : IPlayerEntity
    {
        public List<Player> PlayerList { get; private set; }
        public int PlayerOwnerIndex { get; private set; }
        public int PlayerCorrectIndex { get; private set; }

        private Dictionary<int, string> ActorNumber2Comment = new Dictionary<int, string>();

        public void SetPlayerList(List<Player> playerList)
            => PlayerList = playerList;

        public void SetPlayerOwnerIndex(int index)
            => PlayerOwnerIndex = index;

        public void SetPlayerCorrectIndex(int index)
            => PlayerCorrectIndex = index;

        public int GetIndexByPlayer(Player player)
            => PlayerList.FindIndex(x => x.ActorNumber == player.ActorNumber);

        public bool IsOwner(int index)
            => index == PlayerOwnerIndex;

        public bool IsOwner(Player player)
            => IsOwner(GetIndexByPlayer(player));
    }
}