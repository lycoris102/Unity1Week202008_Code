using System;
using DG.Tweening;
using Lycoris102.Unity1Week202008.Utility;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class KeyboardKey : UIBehaviour, IKeyboardKeyRenderer, IKeyboardKeyHandler
    {
        [SerializeField] private TextMeshProUGUI TextMeshPro = default;
        [SerializeField] private Image Image = default;
        [SerializeField] private Image PoinerEnterImage = default;
        [SerializeField] private PlayableDirector EnablePlayableDirector = default;
        [SerializeField] private PlayableDirector DisablePlayableDirector = default;

        private Tween PoinerEnterImageFade;

        protected override void Start()
        {
            PoinerEnterImage.SetAlpha(0);

            this.OnPointerEnterAsObservable()
                .Subscribe(_ => PoinerEnterImage.SetAlpha(1))
                .AddTo(this);

            this.OnPointerExitAsObservable()
                .Subscribe(_ => PoinerEnterImage.DOFade(0, 0.5f).Play())
                .AddTo(this);
        }

        public void Enable()
        {
            if (!Image.raycastTarget)
            {
                Image.raycastTarget = true;
                EnablePlayableDirector.Rewind();
                EnablePlayableDirector.Play();
            }
        }

        public void Disable()
        {
            if (Image.raycastTarget)
            {
                Image.raycastTarget = false;
                DisablePlayableDirector.Rewind();
                DisablePlayableDirector.Play();
            }
        }

        public IObservable<string> OnDownAsObservable()
            => this.OnPointerDownAsObservable().Select(_ => TextMeshPro.text);
    }
}