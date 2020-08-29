using System;
using Lycoris102.Unity1Week202008.Utility;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class StageResultTimeline : MonoBehaviour, IStageResultTimelineHandler, IStageResultTimelineRenderer
    {
        [SerializeField] private PlayableDirector CorrectPlayableDirector = default;
        [SerializeField] private PlayableDirector TimeoutPlayableDirector = default;

        private ISubject<Unit> FinishSubject = new ReplaySubject<Unit>();

        // Signal経由で呼び出す
        public void Finish()
            => FinishSubject.OnNext(Unit.Default);

        public IObservable<Unit> OnFinishAsObservable()
            => FinishSubject;

        public void Play(bool isCorrect)
        {
            if (isCorrect)
            {
                CorrectPlayableDirector.Rewind();
                CorrectPlayableDirector.Play();
            }
            else
            {
                TimeoutPlayableDirector.Rewind();
                TimeoutPlayableDirector.Play();
            }
        }
    }
}