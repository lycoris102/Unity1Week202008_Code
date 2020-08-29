using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    public class TutorialPager : UIBehaviour
    {
        [SerializeField] private List<Sprite> SpriteList = default;
        [SerializeField] private Image Image = default;
        [SerializeField] private TutorialPagerButton TutorialPagerNextButton = default;
        [SerializeField] private TutorialPagerButton TutorialPagerPrevButton = default;
        private int Index;
        private int MaxPageIndex => SpriteList.Count - 1;

        protected override void Start()
        {
            TutorialPagerNextButton.OnPointerDownAsObservable()
                .ThrottleFirst(TimeSpan.FromMilliseconds(300))
                .Subscribe(_ =>
                {
                    UpdateIndex(Index + 1);
                    Render();
                })
                .AddTo(this);

            TutorialPagerPrevButton.OnPointerDownAsObservable()
                .ThrottleFirst(TimeSpan.FromMilliseconds(300))
                .Subscribe(_ =>
                {
                    UpdateIndex(Index - 1);
                    Render();
                })
                .AddTo(this);
        }

        protected override void OnEnable()
        {
            UpdateIndex(0);
            Render();
        }

        private void UpdateIndex(int index)
            => Index = Mathf.Clamp(index, 0, SpriteList.Count - 1);

        private void Render()
        {
            TutorialPagerNextButton.Enable(Index < MaxPageIndex);
            TutorialPagerPrevButton.Enable(Index > 0);
            Image.sprite = SpriteList[Index];
        }
    }
}