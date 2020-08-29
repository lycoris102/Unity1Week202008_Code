using System;
using UniRx;

namespace Lycoris102.Unity1Week202008.View.Main.Interface
{
    public interface IKeyboardSendButtonHandler
    {
        IObservable<Unit> OnDownAsObservable();
    }
}