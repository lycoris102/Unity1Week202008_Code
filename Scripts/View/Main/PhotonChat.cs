using System;
using Lycoris102.Unity1Week202008.Const;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using Photon.Pun;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class PhotonChat : MonoBehaviourPunCallbacks, IPhotonChatPrcRequester, IChatHandler
    {
        [SerializeField] private Chat ChatPrefab = default;
        [SerializeField] private ScrollRect ScrollRect = default;
        [SerializeField] private Transform ChatParent = default;
        private ISubject<Tuple<int, string>> ChatSubject = new ReplaySubject<Tuple<int, string>>();

        private AudioSource audioSource;
        private AudioSource AudioSource => audioSource ?? (audioSource = GetComponent<AudioSource>());

        public void Request(string playerName, int playerIndex, string text, bool isOwner)
            => photonView.RpcSecure("SpawnChat", RpcTarget.All, true, playerName, playerIndex, text, isOwner);

        [PunRPC]
        public void SpawnChat(string playerName, int playerIndex, string text, bool isOwner)
        {
            Render(playerName, text, playerIndex, isOwner);
            ChatSubject.OnNext(new Tuple<int, string>(playerIndex, text));
        }

        public void Delete()
        {
            foreach (Transform child in ChatParent)
            {
                Destroy(child.gameObject);
            }
        }

        private void Render(string playerName, string text, int playerIndex, bool isOwner)
        {
            var chat = Instantiate(ChatPrefab);
            chat.transform.SetParent(ChatParent, false);
            chat.Render(playerName, Setting.PlayerIndex2ColorMap[playerIndex], text, isOwner);
            ScrollRect.verticalNormalizedPosition = 0;
            AudioSource.PlayOneShot(AudioSource.clip);
        }

        public IObservable<Tuple<int, string>> OnChatAsObservable()
            => ChatSubject;
    }
}