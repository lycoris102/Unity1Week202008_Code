using System;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using Photon.Pun;
using UniRx;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class PhotonStageReady : MonoBehaviourPunCallbacks, IStageReadyRpcRequester, IStageReadyHandler
    {
        private ISubject<string> SetAnswerSubject = new ReplaySubject<string>();

        public void Request(string answer)
            => photonView.RpcSecure("SetAnswer", RpcTarget.All, true, answer);

        [PunRPC]
        public void SetAnswer(string answer)
            => SetAnswerSubject.OnNext(answer);

        public IObservable<string> OnSetAnswerAsObservable()
            => SetAnswerSubject;
    }
}