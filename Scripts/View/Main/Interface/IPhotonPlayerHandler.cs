using System;
using Photon.Realtime;
using UniRx;

namespace Lycoris102.Unity1Week202008.View.Main.Interface
{
    public interface IPhotonPlayerHandler
    {
        IObservable<Player> OnEnterAsObservable();
        IObservable<Player> OnLeftAsObservable();
        IObservable<Unit> OnUpdateCustomPropertyAsObservable();
    }
}