using DG.Tweening;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using TMPro;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class Info : UIBehaviour, IInfoRenderer
    {
        private TextMeshProUGUI text;
        private TextMeshProUGUI Text => text ?? (text = GetComponent<TextMeshProUGUI>());

        public void Render(string text)
        {
            Text.text = "";
            Text.DOText(text, text.Length * 0.01f).SetEase(Ease.Linear).Play();
        }
    }
}