using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    public class Lobby : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject RoomListObject = default;
        [SerializeField] private GameObject DisconnectObject = default;
        [SerializeField] private GameObject RoomConnectingObject = default;
        [SerializeField] private GameObject RoomConnectFailedObject = default;
        [SerializeField] private TextMeshProUGUI DisconnectText = default;
        [SerializeField] private TextMeshProUGUI InfoText = default;

        public void Start()
        {
            if (!PhotonNetwork.IsConnected)
            {
                Connect();
            }

            if (PhotonNetwork.IsConnected && !PhotonNetwork.InLobby)
            {
                JoinLobby();
            }

            DisconnectObject.SetActive(!PhotonNetwork.IsConnected);
            RoomListObject.SetActive(PhotonNetwork.IsConnected);
        }

        // Success
        public override void OnConnectedToMaster()
            => JoinLobby();

        private void Connect()
        {
            PhotonNetwork.ConnectUsingSettings();
            SetInfoText("接続を開始します...");
        }

        private void JoinLobby()
        {
            PhotonNetwork.JoinLobby();
            SetInfoText("接続しました ロビーに接続します...");
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            DisconnectObject.SetActive(false);
            RoomListObject.SetActive(true);
            SetInfoText("ロビーに接続しました 「もじもじフラグメンツ」をお楽しみください");
        }

        public override void OnDisconnected(DisconnectCause disconnectCause)
        {
            DisconnectObject.SetActive(true);
            RoomListObject.SetActive(false);
            switch (disconnectCause)
            {
                case DisconnectCause.MaxCcuReached:
                    DisconnectText.text = "接続可能人数が上限に達したため、接続できません\n時間をおいて再度アクセスをお願いします";
                    break;
                default:
                    DisconnectText.text = "サーバに接続できませんでした\n時間をおいて再度アクセスをお願いします";
                    break;
            }
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            RoomConnectingObject.SetActive(false);
            RoomConnectFailedObject.SetActive(true);
        }

        private void SetInfoText(string text)
        {
            InfoText.text = "";
            InfoText.DOText(text, text.Length * 0.01f).SetEase(Ease.Linear).Play();
        }
    }
}