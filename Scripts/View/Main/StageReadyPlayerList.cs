using System.Collections.Generic;
using Lycoris102.Unity1Week202008.Const;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using Photon.Realtime;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.View.Main
{
    public class StageReadyPlayerList : MonoBehaviour, IStageReadyPlayerListRenderer
    {
        [SerializeField] private List<StageReadyPlayerCell> WaitPlayerAnswererCellList = default;
        [SerializeField] private StageReadyPlayerCell WaitPlayerOwnerCell = default;

        public void Render(List<Player> playerList, int playerOwnerIndex, int localPlayerIndex)
        {
            var waitPlayerIndex = 0;
            for (int i = 0; i < playerList.Count; i++)
            {
                var player = playerList[i];
                var color = Setting.PlayerIndex2ColorMap[i];
                var isLocalPlayer = i == localPlayerIndex;
                if (i == playerOwnerIndex)
                {
                    // 出題者(Owner)は1人
                    WaitPlayerOwnerCell.Render(player, color, isLocalPlayer);
                }
                else
                {
                    WaitPlayerAnswererCellList[waitPlayerIndex].Render(player, color, isLocalPlayer);
                    waitPlayerIndex++;
                }
            }
        }
    }
}