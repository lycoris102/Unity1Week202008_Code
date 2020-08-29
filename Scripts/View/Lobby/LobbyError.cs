using Photon.Pun;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    public class LobbyError : MonoBehaviourPunCallbacks
    {
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            Debug.Log(returnCode);
        }
    }
}