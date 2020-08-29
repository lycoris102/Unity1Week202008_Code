using System;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.UseCase.Main.Interface;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using UniRx;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.UseCase.Main
{
    public class InfoUseCase : IInitializable, IDisposable, IInfoUseCase
    {
        private IInfoEntity InfoEntity { get; }
        private IInfoRenderer InfoRenderer { get; }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public void Dispose() => Disposable?.Clear();

        public InfoUseCase(IInfoEntity infoEntity, IInfoRenderer infoRenderer)
        {
            InfoEntity = infoEntity;
            InfoRenderer = infoRenderer;
        }

        public void Initialize()
        {
            InfoEntity.OnUpdateAsObservable()
                .Subscribe(Render)
                .AddTo(Disposable);
        }

        private void Render(string text)
            => InfoRenderer.Render(text);
    }
}