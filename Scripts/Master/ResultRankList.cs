using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.Master
{
    [CreateAssetMenu(fileName = "ResultRankList", menuName = "Create ResultRankList", order = 0)]
    public class ResultRankList : ScriptableObject
    {
        public List<ResultRank> List;

        public ResultRank GetResultRankByTime(int time)
        {
            return List.OrderBy(x => x.ThresholdTime)
                .FirstOrDefault(x => time < x.ThresholdTime);
        }
    }

    [Serializable]
    public class ResultRank
    {
        public int ThresholdTime;
        public string Rank;
        public string Comment;
    }
}