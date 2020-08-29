namespace Lycoris102.Unity1Week202008.View.Main.Interface
{
    public interface IStageResultRpcRequester
    {
        void Request(int stageCount, int time, int correctPlayerIndex, bool isCorrect);
    }
}