using Lycoris102.Unity1Week202008.View.Main.Interface;
using TMPro;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class Stage : UIBehaviour, IStageRenderer
    {
        private TextMeshProUGUI text;
        private TextMeshProUGUI Text => text ?? (text = GetComponent<TextMeshProUGUI>());

        public void Render(int stage)
            => Text.text = $"ステージ{stage}";
    }
}