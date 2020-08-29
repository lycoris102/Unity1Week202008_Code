namespace Lycoris102.Unity1Week202008.Struct
{
    public struct PlayerData
    {
        public int Index;
        public string Nickname;
        public string Comment;

        public PlayerData(int index, string nickname, string comment)
        {
            Index = index;
            Nickname = nickname;
            Comment = comment;
        }
    }
}