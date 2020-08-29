using System;

namespace Lycoris102.Unity1Week202008.Entity.Interface
{
    public interface IMainStateEntity
    {
        MainState State { get; }
        IObservable<MainState> OnChangeStateAsObservable();
        IObservable<MainState> OnChangeStateAsObservable(MainState state);
        void Change(MainState state);
        bool Check(params MainState[] stateList);
    }
}