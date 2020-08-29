using System;
using System.Collections.Generic;
using Lycoris102.Unity1Week202008.Const;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.Struct;
using Lycoris102.Unity1Week202008.UseCase.Main.Interface;
using Lycoris102.Unity1Week202008.Utility;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using Photon.Pun;
using UniRx;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.UseCase.Main
{
    public class StageResultUseCase : IInitializable, IDisposable, IStagePlayUseCase
    {
        private IAnswerRenderer AnswerRenderer { get; }
        private IEffectCrackerRenderer EffectCrackerRenderer { get; }
        private IInfoEntity InfoEntity { get; }
        private IMainStateEntity MainStateEntity { get; }
        private IMainStateRpcRequester MainStateRpcRequester { get; }
        private IPlayerEntity PlayerEntity { get; }
        private IReadOnlyList<IStageResultListRenderer> StageResultListRendererList { get; }
        private IResultEntity ResultEntity { get; }
        private IStageEntity StageEntity { get; }
        private IStageResultHandler StageResultHandler { get; }
        private IStageResultRenderer StageResultRenderer { get; }
        private IStageResultTimelineHandler StageResultTimelineHandler { get; }
        private IStageResultTimelineRenderer StageResultTimelineRenderer { get; }
        private ITimerEntity TimerEntity { get; }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public void Dispose() => Disposable?.Clear();

        public StageResultUseCase(IAnswerRenderer answerRenderer,
            IEffectCrackerRenderer effectCrackerRenderer,
            IInfoEntity infoEntity,
            IMainStateEntity mainStateEntity,
            IMainStateRpcRequester mainStateRpcRequester,
            IPlayerEntity playerEntity,
            IReadOnlyList<IStageResultListRenderer> stageResultListRendererList,
            IResultEntity resultEntity,
            IStageEntity stageEntity,
            IStageResultHandler stageResultHandler,
            IStageResultRenderer stageResultRenderer,
            IStageResultTimelineHandler stageResultTimelineHandler,
            IStageResultTimelineRenderer stageResultTimelineRenderer,
            ITimerEntity timerEntity)
        {
            AnswerRenderer = answerRenderer;
            EffectCrackerRenderer = effectCrackerRenderer;
            InfoEntity = infoEntity;
            MainStateEntity = mainStateEntity;
            MainStateRpcRequester = mainStateRpcRequester;
            PlayerEntity = playerEntity;
            StageResultListRendererList = stageResultListRendererList;
            ResultEntity = resultEntity;
            StageEntity = stageEntity;
            StageResultHandler = stageResultHandler;
            StageResultRenderer = stageResultRenderer;
            StageResultTimelineHandler = stageResultTimelineHandler;
            StageResultTimelineRenderer = stageResultTimelineRenderer;
            TimerEntity = timerEntity;
        }

        public void Initialize()
        {
            // 結果が送られてきたら格納/描画しておく
            StageResultHandler.OnSetAsObservable()
                .Subscribe(result =>
                {
                    StopTimer();
                    Set(result);
                    Render(result);
                    IncreaseStageCount();
                })
                .AddTo(Disposable);

            // Timelineの終了を受けて、次のステージに行くか、結果画面にいくか判定をする
            StageResultTimelineHandler.OnFinishAsObservable()
                .Subscribe(_ =>
                {
                    if (StageEntity.StageCount > Setting.RoomPlayerCount)
                    {
                        Finish();
                    }
                    else
                    {
                        NextStage();
                    }
                })
                .AddTo(Disposable);
        }

        private void IncreaseStageCount()
            => StageEntity.IncreaseStageCount();

        private void Set(ResultData result)
            => ResultEntity.Add(result);

        private void Render(ResultData result)
        {
            var playerIndex = result.CorrectPlayerIndex;
            var player = PlayerEntity.PlayerList[playerIndex];
            StageResultRenderer.Render(player.NickName, playerIndex, result.IsCorrect);

            // リストに追加する
            foreach (var stageResultListRenderer in StageResultListRendererList)
            {
                stageResultListRenderer.Render(result.Time, result.StageCount);
            }

            // 答えを表示する
            AnswerRenderer.Render(StageEntity.Answer, false);

            // Timelineを再生する
            StageResultTimelineRenderer.Play(result.IsCorrect);

            if (result.IsCorrect)
            {
                EffectCrackerRenderer.Play();
                InfoEntity.Set(Setting.CorrectInfoList.Random());
            }
            else
            {
                InfoEntity.Set(Setting.TimeoutInfoList.Random());
            }
        }

        private void StopTimer()
            => TimerEntity.Stop();

        private void NextStage()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                MainStateRpcRequester.Request(MainState.StageReady);
            }
        }

        private void Finish()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                MainStateRpcRequester.Request(MainState.Result);
            }
        }
    }
}