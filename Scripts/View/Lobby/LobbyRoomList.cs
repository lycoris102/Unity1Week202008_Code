using System.Collections.Generic;
using System.Linq;
using Lycoris102.Unity1Week202008.Const;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    public class LobbyRoomList : MonoBehaviourPunCallbacks
    {
        [SerializeField] private List<LobbyRoom> Rooms = default;

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            var roomMap = roomList.ToDictionary(x => x.Name, x => x);
            foreach (var room in Rooms)
            {
                var playerCount = roomMap.ContainsKey(room.Name) ? roomMap[room.Name].PlayerCount : 0;
                room.UpdateState(playerCount, Setting.RoomPlayerCount);
            }
        }
    }
}