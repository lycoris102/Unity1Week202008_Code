using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    public class TutorialOpenButton : UIBehaviour
    {
        [SerializeField] private GameObject TutorialObject = default;

        protected override void Start()
        {
            this.OnPointerDownAsObservable()
                .Subscribe(_ => TutorialObject.SetActive(true))
                .AddTo(this);
        }
    }
}