using System;
using Lycoris102.Unity1Week202008.Entity.Interface;
using UniRx;

namespace Lycoris102.Unity1Week202008.Entity
{
    public class TimerEntity : ITimerEntity, IDisposable
    {
        private IntReactiveProperty TimeReactiveProperty = new IntReactiveProperty();
        private IDisposable TimerDisposable;

        public int Time => TimeReactiveProperty.Value;

        public void Start()
            => TimerDisposable = Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => TimeReactiveProperty.Value++);

        public void Stop()
            => TimerDisposable.Dispose();

        public void Reset()
            => TimeReactiveProperty.Value = 0;

        public IObservable<int> OnUpdateTimerAsObservable()
            => TimeReactiveProperty;

        public void Dispose()
        {
            TimeReactiveProperty?.Dispose();
            TimerDisposable?.Dispose();
        }
    }
}