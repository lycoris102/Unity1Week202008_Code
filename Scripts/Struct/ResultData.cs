namespace Lycoris102.Unity1Week202008.Struct
{
    public struct ResultData
    {
        public int StageCount;
        public int Time;
        public int CorrectPlayerIndex;
        public bool IsCorrect;

        public ResultData(int stageCount,
            int time,
            int correctPlayerIndex,
            bool isCorrect)
        {
            StageCount = stageCount;
            Time = time;
            CorrectPlayerIndex = correctPlayerIndex;
            IsCorrect = isCorrect;
        }
    }
}