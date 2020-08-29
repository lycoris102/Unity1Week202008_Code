using System;
using System.Collections.Generic;
using System.Linq;
using Lycoris102.Unity1Week202008.Const;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.Master;
using Lycoris102.Unity1Week202008.UseCase.Main.Interface;
using Lycoris102.Unity1Week202008.View.Common.Interface;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using Photon.Pun;
using UniRx;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.UseCase.Main
{
    public class StageReadyUseCase : IInitializable, IDisposable, IStageReadyUseCase
    {
        private AnswerList AnswerList { get; }
        private IAnswerRenderer AnswerRenderer { get; }
        private IAudioPlayer AudioPlayer { get; }
        private IInfoEntity InfoEntity { get; }
        private IKeyboardEntity KeyboardEntity { get; }
        private IMainStateEntity MainStateEntity { get; }
        private IPhotonChatPrcRequester PhotonChatPrcRequester { get; }
        private IPlayerEntity PlayerEntity { get; }
        private IReadOnlyList<IKeyboardKeyRenderer> KeyboardKeyRendererList { get; }
        private IReadOnlyList<IStageRenderer> StageRendererList { get; }
        private IStageEntity StageEntity { get; }
        private IStageReadyHandler StageReadyHandler { get; }
        private IStageReadyPlayerListRenderer StageReadyPlayerListRenderer { get; }
        private IStageReadyRpcRequester StageReadyRpcRequester { get; }
        private ITimerEntity TimerEntity { get; }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public void Dispose() => Disposable?.Clear();

        public StageReadyUseCase(AnswerList answerList,
            IAnswerRenderer answerRenderer,
            IAudioPlayer audioPlayer,
            IInfoEntity infoEntity,
            IKeyboardEntity keyboardEntity,
            IMainStateEntity mainStateEntity,
            IPhotonChatPrcRequester photonChatPrcRequester,
            IPlayerEntity playerEntity,
            IReadOnlyList<IKeyboardKeyRenderer> keyboardKeyRendererList,
            IReadOnlyList<IStageRenderer> stageRendererList,
            IStageEntity stageEntity,
            IStageReadyHandler stageReadyHandler,
            IStageReadyPlayerListRenderer stageReadyPlayerListRenderer,
            IStageReadyRpcRequester stageReadyRpcRequester,
            ITimerEntity timerEntity)
        {
            AnswerList = answerList;
            AnswerRenderer = answerRenderer;
            AudioPlayer = audioPlayer;
            InfoEntity = infoEntity;
            KeyboardEntity = keyboardEntity;
            MainStateEntity = mainStateEntity;
            PhotonChatPrcRequester = photonChatPrcRequester;
            PlayerEntity = playerEntity;
            KeyboardKeyRendererList = keyboardKeyRendererList;
            StageRendererList = stageRendererList;
            StageEntity = stageEntity;
            StageReadyHandler = stageReadyHandler;
            StageReadyPlayerListRenderer = stageReadyPlayerListRenderer;
            StageReadyRpcRequester = stageReadyRpcRequester;
            TimerEntity = timerEntity;
        }

        public void Initialize()
        {
            MainStateEntity.OnChangeStateAsObservable(MainState.StageReady)
                .Subscribe(_ =>
                {
                    Setup();
                    Render();
                })
                .AddTo(Disposable);

            // RPC経由で全ユーザに答えをセットする
            StageReadyHandler.OnSetAnswerAsObservable()
                .Delay(TimeSpan.FromMilliseconds(300))
                .Subscribe(answer =>
                {
                    SetAnswer(answer);
                    SetInfo();
                })
                .AddTo(Disposable);
        }

        private void Setup()
        {
            // BGM変更
            AudioPlayer.Play(StageEntity.StageCount >= Setting.RoomPlayerCount
                ? AudioType.Main3
                : AudioType.Main2);

            // タイマー初期化
            TimerEntity.Reset();

            // チャット欄を削除しておく
            PhotonChatPrcRequester.Delete();

            // Owner更新
            PlayerEntity.SetPlayerOwnerIndex(StageEntity.StageCount - 1);

            // Masterが抽選をして答えをランダムにセットするように要求
            if (PhotonNetwork.IsMasterClient)
            {
                StageReadyRpcRequester.Request(AnswerList.GetRandom());
            }

            if (PlayerEntity.IsOwner(PhotonNetwork.LocalPlayer))
            {
                // 出題者は限られた文字しか使えない
                KeyboardEntity.InitializeOwner(KeyboardKeyRendererList.Count);
                var disableIndexList = Enumerable
                    .Range(0, KeyboardKeyRendererList.Count)
                    .Except(KeyboardEntity.EnableOwner(Setting.InitialEnableKeyCount));

                foreach (var index in disableIndexList)
                {
                    KeyboardKeyRendererList[index].Disable();
                }
            }
            else
            {
                foreach (var keyboardKeyRenderer in KeyboardKeyRendererList)
                {
                    keyboardKeyRenderer.Enable();
                }
            }
        }

        private void SetAnswer(string answer)
        {
            StageEntity.Setup(answer);
            AnswerRenderer.Render(answer, !PlayerEntity.IsOwner(PhotonNetwork.LocalPlayer));
        }

        private void SetInfo()
        {
            if (PlayerEntity.IsOwner(PhotonNetwork.LocalPlayer))
            {
                InfoEntity.Set($"あなたは「出題者」です 限られた文字で「{StageEntity.Answer}」を表現しましょう");
            }
            else
            {
                InfoEntity.Set($"あなたは「回答者」です 出題者に質問をして「答え」を導きましょう");
            }
        }

        private void Render()
        {
            foreach (var stageRenderer in StageRendererList)
            {
                stageRenderer.Render(StageEntity.StageCount);
            }

            var localPlayerIndex = PlayerEntity.GetIndexByPlayer(PhotonNetwork.LocalPlayer);
            StageReadyPlayerListRenderer.Render(PlayerEntity.PlayerList, PlayerEntity.PlayerOwnerIndex,
                localPlayerIndex);
        }
    }
}