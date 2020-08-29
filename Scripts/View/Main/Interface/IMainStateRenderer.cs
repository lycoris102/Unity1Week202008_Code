namespace Lycoris102.Unity1Week202008.View.Main.Interface
{
    public interface IMainStateRenderer
    {
        void Render();
        MainState State { get; }
    }
}