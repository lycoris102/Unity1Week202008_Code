# 概要
Unity1週間ゲームジャムで制作した[もじもじフラグメンツ](https://unityroom.com/games/word_fragments) というゲームのソースコードです

# 前置き
* 名前空間はCleanArchitecture (のサンプルレイヤー) っぽいですが、MVPに近そうです。この辺りあんまり自信ないです
* 何かあれば色々ご意見いただけたらと思います (リプでもコメントでも)

# 主要レイヤーの責務
* [View](https://github.com/lycoris102/Unity1Week202008_Code/tree/master/Scripts/View) 
    * Unityからのイベントを受け取って通知したり、Unity側の描画情報を更新する
    * ViewはUseCase/Entityのことを知らず、複雑な計算やゲーム内処理は行わない (UnityAPIを叩くだけ)
    * MonoBahaviourもしくはUIBehaviourを継承する
* [UseCase](https://github.com/lycoris102/Unity1Week202008_Code/tree/master/Scripts/UseCase)
    * Viewから通知を受け取りEntityを更新したり、Entityから通知を受け取りViewに描画命令を投げたりする中間層
    * ビジネスロジック (更新や判定) をUseCaseに寄せている
    * UseCaseは他のUseCaseを知らず、Entityを介して処理を伝搬する
* [Entity](https://github.com/lycoris102/Unity1Week202008_Code/tree/master/Scripts/Entity)
    * 情報を保持/精査/更新する
    * 状態の更新を通知する
    * EntityはView/UseCaseのことを知らず、他のEntityのことも知らない

## 例: キーボードの入力 / Viewから入力を受け取りEntityを更新する
このゲームではキーボードのUIボタンを選択すると、フォームに文字が入力されます。

* Viewでは [どういう処理でイベントを通知するか](https://github.com/lycoris102/Unity1Week202008_Code/blob/master/Scripts/View/Main/KeyboardKey.cs#L58-L59) のみを定義しています 
* UseCaseにて、[通知を受けた時にどういう処理をするか](https://github.com/lycoris102/Unity1Week202008_Code/blob/master/Scripts/UseCase/Main/KeyboardUseCase.cs) を記載しています
* 今回の場合は [Entityの中に情報を格納し、フォームに描画する](https://github.com/lycoris102/Unity1Week202008_Code/blob/master/Scripts/UseCase/Main/KeyboardUseCase.cs#L115-L119) という処理が実行されています
* 実際のEntityの中では [Validateしつつ文字を追加](https://github.com/lycoris102/Unity1Week202008_Code/blob/master/Scripts/Entity/KeyboardEntity.cs#L22-L26) しています
* メッセージを送信するタイミングで [更新されたEntity](https://github.com/lycoris102/Unity1Week202008_Code/blob/master/Scripts/UseCase/Main/KeyboardUseCase.cs#L134) を参照するようにしています。

## 例: インフォメーションテキストの更新 / Entityを介したUseCase間の伝搬
* 画面下部のインフォメーションを更新するタイミングで [InfoEntity にテキストを渡します](https://github.com/lycoris102/Unity1Week202008_Code/blob/master/Scripts/UseCase/Main/ResultUseCase.cs#L78)
* InfoEntity が [テキストを更新し、更新されたことを通知](https://github.com/lycoris102/Unity1Week202008_Code/blob/master/Scripts/Entity/InfoEntity.cs#L12-L19) します
* InfoUseCase が [通知を受け取り、Viewに描画命令を投げます](https://github.com/lycoris102/Unity1Week202008_Code/blob/master/Scripts/UseCase/Main/InfoUseCase.cs#L24-L33)

## 責務を分解することの所感
* ✨ メリット
    * どこにコードを書けばいいか迷わない
        * ゲーム内処理多めなクイズやカードゲームなどでは良い感じに整理出来そう
    * イベントの流れがシンプルで追いやすい
* 😭 デメリット
    * コード量が増えて正直ゲームジャム向きではない、慣れないと本質(ゲームの面白さや完成)でないところで疲弊する
        * 特にMonoBehaviourに処理を寄せがちなアクションやシューティングゲームにはそこまで利点がないかも
        * 一方ゲームジャムの目的を何にするか、ということにも依存し、例えば「アーキテクチャを学習する」を主目的にするのであれば、あり

# アセット
* マルチプレイの実装に [PUN2](https://assetstore.unity.com/packages/tools/network/pun-2-free-119922?locale=ja-JP) を使用しています
    * 初めて実装しましたが、意外とすんなり対応できました
    * [@o8que](https://twitter.com/o8que) さんの [資料が神掛かっていた](https://connect.unity.com/p/pun2deshi-meruonraingemukai-fa-ru-men-sono1) おかげです、Photon使ってみたい人は全員読みましょう
* イベントを監視するために [UniRx](https://github.com/neuecc/UniRx) を使用しています。
* PureC#クラスの生成・レイヤー間の依存関係の解消 (UseCaseがViewやEntityを認知している状態にする) に [VContainer](https://github.com/hadashiA/VContainer) を使用しています
    * [@hadashiA](https://twitter.com/hadashiA) さんが開発された Zenjectのように扱えるUntiy向けDIツールです
    * パフォーマンスチューニングされており、Zenjectより高速化されています
    * Zenjectで提供されているSignalやFactoryは現時点ではなく、非常にシンプルなツールです
    * Zenjectとの [API対応表](https://github.com/hadashiA/VContainer#comparing-vcontainer-to-zenject) が実装する上で非常に役に経ちました
    * (Zenjectの方がメジャーなので、初めて触られる方はZenjctから入ると資料が豊富で良いかも)
