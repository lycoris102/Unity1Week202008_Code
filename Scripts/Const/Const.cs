using System.Collections.Generic;
using UnityEngine;

namespace Lycoris102.Unity1Week202008.Const
{
    public static class Setting
    {
        public static readonly int RoomPlayerCount = 3;
        public static readonly int TimeoutSeconds = 180;
        public static readonly int InitialEnableKeyCount = 8;
        public static readonly string DefaultComment = "よろしくね";
        public static readonly string UnityRoomGameId = "word_fragments";

        public static readonly Dictionary<int, Color> PlayerIndex2ColorMap = new Dictionary<int, Color>()
        {
            {0, new Color(0.4862745f, 0.5372549f, 0.6784314f, 1)},
            {1, new Color(0.4235294f, 0.6745098f, 0.2705882f, 1)},
            {2, new Color(0.8980392f, 0.4509804f, 0.4509804f, 1)},
        };

        public static readonly List<string> CorrectInfoList = new List<string>()
        {
            "お見事です！",
            "いい感じですね！",
            "素晴らしいです！",
            "ナイスです！",
        };

        public static readonly List<string> TimeoutInfoList = new List<string>()
        {
            "少し難しかったですね...",
            "惜しかったです！",
        };
    }
}