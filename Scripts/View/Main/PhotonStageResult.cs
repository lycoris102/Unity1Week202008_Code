using System;
using Lycoris102.Unity1Week202008.Struct;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using Photon.Pun;
using UniRx;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class PhotonStageResult : MonoBehaviourPunCallbacks, IStageResultRpcRequester, IStageResultHandler
    {
        private ISubject<ResultData> SetResultSubject = new ReplaySubject<ResultData>();

        public void Request(int stageCount, int time, int correctPlayerIndex, bool isCorrect)
            => photonView.RpcSecure("SetResult", RpcTarget.All, true, stageCount, time, correctPlayerIndex, isCorrect);

        [PunRPC]
        public void SetResult(int stageCount, int time, int correctPlayerIndex, bool isCorrect)
            => SetResultSubject.OnNext(new ResultData(stageCount, time, correctPlayerIndex, isCorrect));

        public IObservable<ResultData> OnSetAsObservable()
            => SetResultSubject;
    }
}