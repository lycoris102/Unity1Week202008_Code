using System.Collections.Generic;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.Master
{
    [CreateAssetMenu(fileName = "AnswerList", menuName = "Create AnswerList", order = 0)]
    public class AnswerList : ScriptableObject
    {
        public List<string> List;

        // 余力があれば重複弾く
        public string GetRandom()
            => List[Random.Range(0, List.Count)];
    }
}