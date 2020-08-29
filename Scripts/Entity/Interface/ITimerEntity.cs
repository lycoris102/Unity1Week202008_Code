using System;

namespace Lycoris102.Unity1Week202008.Entity.Interface
{
    public interface ITimerEntity
    {
        int Time { get; }
        void Start();
        void Stop();
        void Reset();
        IObservable<int> OnUpdateTimerAsObservable();
    }
}