using Lycoris102.Unity1Week202008.Const;
using naichilab;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

namespace Lycoris102.Unity1Week202008.View.Lobby
{
    public class TweetButton : UIBehaviour
    {
        protected override void Start()
        {
            base.Start();
            this.OnPointerDownAsObservable()
                .Subscribe(_ => Tweet())
                .AddTo(this);
        }

        private void Tweet()
        {
            var body = $"【超ハイコンテキスト・オンラインクイズゲーム】\nもじもじフラグメンツで一緒に遊ぼう！\n";
            UnityRoomTweet.Tweet(Setting.UnityRoomGameId, body, "unity1week", "もじフラ");
        }
    }
}