using UnityEngine;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    // TutorialPagerで対応する
    public class TutorialPagerButton : UIBehaviour
    {
        private CanvasGroup canvasGroup = default;
        private CanvasGroup CanvasGroup => canvasGroup ?? GetComponent<CanvasGroup>();

        public void Enable(bool enabled)
        {
            CanvasGroup.blocksRaycasts = enabled;
            CanvasGroup.alpha = enabled ? 1f : 0f;
        }
    }
}