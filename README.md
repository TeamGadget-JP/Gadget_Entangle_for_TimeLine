# Gadget Entangle for TimeLine (GETL)

このツールは、UnityのタイムラインをマスターコントローラーとしてCascadeur&iCloneのタイムラインとリアルタイム同期するために制作しました。

##  重要な注意事項（必ずお読みください）
* **サポートなし（現状渡し）:** 開発者は普段、別の本業を抱える個人エンジニアです。そのため、個別の環境に合わせた技術サポートを提供することは事実上不可能です。本ツールは「完全無料・サポート対象外」として提供されます。

##  開発者からのお願い
本ツールは完全無料でお使いいただけます。もし皆様の制作のお役に立てましたら、ぜひ**YouTubeチャンネルの登録と高評価**をお願いいたします！
皆様からの応援が、今後の開発（現在進行中のiClone同期ツールなど）の最大のモチベーションになります！
▶️ [https://youtu.be/kNBWSCf2cIw](https://www.youtube.com/channel/UCj9OYwzMAIgYAeVkTV4wczw)

---
**Windows専用:** 本ツールは現在、Windows環境でのみ動作します。macOSやLinuxには対応しておりません。

## 🚀 導入手順* 

### 1. Cascadeur側の準備
1. このリポジトリから `gec_live_link.pyc` をダウンロードします。
2. CascadeurのPythonプラグインフォルダに配置します：`[Cascadeurインストール先]\resources\scripts\python\commands\`
3. Cascadeurを再起動します。

### 2. Unity側の準備
1. Unityで**空の新規プロジェクト**を作成します（推奨：Unity 6 / URP環境）。
2. このリポジトリから `GEC_v1.0.0.unitypackage` をダウンロードします。
3. Unityプロジェクトにインポートします（`Assets > Import Package > Custom Package...`）。

## 🎮 使い方（リアルタイム同期の実行）
1. **シーンを開く:** パッケージ内の `SampleScene` を開きます。
2. **ダッシュボード起動:** Unity上部メニューから `Gadget > Gadget Entangle Dashboard` をクリックします。
3. **Unity再生:** Unityの **Playボタン** を押してプレイモードに入ります。
4. **接続待機:** ダッシュボードの `🟢 CONNECT (Start)` ボタンを押します。
5. **Cascadeurで同期開始:** Cascadeurを開き、`Commands > Gadget Entangle for Cascadeur` スクリプトを実行して、Event logに `standing by!` が表示されればOKです！

これで準備完了です！Cascadeur側でキャラクターを動かすと、Unityのプレイモード上で物理演算やライティングが効いた状態のまま、超低遅延でモーションが同期します！

### ⚠️ プロップのメッシュがエディターで同期しない場合 (URP環境)
最新のUnity URP（Universal Render Pipeline）環境において、Cascadeurからの接続時にプロップ（小道具）のTransform数値は更新されるのに、メッシュの見た目がシーンビュー上で追従しない現象が発生する場合があります。
これはURPの強力な描画キャッシュ機能がエディターモードで干渉しているために起こります。以下の手順で設定を変更してください。

1. Projectウィンドウから、現在使用している**URPアセット**を選択します（例: `Assets/Settings/PC_RPAsset` など）。
   *(※場所が不明な場合は、上部メニューの `Edit > Project Settings > Graphics` を開き、一番上に設定されているファイルを確認してください)*
2. Inspectorウィンドウ上部の `Rendering` 項目を開きます。
3. `GPU Resident Drawer` の設定を `Instanced Drawing` から **`Disabled`** に変更します。

## 🚀 Roadmap / Upcoming Features (次期アップデート予定)
We are constantly improving the tool. The following features will be added in the next minor version update:
現在、以下の機能を次期マイナーバージョンアップに向けて開発中です：

- [ ] **Hybrid Lerp Adjustment UI (部位別Lerp調整UI)**
  - Users will be able to adjust Lerp values (0 - 60) per body part to completely prevent foot sliding and control character weight.
  - 足滑りを完全に防ぐため、ユーザーが部位ごと（足、体幹など）の補間強度（0〜60）をUIから直接チューニングできるようになります。
## ⚖️ License
This software is provided under a proprietary license. Unauthorized redistribution, modification, and reverse engineering (decompiling) are strictly prohibited. 
For full details, please read the [LICENSE](./LICENSE) file.

本ソフトウェアは独自ライセンスのもとで提供されています。無断再配布、改変、および逆コンパイル（リバースエンジニアリング）は固く禁じられています。
詳細については、[LICENSE](./LICENSE) ファイルをご確認ください。
