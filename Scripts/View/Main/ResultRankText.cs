using Lycoris102.Unity1Week202008.View.Main.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class ResultRankText : UIBehaviour, IResultRankRenderer
    {
        [SerializeField] private TextMeshProUGUI RankText = default;
        [SerializeField] private TextMeshProUGUI CommentText = default;

        public void Render(string rank, string comment)
        {
            RankText.text = rank;
            CommentText.text = comment;
        }
    }
}