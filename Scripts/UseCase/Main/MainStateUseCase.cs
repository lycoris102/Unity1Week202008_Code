using System;
using System.Collections.Generic;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.UseCase.Main.Interface;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using UniRx;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.UseCase.Main
{
    public class MainStateUseCase : IInitializable, IDisposable, IMainStateUseCase
    {
        private IMainStateEntity MainStateEntity { get; }
        private IMainStateHandler MainStateHandler { get; }
        private IReadOnlyList<IMainStateRenderer> MainStateRendererList { get; }

        public MainStateUseCase(IMainStateEntity mainStateEntity,
            IReadOnlyList<IMainStateRenderer> mainStateRendererList,
            IMainStateHandler mainStateHandler)
        {
            MainStateEntity = mainStateEntity;
            MainStateRendererList = mainStateRendererList;
            MainStateHandler = mainStateHandler;
        }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public void Dispose() => Disposable?.Clear();

        public void Initialize()
        {
            MainStateHandler.OnChangeAsObservable()
                .Subscribe(Change)
                .AddTo(Disposable);

            MainStateEntity.OnChangeStateAsObservable()
                .Subscribe(Render)
                .AddTo(Disposable);
        }

        private void Change(MainState state)
            => MainStateEntity.Change(state);

        private void Render(MainState state)
        {
            foreach (var mainStateRenderer in MainStateRendererList)
            {
                if (mainStateRenderer.State == state) mainStateRenderer.Render();
            }
        }
    }
}