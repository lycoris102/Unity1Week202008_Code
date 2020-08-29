using System;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.UseCase.Main.Interface;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using UniRx;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.UseCase.Main
{
    public class ReadyUseCase : IInitializable, IDisposable, IReadyUseCase
    {
        private IEffectCrackerRenderer EffectCrackerRenderer { get; }
        private IMainStateEntity MainStateEntity { get; }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public void Dispose() => Disposable?.Clear();

        public ReadyUseCase(IEffectCrackerRenderer effectCrackerRenderer, IMainStateEntity mainStateEntity)
        {
            EffectCrackerRenderer = effectCrackerRenderer;
            MainStateEntity = mainStateEntity;
        }

        public void Initialize()
        {
            MainStateEntity.OnChangeStateAsObservable(MainState.Ready)
                .Subscribe(_ => Render())
                .AddTo(Disposable);
        }

        private void Render()
            => EffectCrackerRenderer.Play();
    }
}