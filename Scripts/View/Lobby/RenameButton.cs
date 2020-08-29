using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    public class RenameButton : UIBehaviour
    {
        [SerializeField] private GameObject RenameObject = default;

        protected override void Start()
        {
            this.OnPointerDownAsObservable()
                .Subscribe(_ => RenameObject.SetActive(true))
                .AddTo(this);
        }
    }
}