using System;

namespace Lycoris102.Unity1Week202008.View.Lobby.Interface
{
    public interface INameHandler
    {
        IObservable<string> OnSetNameAsObservable();
    }
}