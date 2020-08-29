using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Common
{
    public class AudioButton : UIBehaviour
    {
        private AudioSource audioSource;
        private AudioSource AudioSource => audioSource ?? (audioSource = GetComponent<AudioSource>());

        [SerializeField] private bool IsRandomPitch = default;

        protected override void Start()
        {
            base.Start();
            this.OnPointerDownAsObservable()
                .Subscribe(_ =>
                {
                    if (IsRandomPitch)
                    {
                        AudioSource.pitch = Random.Range(0.9f, 1.1f);
                    }

                    AudioSource.PlayOneShot(AudioSource.clip);
                })
                .AddTo(this);
        }
    }
}