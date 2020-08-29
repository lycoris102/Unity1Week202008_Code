namespace Lycoris102.Unity1Week202008.Entity.Interface
{
    public interface IStageEntity
    {
        int StageCount { get; }
        string Answer { get; }
        void IncreaseStageCount();
        void Setup(string answer);
    }
}