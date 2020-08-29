using System;
using System.Linq;
using Lycoris102.Unity1Week202008.Const;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.Master;
using Lycoris102.Unity1Week202008.UseCase.Main.Interface;
using Lycoris102.Unity1Week202008.View.Common.Interface;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using naichilab;
using Photon.Pun;
using UniRx;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.UseCase.Main
{
    public class ResultUseCase : IInitializable, IDisposable, IResultUseCase
    {
        private IAudioPlayer AudioPlayer { get; }
        private IInfoEntity InfoEntity { get; }
        private IMainStateEntity MainStateEntity { get; }
        private IResultEntity ResultEntity { get; }
        private IResultRankRenderer ResultRankRenderer { get; }
        private IResultTotalTimeRenderer ResultTotalTimeRenderer { get; }
        private ITweetButtonHandler TweetButtonHandler { get; }
        private IPlayerEntity PlayerEntity { get; }
        private ResultRankList ResultRankList { get; }

        public ResultUseCase(IAudioPlayer audioPlayer,
            IInfoEntity infoEntity,
            IMainStateEntity mainStateEntity,
            IResultEntity resultEntity,
            IResultRankRenderer resultRankRenderer,
            IResultTotalTimeRenderer resultTotalTimeRenderer,
            ITweetButtonHandler tweetButtonHandler,
            IPlayerEntity playerEntity,
            ResultRankList resultRankList)
        {
            AudioPlayer = audioPlayer;
            InfoEntity = infoEntity;
            MainStateEntity = mainStateEntity;
            ResultEntity = resultEntity;
            ResultRankRenderer = resultRankRenderer;
            ResultTotalTimeRenderer = resultTotalTimeRenderer;
            TweetButtonHandler = tweetButtonHandler;
            PlayerEntity = playerEntity;
            ResultRankList = resultRankList;
        }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public void Dispose() => Disposable?.Clear();

        public void Initialize()
        {
            MainStateEntity.OnChangeStateAsObservable(MainState.Result)
                .Subscribe(_ =>
                {
                    Disconnect();
                    Render();
                })
                .AddTo(Disposable);

            TweetButtonHandler.OnDownAsObservable()
                .Subscribe(_ => Tweet())
                .AddTo(Disposable);
        }

        private void Disconnect()
            => PhotonNetwork.LeaveRoom();

        private void Render()
        {
            AudioPlayer.Play(AudioType.Main4);

            var totalTime = ResultEntity.CalcTotalTime();
            var resultRank = ResultRankList.GetResultRankByTime(totalTime);
            ResultTotalTimeRenderer.Set(totalTime);
            ResultRankRenderer.Render(resultRank.Rank, resultRank.Comment);
            InfoEntity.Set("お疲れ様でした！ また一緒に遊んでくださいね");
        }

        private void Tweet()
        {
            var totalTime = ResultEntity.CalcTotalTime();
            var resultRank = ResultRankList.GetResultRankByTime(totalTime).Rank;
            var nicknameList = PlayerEntity.PlayerList.Select(x => x.NickName).ToList();
            var body =
                $"【超ハイコンテキスト・オンラインクイズゲーム】\nもじもじフラグメンツで遊んだよ！\n\n【結果】\n時間: {totalTime}秒 / ランク: {resultRank}\n\n【メンバー】\n";

            foreach (var nickName in nicknameList)
            {
                body += $"・{nickName}\n";
            }

            body += "\n";
            UnityRoomTweet.Tweet(Setting.UnityRoomGameId, body, "unity1week", "もじフラ");
        }
    }
}