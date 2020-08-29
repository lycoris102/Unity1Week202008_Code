namespace Lycoris102.Unity1Week202008.View.Main.Interface
{
    public interface IPhotonChatPrcRequester
    {
        void Request(string playerName, int playerIndex, string text, bool isOwner);
        void Delete();
    }
}