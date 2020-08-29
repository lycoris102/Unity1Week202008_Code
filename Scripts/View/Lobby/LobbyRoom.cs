using Lycoris102.Unity1Week202008.Const;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    public class LobbyRoom : UIBehaviour
    {
        // Nameは内部で使用する名称
        [SerializeField] public string Name = default;
        [SerializeField] private TextMeshProUGUI RoomCountText = default;
        [SerializeField] private Image Image = default;
        [SerializeField] private GameObject EnableObject = default;
        [SerializeField] private GameObject DisableObject = default;
        [SerializeField] private GameObject RoomConnectingObject = default;

        protected override void Start()
        {
            this.OnPointerDownAsObservable()
                .Subscribe(_ => Join())
                .AddTo(this);
        }

        public void UpdateState(int current, int max)
        {
            RoomCountText.text = $"{current}/{max}";
            bool enabled = current < max;
            EnableObject.SetActive(enabled);
            DisableObject.SetActive(!enabled);
            Image.raycastTarget = enabled;
        }

        private void Join()
        {
            if (PhotonNetwork.InRoom) return;
            RoomConnectingObject.SetActive(true);
            PhotonNetwork.JoinOrCreateRoom(Name, new RoomOptions()
            {
                MaxPlayers = (byte) Setting.RoomPlayerCount,
                EmptyRoomTtl = 0,
            }, TypedLobby.Default);
        }
    }
}