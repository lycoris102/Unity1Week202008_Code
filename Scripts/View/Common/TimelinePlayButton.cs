using Lycoris102.Unity1Week202008.Utility;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

namespace Lycoris102.Unity1Week202008.View.Common
{
    public class TimelinePlayButton : UIBehaviour
    {
        [SerializeField] private PlayableDirector PlayableDirector = default;

        protected override void Start()
        {
            base.Start();
            this.OnPointerDownAsObservable()
                .Where(_ => PlayableDirector.state != PlayState.Playing)
                .Subscribe(_ =>
                {
                    PlayableDirector.Rewind();
                    PlayableDirector.Play();
                })
                .AddTo(this);
        }
    }
}