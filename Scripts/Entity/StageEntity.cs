using Lycoris102.Unity1Week202008.Entity.Interface;

namespace Lycoris102.Unity1Week202008.Entity
{
    public class StageEntity : IStageEntity
    {
        public int StageCount { get; private set; } = 1;
        public string Answer { get; private set; }

        public void IncreaseStageCount()
            => StageCount++;

        public void Setup(string answer)
            => Answer = answer;
    }
}