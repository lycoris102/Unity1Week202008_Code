namespace Lycoris102.Unity1Week202008.View.Common.Interface
{
    public interface IAudioPlayer
    {
        void Play(AudioType audioType, bool isContinue = true);
        void SetPitch(float pitch);
    }
}