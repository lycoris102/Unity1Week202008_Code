using TMPro;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class StageResultCell : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private TextMeshProUGUI Text => text ?? (text = GetComponent<TextMeshProUGUI>());

        public void Render(int time)
            => Text.text = $"{time}秒";
    }
}