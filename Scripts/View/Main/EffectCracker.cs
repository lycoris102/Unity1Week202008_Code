using System.Collections.Generic;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class EffectCracker : MonoBehaviour, IEffectCrackerRenderer
    {
        [SerializeField] private List<ParticleSystem> List = default;

        public void Play()
            => List.ForEach(x => x.Play());
    }
}