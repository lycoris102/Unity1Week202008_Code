using Lycoris102.Unity1Week202008.View.Main.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class KeyboardForm : UIBehaviour, IKeyboardFormRenderer
    {
        [SerializeField] private TextMeshProUGUI TextMeshPro = default;

        public void Render(string text)
            => TextMeshPro.text = text;
    }
}