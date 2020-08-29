using Lycoris102.Unity1Week202008.Utility;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using UnityEngine;
using UnityEngine.Playables;

namespace Lycoris102.Unity1Week202008.View.Main
{
    [RequireComponent(typeof(PlayableDirector))]
    public class MainStateTimeline : MonoBehaviour, IMainStateRenderer
    {
        private PlayableDirector playableDirector;

        private PlayableDirector PlayableDirector =>
            playableDirector ?? (playableDirector = GetComponent<PlayableDirector>());

        [SerializeField] private MainState state = default;
        public MainState State => state;

        public void Render()
        {
            PlayableDirector.Rewind();
            PlayableDirector.Play();
        }
    }
}