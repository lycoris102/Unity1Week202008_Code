using DG.Tweening;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using TMPro;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class ResultTotalTime : UIBehaviour, IResultTotalTimeRenderer
    {
        private TextMeshProUGUI text;
        private TextMeshProUGUI Text => text ?? (text = GetComponent<TextMeshProUGUI>());

        private int Time;

        public void Set(int time)
            => Time = time;

        // Signal経由で呼び出す
        public void Animation()
            => Text.DOCounter(0, Time, 1f).SetEase(Ease.Linear).Play();
    }
}