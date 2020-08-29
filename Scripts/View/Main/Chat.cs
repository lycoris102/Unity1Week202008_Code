using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class Chat : MonoBehaviour
    {
        [SerializeField] private GameObject PlayerAnswerObject = default;
        [SerializeField] private GameObject PlayerOwnerObject = default;
        [SerializeField] private TextMeshProUGUI PlayerAnswerText = default;
        [SerializeField] private TextMeshProUGUI PlayerAnswerLabelText = default;
        [SerializeField] private TextMeshProUGUI PlayerAnswerNameText = default;
        [SerializeField] private Image PlayerAnswerBaloonImage = default;
        [SerializeField] private TextMeshProUGUI PlayerOwnerText = default;
        [SerializeField] private TextMeshProUGUI PlayerOwnerLabelText = default;
        [SerializeField] private TextMeshProUGUI PlayerOwnerNameText = default;
        [SerializeField] private Image PlayerOwnerBaloonImage = default;

        public void Render(string playerName, Color color, string text, bool isOwner)
        {
            if (isOwner)
            {
                PlayerOwnerNameText.text = playerName;
                PlayerOwnerNameText.color = color;
                PlayerOwnerLabelText.color = color;
                PlayerOwnerText.text = text;
                PlayerOwnerBaloonImage.color = color;
                PlayerOwnerObject.SetActive(true);
                PlayerAnswerObject.SetActive(false);
            }
            else
            {
                PlayerAnswerNameText.text = playerName;
                PlayerAnswerNameText.color = color;
                PlayerAnswerLabelText.color = color;
                PlayerAnswerText.text = text;
                PlayerAnswerBaloonImage.color = color;
                PlayerAnswerObject.SetActive(true);
                PlayerOwnerObject.SetActive(false);
            }
        }
    }
}