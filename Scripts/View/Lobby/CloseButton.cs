using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    public class CloseButton : UIBehaviour
    {
        [SerializeField] private GameObject GameObject = default;

        protected override void Start()
        {
            this.OnPointerDownAsObservable()
                .DelayFrame(1)
                .Subscribe(_ => GameObject.SetActive(false))
                .AddTo(this);
        }
    }
}