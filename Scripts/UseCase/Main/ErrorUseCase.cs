using System;
using Lycoris102.Unity1Week202008.Entity;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.UseCase.Main.Interface;
using Lycoris102.Unity1Week202008.View.Main;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using Photon.Pun;
using UniRx;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.UseCase.Main
{
    public class ErrorUseCase : IInitializable, IDisposable, IErrorUseCase
    {
        private IPhotonPlayerHandler PhotonPlayerHandler { get; }
        private IMainStateRpcRequester MainStateRpcRequester { get; }
        private IMainStateEntity MainStateEntity { get; }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public void Dispose() => Disposable?.Clear();

        public ErrorUseCase(IPhotonPlayerHandler photonPlayerHandler,
            IMainStateRpcRequester mainStateRpcRequester,
            IMainStateEntity mainStateEntity)
        {
            PhotonPlayerHandler = photonPlayerHandler;
            MainStateRpcRequester = mainStateRpcRequester;
            MainStateEntity = mainStateEntity;
        }

        public void Initialize()
        {
            PhotonPlayerHandler.OnLeftAsObservable()
                .Where(_ => !MainStateEntity.Check(MainState.Wait, MainState.Result))
                .Where(_ => PhotonNetwork.IsMasterClient)
                .Subscribe(_ => ChangeStateError())
                .AddTo(Disposable);

            MainStateEntity.OnChangeStateAsObservable(MainState.Error)
                .Subscribe(_ => Disconnect())
                .AddTo(Disposable);
        }

        private void ChangeStateError()
            => MainStateRpcRequester.Request(MainState.Error);

        private void Disconnect()
            => PhotonNetwork.LeaveRoom();
    }
}