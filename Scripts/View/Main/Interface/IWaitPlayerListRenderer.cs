using System.Collections.Generic;
using Lycoris102.Unity1Week202008.Struct;

namespace Lycoris102.Unity1Week202008.View.Main.Interface
{
    public interface IWaitPlayerListRenderer
    {
        void Render(List<PlayerData> playerList);
        void PlaySE();
    }
}