using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.Master
{
    [CreateAssetMenu(fileName = "UnlockKeyList", menuName = "Create UnlockKeyList", order = 0)]
    public class UnlockKeyList : ScriptableObject
    {
        public List<UnlockKey> List;
    }

    [Serializable]
    public class UnlockKey
    {
        public int ThresholdTime;
        public int Count;
    }
}