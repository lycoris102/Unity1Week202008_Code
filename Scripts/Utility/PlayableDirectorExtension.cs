using UnityEngine.Playables;

namespace Lycoris102.Unity1Week202008.Utility
{
    public static class PlayableDirectorExtension
    {
        public static void Rewind(this PlayableDirector playableDirector)
        {
            playableDirector.Stop();
            playableDirector.Evaluate();
        }
    }
}