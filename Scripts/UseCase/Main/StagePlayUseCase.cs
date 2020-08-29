using System;
using Lycoris102.Unity1Week202008.Const;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.UseCase.Main.Interface;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using Photon.Pun;
using UniRx;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.UseCase.Main
{
    public class StagePlayUseCase : IInitializable, IDisposable, IStagePlayUseCase
    {
        private IChatHandler ChatHandler;
        private IStageEntity StageEntity;
        private IPlayerEntity PlayerEntity;
        private IMainStateEntity MainStateEntity;
        private IMainStateRpcRequester MainStateRpcRequester { get; }
        private ITimerEntity TimerEntity { get; }
        private ITimeRenderer TimeRenderer { get; }
        private IStageResultRpcRequester StageResultRpcRequester { get; }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public void Dispose() => Disposable?.Clear();

        public StagePlayUseCase(IChatHandler chatHandler,
            IStageEntity stageEntity,
            IPlayerEntity playerEntity,
            IMainStateEntity mainStateEntity,
            IMainStateRpcRequester mainStateRpcRequester,
            ITimerEntity timerEntity,
            ITimeRenderer timeRenderer,
            IStageResultRpcRequester stageResultRpcRequester)
        {
            ChatHandler = chatHandler;
            StageEntity = stageEntity;
            PlayerEntity = playerEntity;
            MainStateEntity = mainStateEntity;
            MainStateRpcRequester = mainStateRpcRequester;
            TimerEntity = timerEntity;
            TimeRenderer = timeRenderer;
            StageResultRpcRequester = stageResultRpcRequester;
        }

        public void Initialize()
        {
            // Timerの管理はそれぞれで行ってみる
            // 何か不都合あれば、Masterクライアントから送るようにする
            MainStateEntity.OnChangeStateAsObservable(MainState.StagePlay)
                .Subscribe(_ => StartTimer())
                .AddTo(Disposable);

            // 時間を描画する
            TimerEntity.OnUpdateTimerAsObservable()
                .Subscribe(RenderTime)
                .AddTo(Disposable);

            // 時間が上限まで達していたら、強制的に終了する
            TimerEntity.OnUpdateTimerAsObservable()
                .Where(_ => PhotonNetwork.IsMasterClient)
                .Where(time => time >= Setting.TimeoutSeconds)
                .Subscribe(_ => Timeout())
                .AddTo(Disposable);

            // 正解時の挙動
            ChatHandler.OnChatAsObservable()
                .Where(_ => MainStateEntity.Check(MainState.StagePlay))
                .Where(_ => PhotonNetwork.IsMasterClient)
                .Where(tuple => !PlayerEntity.IsOwner(tuple.Item1))
                .Where(tuple => tuple.Item2 == StageEntity.Answer)
                .Subscribe(tuple => Correct(tuple.Item1))
                .AddTo(Disposable);
        }

        private void StartTimer()
            => TimerEntity.Start();

        private void RenderTime(int time)
            => TimeRenderer.Render(time);

        private void Timeout()
            => RequestResult(Setting.TimeoutSeconds, 0, false);

        private void Correct(int playerCorrectIndex)
        {
            PlayerEntity.SetPlayerCorrectIndex(playerCorrectIndex);
            RequestResult(TimerEntity.Time, playerCorrectIndex, true);
        }

        private void RequestResult(int time, int playerCorrectIndex, bool isCorrect)
        {
            StageResultRpcRequester.Request(StageEntity.StageCount, time, playerCorrectIndex, isCorrect);
            MainStateRpcRequester.Request(MainState.StageResult);
        }
    }
}