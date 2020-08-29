using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class WaitPlayerCell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI NameText = default;
        [SerializeField] private TextMeshProUGUI CommentText = default;
        [SerializeField] private Image BaloonImage = default;

        public void Enable()
            => gameObject.SetActive(true);

        public void Disable()
            => gameObject.SetActive(false);

        public void Render(string nickName, Color color, string comment)
        {
            NameText.text = nickName;
            NameText.color = color;
            BaloonImage.color = color;
            CommentText.text = comment;
        }
    }
}