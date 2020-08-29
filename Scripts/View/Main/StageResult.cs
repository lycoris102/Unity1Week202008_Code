using Lycoris102.Unity1Week202008.Const;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class StageResult : MonoBehaviour, IStageResultRenderer
    {
        [SerializeField] private GameObject CorrectObject = default;
        [SerializeField] private GameObject TimeoutObject = default;
        [SerializeField] private TextMeshProUGUI PlayerNameText = default;
        [SerializeField] private Image PlayerImage = default;

        public void Render(string playerName, int playerIndex, bool isCorrect)
        {
            if (isCorrect)
            {
                CorrectObject.SetActive(true);
                TimeoutObject.SetActive(false);
                PlayerNameText.text = playerName;
                PlayerImage.color = Setting.PlayerIndex2ColorMap[playerIndex];
            }
            else
            {
                CorrectObject.SetActive(false);
                TimeoutObject.SetActive(true);
            }
        }
    }
}