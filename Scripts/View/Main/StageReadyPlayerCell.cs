using DG.Tweening;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class StageReadyPlayerCell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI NameText = default;
        [SerializeField] private Image Image = default;

        public void Render(Player player, Color color, bool isLocalPlayer)
        {
            NameText.text = player.NickName;
            Image.color = color;
            if (isLocalPlayer)
            {
                transform.DOScale(1.2f, 0.5f)
                    .SetLoops(20, LoopType.Yoyo)
                    .Play();
            }
        }
    }
}