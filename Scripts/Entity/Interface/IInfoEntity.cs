using System;

namespace Lycoris102.Unity1Week202008.Entity.Interface
{
    public interface IInfoEntity
    {
        void Set(string text);
        IObservable<string> OnUpdateAsObservable();
    }
}