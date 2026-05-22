# Gadget Entangle for TimeLine (GETL)

GETLは今までのツールとは干渉することなく単体で使用することができます、GETL二は主に2つの機能があります。
1. Unityのタイムラインをマスターコントローラー(片方向)として Cascadeur & iClone のタイムラインとリアルタイム同期する機能 (別々のポートを使用するため同時使用が可能)
2. リアルタイムに送られてくるキャラクターボーン情報をボタン一発でレコーディングするアニメーションベイク機能

##  開発の目的そして今後の方針(OSS化)
GEC、GEP、GTransporter、そして今回のGETLはUnityをベースとした映像制作環境(VP)を個人ベースで実現できないか？というのが最大の開発目的です。
そしてそれを実現することを目標にツールの制作をしています、ですから、その他の用途でご使用される方は意図する動作をしてくれないケースもあるかと思います、
運用方法の工夫などで解決できれば良いのですが、スクリプトを直接いじらないと駄目なこともあるでしょう、そこで今回の公開からはOSSとした次第です。
また、いままでリリーズ済みのツールも開発元ライセンスを遵守しなければならない物以外は今後、順次OSS公開に切り替えていきます。

##  重要な注意事項（必ずお読みください）
* **サポートなし（現状渡し）:** 開発者は普段、別の本業を抱えるFA系個人エンジニアです。そのため、個別の環境に合わせた技術サポートを提供することは事実上不可能です。本ツールは「完全無料・サポート対象外」として提供されます。
* しかしながら、バグフィックスやバージョンアップは不定期ながらも行って行くつもりです、皆様からのフィードバックは大歓迎です。些細なことでもどんどん書き込んで下さると助かります！

##  開発者からのお願い
もし皆様の制作のお役に立てましたら、ぜひ**YouTubeチャンネルの登録と高評価**をお願いいたします
皆様からの応援が、今後の開発の最大のモチベーションになります
▶️ [https://youtu.be/kNBWSCf2cIw](https://www.youtube.com/channel/UCj9OYwzMAIgYAeVkTV4wczw)

---
**Windows専用:** 本ツールは現在、Windows環境でのみ動作します(Windows APIを使用しているため)。macOSやLinuxでは動作しません。

## 導入手順と使用方法

### 1. Cascadeur側の準備
1. このリポジトリから `GEC_TimeReceiver.py` をダウンロードします。
2. CascadeurのPythonプラグインフォルダに配置します：`[Cascadeurインストール先]\resources\scripts\python\commands\`
3. Cascadeurを起動します。
4. `メニューバー > Commands > GETL TimeLine Receiver`をクリック。
5. Event logに`▶️[GETL TimeLine Receiver]Started syncing with Unity!(Port:8991)`を表示されば同期準備OKです。
6. もう一度`メニューバー > Commands > GETL TimeLine Receiver`をクリックして同期停止です。

### 2. iClone側の準備
1. このリポジトリから `GEi_TimeReceiver.py` をダウンロードします。
2. iCloneを起動します。
3. `メニューバー > Script > Load Python`をクリック。
4. 1.でダウンロードした`GEi_TimeReceiver.py`を読込。
5. ダイアログが開きますので`▶ START SYNC(Port:8992)`ボタンで同期開始。

### 3. Unity側の準備と使用方法
1. このリポジトリから `GETL_v1.0.0.unitypackage` をダウンロードします。
2. Unityプロジェクトにインポートします（`Assets > Import Package > Custom Package...`）。
3. Project > Assets > Gadget > Editor に`GETL_Recorder.cs`が入ります。
4. Project > Assets > Gadget > Scripts に`GETL_Broadcaster.cs`が入ります。
5. Hierarchy に空のゲームオブジェクトを制作します、名称は任意です、それに`GETL_Broadcaster.cs`をアタッチしてください。
6. 5.の空のゲームオブジェクト(名前をつけていればそれに)を選択して`Window > Sequencing > Timeline`
7. Timelineウィンドウの`Create > 任意の名前で制作`これでシークバーを動かせば同期します。

### 4. アニメーションベイクの仕方
1. ツールバー`Gadget > GETL Recorder`でGETL Recorder UIが開きます。
2. Target Avatar にアニメーションベイクしたいキャラクターをドラッグアンドドロップ。
3. Playable Director に GETL_Broadcaster.csをアタッチしたゲームオブジェクトをドラッグアンドドロップ。
4. Overwrite Clip に `.anim`をドラッグアンドドロップ。
5. Target Frame Rate は Cascadeur や iClone で30だったり60だったりしますので、合わせて設定してください。
6. GETL_Broadcaster.csをアタッチしたゲームオブジェクトにもInspector でも Target Frame Rate 項目がありますので忘れずにそちらも合わせてください。
7. `Bake Delay(sec)`はそのままでいいと思います。
8. iClone を同期してベイクする場合は`Bake Facial(Blendshape)`チェックを入れて下さい。
9. 後は`●START ANIMATION BAKE`ボタンを押すだけでベイクします。

本ソフトウェアは独自ライセンスのもとで提供されています。改変は可としますが、販売等の目的での無断再配布は固く禁じられています。
詳細については、[LICENSE](./LICENSE) ファイルをご確認ください。
