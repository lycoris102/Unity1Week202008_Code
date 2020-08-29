using System.Collections.Generic;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class StageResultList : MonoBehaviour, IStageResultListRenderer
    {
        [SerializeField] private List<StageResultCell> StageResultCellList = default;

        public void Render(int time, int stageCount)
            => StageResultCellList[stageCount - 1].Render(time);
    }
}