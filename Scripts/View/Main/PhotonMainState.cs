using System;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using Photon.Pun;
using UniRx;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class PhotonMainState : MonoBehaviourPunCallbacks, IMainStateRpcRequester, IMainStateHandler
    {
        private ISubject<MainState> MainStateSubject = new ReplaySubject<MainState>();

        [PunRPC]
        public void ChangeState(int stateIndex)
            => MainStateSubject.OnNext((MainState) stateIndex);

        public void Request(string state)
            => Request((MainState) Enum.Parse(typeof(MainState), state, true));

        public void Request(MainState state)
        {
            if (PhotonNetwork.InRoom)
            {
                photonView.RpcSecure("ChangeState", RpcTarget.All, true, (int) state);
            }
        }

        public IObservable<MainState> OnChangeAsObservable()
            => MainStateSubject;
    }
}