using System;
using System.Linq;
using Lycoris102.Unity1Week202008.Const;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.Struct;
using Lycoris102.Unity1Week202008.UseCase.Main.Interface;
using Lycoris102.Unity1Week202008.View.Common.Interface;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.UseCase.Main
{
    public class WaitUseCase : IInitializable, IDisposable, IWaitUseCase
    {
        private IAudioPlayer AudioPlayer { get; }
        private IInfoEntity InfoEntity { get; }
        private IMainStateEntity MainStateEntity { get; }
        private IMainStateRpcRequester MainStateRpcRequester { get; }
        private IPhotonPlayerHandler PhotonPlayerHandler { get; }
        private IPlayerEntity PlayerEntity { get; }
        private IWaitLeaveRoomButtonHandler WaitLeaveRoomButtonHandler { get; }
        private IWaitPlayerListRenderer WaitPlayerListRenderer { get; }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public void Dispose() => Disposable?.Clear();

        public WaitUseCase(IAudioPlayer audioPlayer,
            IInfoEntity infoEntity,
            IMainStateEntity mainStateEntity,
            IMainStateRpcRequester mainStateRpcRequester,
            IPhotonPlayerHandler photonPlayerHandler,
            IPlayerEntity playerEntity,
            IWaitLeaveRoomButtonHandler waitLeaveRoomButtonHandler,
            IWaitPlayerListRenderer waitPlayerListRenderer)
        {
            AudioPlayer = audioPlayer;
            InfoEntity = infoEntity;
            MainStateEntity = mainStateEntity;
            MainStateRpcRequester = mainStateRpcRequester;
            PhotonPlayerHandler = photonPlayerHandler;
            PlayerEntity = playerEntity;
            WaitLeaveRoomButtonHandler = waitLeaveRoomButtonHandler;
            WaitPlayerListRenderer = waitPlayerListRenderer;
        }

        public void Initialize()
        {
            AudioPlayer.Play(AudioType.Main1);

            WaitLeaveRoomButtonHandler.OnDownAsObservable()
                .Where(_ => MainStateEntity.Check(MainState.Wait))
                .Subscribe(_ => LeaveRoom())
                .AddTo(Disposable);

            PhotonPlayerHandler.OnEnterAsObservable()
                .Where(_ => MainStateEntity.Check(MainState.Wait))
                .Subscribe(player => SetInfo($"{player.NickName} さんがルームに参加しました"))
                .AddTo(Disposable);

            // 待機中にプレイヤーの情報に変更があれば描画する
            Observable.Merge(
                    PhotonPlayerHandler.OnEnterAsObservable().AsUnitObservable(),
                    PhotonPlayerHandler.OnLeftAsObservable().AsUnitObservable(),
                    PhotonPlayerHandler.OnUpdateCustomPropertyAsObservable().AsUnitObservable()
                )
                .Where(_ => MainStateEntity.Check(MainState.Wait))
                .Subscribe(_ =>
                {
                    OnUpdatedPlayerList();
                    PlaySE();
                })
                .AddTo(Disposable);

            // 人数が上限に達したらホストが状態変更のリクエストを投げる
            PhotonPlayerHandler.OnEnterAsObservable()
                .Where(_ => PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
                .Where(_ => PhotonNetwork.MasterClient.UserId != null)
                .Where(_ => PhotonNetwork.IsMasterClient)
                .Subscribe(_ => ChangeReadyStateRequest())
                .AddTo(Disposable);

            PhotonPlayerHandler.OnLeftAsObservable()
                .Where(_ => MainStateEntity.Check(MainState.Wait))
                .Subscribe(player => SetInfo($"{player.NickName} さんがルームから退出しました"))
                .AddTo(Disposable);

            OnUpdatedPlayerList();
            SetInfo("3人揃うまでお待ちください コメントを変更することができます");
        }

        private void LeaveRoom()
        {
            if (!PhotonNetwork.InRoom) return;
            PhotonNetwork.LeaveRoom();
        }

        private void OnUpdatedPlayerList()
        {
            UpdatePlayerList();
            Render();
        }

        private void PlaySE()
            => WaitPlayerListRenderer.PlaySE();

        private void UpdatePlayerList()
            => PlayerEntity
                .SetPlayerList(PhotonNetwork.CurrentRoom.Players.Values.OrderBy(x => x.ActorNumber)
                    .ToList());

        private void Render()
            => WaitPlayerListRenderer.Render(PlayerEntity
                .PlayerList
                .Select((x, index) => new PlayerData(index, x.NickName, GetComment(x))).ToList());

        private void ChangeReadyStateRequest()
            => MainStateRpcRequester.Request(MainState.Ready);

        private string GetComment(Player player)
        {
            if (player.CustomProperties.TryGetValue("Comment", out object commentObject))
            {
                return (string) commentObject;
            }

            return Setting.DefaultComment;
        }

        private void SetInfo(string text)
            => InfoEntity.Set(text);
    }
}