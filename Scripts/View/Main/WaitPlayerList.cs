using System.Collections.Generic;
using Lycoris102.Unity1Week202008.Const;
using Lycoris102.Unity1Week202008.Struct;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class WaitPlayerList : MonoBehaviour, IWaitPlayerListRenderer
    {
        [SerializeField] private List<WaitPlayerCell> WaitPlayerCellList = default;

        // 責務的にここじゃなさそう
        private AudioSource audioSource;
        private AudioSource AudioSource => audioSource ?? (audioSource = GetComponent<AudioSource>());

        public void Render(List<PlayerData> playerList)
        {
            foreach (var waitPlayerCell in WaitPlayerCellList)
            {
                waitPlayerCell.Disable();
            }

            for (int i = 0; i < playerList.Count; i++)
            {
                var player = playerList[i];
                WaitPlayerCellList[i].Enable();
                WaitPlayerCellList[i].Render(player.Nickname, Setting.PlayerIndex2ColorMap[i], player.Comment);
            }
        }

        public void PlaySE()
            => AudioSource.PlayOneShot(AudioSource.clip);
    }
}