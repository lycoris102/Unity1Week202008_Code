using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Lycoris102.Unity1Week202008.Entity.Interface;
using Lycoris102.Unity1Week202008.Master;
using Lycoris102.Unity1Week202008.UseCase.Main.Interface;
using Lycoris102.Unity1Week202008.View.Main.Interface;
using Photon.Pun;
using UniRx;
using VContainer.Unity;

namespace Lycoris102.Unity1Week202008.UseCase.Main
{
    public class KeyboardUseCase : IInitializable, IDisposable, IKeyboardUseCase
    {
        private IInfoEntity InfoEntity { get; }
        private IKeyboardDeleteButtonHandler KeyboardDeleteButtonHandler { get; }
        private IKeyboardEntity KeyboardEntity { get; }
        private IKeyboardFormRenderer KeyboardFormRenderer { get; }
        private IKeyboardListRenderer KeyboardListRenderer { get; }
        private IKeyboardSendButtonHandler KeyboardSendButtonHandler { get; }
        private IMainStateEntity MainStateEntity { get; }
        private IPhotonChatPrcRequester PhotonChatPrcRequester { get; }
        private IPlayerEntity PlayerEntity { get; }
        private IReadOnlyList<IKeyboardKeyHandler> KeyboardKeyHandlerList { get; }
        private IReadOnlyList<IKeyboardKeyRenderer> KeyboardKeyRendererList { get; }
        private ITimerEntity TimerEntity { get; }
        private UnlockKeyList UnlockKeyList { get; }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public void Dispose() => Disposable?.Clear();

        public KeyboardUseCase(IInfoEntity infoEntity,
            IKeyboardDeleteButtonHandler keyboardDeleteButtonHandler,
            IKeyboardEntity keyboardEntity,
            IKeyboardFormRenderer keyboardFormRenderer,
            IKeyboardListRenderer keyboardListRenderer,
            IKeyboardSendButtonHandler keyboardSendButtonHandler,
            IMainStateEntity mainStateEntity,
            IPhotonChatPrcRequester photonChatPrcRequester,
            IPlayerEntity playerEntity,
            IReadOnlyList<IKeyboardKeyHandler> keyboardKeyHandlerList,
            IReadOnlyList<IKeyboardKeyRenderer> keyboardKeyRendererList,
            ITimerEntity timerEntity,
            UnlockKeyList unlockKeyList)
        {
            InfoEntity = infoEntity;
            KeyboardDeleteButtonHandler = keyboardDeleteButtonHandler;
            KeyboardEntity = keyboardEntity;
            KeyboardFormRenderer = keyboardFormRenderer;
            KeyboardListRenderer = keyboardListRenderer;
            KeyboardSendButtonHandler = keyboardSendButtonHandler;
            MainStateEntity = mainStateEntity;
            PhotonChatPrcRequester = photonChatPrcRequester;
            PlayerEntity = playerEntity;
            KeyboardKeyHandlerList = keyboardKeyHandlerList;
            KeyboardKeyRendererList = keyboardKeyRendererList;
            TimerEntity = timerEntity;
            UnlockKeyList = unlockKeyList;
        }

        public void Initialize()
        {
            // 入力
            KeyboardKeyHandlerList.Select(x => x.OnDownAsObservable())
                .Merge()
                .Subscribe(Set)
                .AddTo(Disposable);

            // 送信
            KeyboardSendButtonHandler.OnDownAsObservable()
                .Where(_ => !KeyboardEntity.IsEmpty())
                .Subscribe(_ =>
                {
                    switch (MainStateEntity.State)
                    {
                        case MainState.Wait:
                            Comment();
                            break;
                        case MainState.StagePlay:
                            SendChat();
                            break;
                    }
                })
                .AddTo(Disposable);

            //  削除          
            KeyboardDeleteButtonHandler.OnDownAsObservable()
                .Subscribe(_ => Delete())
                .AddTo(Disposable);

            // Owenrの場合、徐々に文字が増えていく
            TimerEntity.OnUpdateTimerAsObservable()
                .Where(_ => PlayerEntity.IsOwner(PhotonNetwork.LocalPlayer))
                .Where(_ => MainStateEntity.Check(MainState.StagePlay))
                .Subscribe(time =>
                {
                    var unlockKey = UnlockKeyList.List.ElementAtOrDefault(KeyboardEntity.UnlockKeyIndex);
                    if (unlockKey != null && time >= unlockKey.ThresholdTime)
                    {
                        foreach (var i in KeyboardEntity.EnableOwner(unlockKey.Count))
                        {
                            KeyboardKeyRendererList[i].Enable();
                        }

                        KeyboardEntity.IncreaseUnlockKeyIndex();
                        KeyboardListRenderer.PlayEnableSound();
                        InfoEntity.Set($"新しく {unlockKey.Count}文字 追加されました");
                    }
                })
                .AddTo(Disposable);
        }

        private void Set(string text)
        {
            KeyboardEntity.Add(text);
            KeyboardFormRenderer.Render(KeyboardEntity.Text);
        }

        private void Comment()
        {
            var properties = new Hashtable();
            properties.Add("Comment", KeyboardEntity.Text);
            PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
            Delete();
        }

        private void SendChat()
        {
            var player = PhotonNetwork.LocalPlayer;
            var index = PlayerEntity.GetIndexByPlayer(player);
            var isOwner = PlayerEntity.IsOwner(player);
            PhotonChatPrcRequester.Request(player.NickName, index, KeyboardEntity.Text, isOwner);
            Delete();
        }

        private void Delete()
        {
            KeyboardEntity.Delete();
            KeyboardFormRenderer.Render("");
        }
    }
}