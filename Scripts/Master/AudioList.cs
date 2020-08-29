using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.Master
{
    [CreateAssetMenu(fileName = "AudioList", menuName = "Create AudioList", order = 0)]
    public class AudioList : ScriptableObject
    {
        public List<AudioData> List;

        private Dictionary<AudioType, AudioClip> type2ClipMap = null;

        private Dictionary<AudioType, AudioClip> Type2ClipMap =>
            type2ClipMap ?? (type2ClipMap = List.ToDictionary(x => x.AudioType, x => x.AudioClip));

        public AudioClip GetClipByType(AudioType audioType)
            => Type2ClipMap[audioType];
    }

    [Serializable]
    public class AudioData
    {
        public AudioType AudioType;
        public AudioClip AudioClip;
    }
}