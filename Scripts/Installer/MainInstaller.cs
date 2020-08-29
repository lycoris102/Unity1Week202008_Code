using System.Collections.Generic;
using Lycoris102.Unity1Week202008.Entity;
using Lycoris102.Unity1Week202008.Master;
using Lycoris102.Unity1Week202008.UseCase.Main;
using Lycoris102.Unity1Week202008.View.Main;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.Installer
{
    public class MainInstaller : LifetimeScope
    {
        // Photon
        [SerializeField] private PhotonPlayer PhotonPlayer = default;
        [SerializeField] private PhotonMainState PhotonMainState = default;
        [SerializeField] private PhotonStageReady PhotonStageReady = default;
        [SerializeField] private PhotonStageResult PhotonStageResult = default;
        [SerializeField] private PhotonChat PhotonChat = default;

        // MainState
        [SerializeField] private List<MainStateTimeline> MainStateTimelineList = default;

        // Wait
        [SerializeField] private WaitPlayerList WaitPlayerList = default;
        [SerializeField] private WaitLeaveRoomButton WaitLeaveRoomButton = default;

        // StageReady
        [SerializeField] private Answer Answer = default;
        [SerializeField] private AnswerList AnswerList = default;
        [SerializeField] private StageReadyPlayerList StageReadyPlayerList = default;
        [SerializeField] private List<Stage> StageList = default;

        // StagePlay
        [SerializeField] private List<KeyboardKey> KeyboardKeyList = default;
        [SerializeField] private KeyboardForm KeyboardForm = default;
        [SerializeField] private KeyboardDeleteButton KeyboardDeleteButton = default;
        [SerializeField] private KeyboardSendButton KeyboardSendButton = default;
        [SerializeField] private KeyboardList KeyboardList = default;

        [SerializeField] private Timer Timer = default;
        [SerializeField] private int ChatMaxLength = default;
        [SerializeField] private UnlockKeyList UnlockKeyList = default;

        // StageResult
        [SerializeField] private StageResult StageResult = default;
        [SerializeField] private StageResultTimeline StageResultTimeline = default;
        [SerializeField] private List<StageResultList> StageResultList = default;

        // Result
        [SerializeField] private ResultRankList ResultRankList = default;
        [SerializeField] private ResultTotalTime ResultTotalTime = default;
        [SerializeField] private ResultRankText ResultRankText = default;

        // Info
        [SerializeField] private Info Info = default;

        // Other
        [SerializeField] private EffectCracker EffectCracker = default;
        [SerializeField] private TweetButton TweetButton = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // Photon
            builder.RegisterInstance(PhotonPlayer).AsImplementedInterfaces();
            builder.RegisterInstance(PhotonMainState).AsImplementedInterfaces();
            builder.RegisterInstance(PhotonStageReady).AsImplementedInterfaces();
            builder.RegisterInstance(PhotonStageResult).AsImplementedInterfaces();
            builder.RegisterInstance(PhotonChat).AsImplementedInterfaces();

            // MainState
            builder.Register<MainStateEntity>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<MainStateUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            foreach (var mainStateTimeline in MainStateTimelineList)
                builder.RegisterInstance(mainStateTimeline).AsImplementedInterfaces();

            // Wait
            builder.Register<WaitUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerEntity>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterInstance(WaitPlayerList).AsImplementedInterfaces();
            builder.RegisterInstance(WaitLeaveRoomButton).AsImplementedInterfaces();

            // Ready
            builder.Register<ReadyUseCase>(Lifetime.Scoped).AsImplementedInterfaces();

            // StageReady
            builder.Register<StageEntity>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<StageReadyUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterInstance(AnswerList);
            builder.RegisterInstance(Answer).AsImplementedInterfaces();
            builder.RegisterInstance(StageReadyPlayerList).AsImplementedInterfaces();
            foreach (var stage in StageList)
                builder.RegisterInstance(stage).AsImplementedInterfaces();

            // StagePlay
            builder.Register<KeyboardEntity>(Lifetime.Scoped)
                .WithParameter("maxLength", ChatMaxLength)
                .AsImplementedInterfaces();

            builder.Register<TimerEntity>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<StagePlayUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<KeyboardUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterInstance(KeyboardKeyList).AsImplementedInterfaces();
            builder.RegisterInstance(KeyboardForm).AsImplementedInterfaces();
            builder.RegisterInstance(KeyboardDeleteButton).AsImplementedInterfaces();
            builder.RegisterInstance(KeyboardSendButton).AsImplementedInterfaces();
            builder.RegisterInstance(KeyboardList).AsImplementedInterfaces();
            builder.RegisterInstance(Timer).AsImplementedInterfaces();
            builder.RegisterInstance(UnlockKeyList);
            foreach (var keyboardKeyList in KeyboardKeyList)
                builder.RegisterInstance(keyboardKeyList).AsImplementedInterfaces();

            // StageResult
            builder.Register<ResultEntity>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<StageResultUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterInstance(StageResult).AsImplementedInterfaces();
            builder.RegisterInstance(StageResultTimeline).AsImplementedInterfaces();
            foreach (var stageResultList in StageResultList)
                builder.RegisterInstance(stageResultList).AsImplementedInterfaces();

            // Result
            builder.Register<ResultUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterInstance(ResultRankList);
            builder.RegisterInstance(ResultTotalTime).AsImplementedInterfaces();
            builder.RegisterInstance(ResultRankText).AsImplementedInterfaces();

            // Error
            builder.Register<ErrorUseCase>(Lifetime.Scoped).AsImplementedInterfaces();

            // Info
            builder.Register<InfoEntity>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<InfoUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterInstance(Info).AsImplementedInterfaces();

            // Other
            builder.RegisterInstance(EffectCracker).AsImplementedInterfaces();
            builder.RegisterInstance(TweetButton).AsImplementedInterfaces();
        }
    }
}