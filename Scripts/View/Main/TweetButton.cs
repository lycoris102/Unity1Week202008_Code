using System;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class TweetButton : UIBehaviour, ITweetButtonHandler
    {
        public IObservable<Unit> OnDownAsObservable()
            => this.OnPointerDownAsObservable()
                .AsUnitObservable();
    }
}