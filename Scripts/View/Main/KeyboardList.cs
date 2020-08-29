using Lycoris102.Unity1Week202008.View.Main.Interface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class KeyboardList : UIBehaviour, IKeyboardListRenderer
    {
        private AudioSource audioSource;
        private AudioSource AudioSource => audioSource ?? (audioSource = GetComponent<AudioSource>());

        public void PlayEnableSound()
            => AudioSource.Play();
    }
}