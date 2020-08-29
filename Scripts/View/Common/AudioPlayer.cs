using Lycoris102.Unity1Week202008.Master;
using Lycoris102.Unity1Week202008.View.Common.Interface;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.View.Common
{
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        private AudioSource audioSource;
        private AudioSource AudioSource => audioSource ?? (audioSource = GetComponent<AudioSource>());

        // XXX Viewが直接データ参照してて悩ましい
        [SerializeField] private AudioList AudioList = default;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void Play(AudioType audioType, bool isContinue)
        {
            var time = AudioSource.time;
            AudioSource.clip = AudioList.GetClipByType(audioType);
            if (isContinue) AudioSource.time = time;
            AudioSource.Play();
        }

        public void SetPitch(float pitch)
            => AudioSource.pitch = pitch;
    }
}