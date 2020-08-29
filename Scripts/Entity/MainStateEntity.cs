using System;
using System.Linq;
using Lycoris102.Unity1Week202008.Entity.Interface;
using UniRx;

namespace Lycoris102.Unity1Week202008.Entity
{
    public class MainStateEntity : IMainStateEntity, IDisposable
    {
        private ReactiveProperty<MainState> MainStateReactiveProperty { get; } =
            new ReactiveProperty<MainState>(MainState.Wait);

        public MainState State => MainStateReactiveProperty.Value;

        public IObservable<MainState> OnChangeStateAsObservable()
            => MainStateReactiveProperty;

        public IObservable<MainState> OnChangeStateAsObservable(MainState state)
            => MainStateReactiveProperty.Where(x => x == state);

        public void Change(MainState state)
            => MainStateReactiveProperty.Value = state;

        public bool Check(params MainState[] stateList)
            => stateList.Any(x => x == State);

        public void Dispose()
        {
            MainStateReactiveProperty?.Dispose();
        }
    }
}