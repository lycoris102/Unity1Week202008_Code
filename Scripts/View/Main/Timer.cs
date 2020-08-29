using Lycoris102.Unity1Week202008.View.Main.Interface;
using TMPro;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class Timer : UIBehaviour, ITimeRenderer
    {
        private TextMeshProUGUI text;
        private TextMeshProUGUI Text => text ?? (text = GetComponent<TextMeshProUGUI>());

        public void Render(int time)
            => Text.text = time.ToString();
    }
}