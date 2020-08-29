using Lycoris102.Unity1Week202008.View.Main.Interface;
using TMPro;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class Answer : MonoBehaviour, IAnswerRenderer
    {
        [SerializeField] private TextMeshProUGUI Text = default;

        public void Render(string answer, bool isHide)
            => Text.text = isHide ? "?" : answer;
    }
}