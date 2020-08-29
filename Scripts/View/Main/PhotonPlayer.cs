using System;
using ExitGames.Client.Photon;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using Photon.Pun;
using Photon.Realtime;
using UniRx;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class PhotonPlayer : MonoBehaviourPunCallbacks, IPhotonPlayerHandler
    {
        private ISubject<Player> PlayerEnteredRoomSubject = new ReplaySubject<Player>();
        private ISubject<Player> PlayerLeftRoomSubject = new ReplaySubject<Player>();
        private ISubject<Unit> PlayerUpdateCustomPropertySubject = new ReplaySubject<Unit>();

        public override void OnPlayerEnteredRoom(Player player)
            => PlayerEnteredRoomSubject.OnNext(player);

        public override void OnPlayerLeftRoom(Player player)
            => PlayerLeftRoomSubject.OnNext(player);

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
            => PlayerUpdateCustomPropertySubject.OnNext(Unit.Default);

        public IObservable<Player> OnEnterAsObservable()
            => PlayerEnteredRoomSubject;

        public IObservable<Player> OnLeftAsObservable()
            => PlayerLeftRoomSubject;

        public IObservable<Unit> OnUpdateCustomPropertyAsObservable()
            => PlayerUpdateCustomPropertySubject.AsUnitObservable();
    }
}