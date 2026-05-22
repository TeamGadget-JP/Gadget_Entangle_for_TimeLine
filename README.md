# Gadget Entangle for TimeLine (GETL)

GETL is designed to be used independently without interfering with existing tools. It primarily offers two key features:

1. **Master Timeline Synchronization:** Uses Unity's Timeline as a master controller (one-way) to synchronize in real-time with the timelines of Cascadeur & iClone. (Simultaneous use is possible as they use separate ports).
2. **Animation Baking:** A one-click feature that records and bakes the character bone data sent in real-time.

## Development Purpose & Future Policy (Open Source)
The ultimate goal behind the development of GEC, GEP, GTransporter, and now GETL, is to realize a Virtual Production (VP) environment based on Unity at a personal scale. We are creating these tools specifically to achieve this goal.

Therefore, if you intend to use this tool for other purposes, it may not behave as expected. While workarounds in your workflow might solve some issues, there will be cases where direct script modification is necessary. 

Because of this, we have decided to release this tool as Open Source Software (OSS). Furthermore, for the tools we have already released (excluding those that must comply with the original developer's license), we plan to gradually transition them to OSS in the future.

## Important Notices (Please Read Carefully)
* **No Support (As-is Delivery):** The developer is an individual FA (Factory Automation) engineer with a separate full-time job. Therefore, it is practically impossible to provide technical support tailored to individual environments. This tool is provided "completely free of charge and unsupported."
* However, we plan to perform bug fixes and version updates irregularly. We highly welcome your feedback! Even the smallest comments are greatly appreciated.

## A Request from the Developer
If this tool helps you in your production, we would be incredibly grateful if you could subscribe to our YouTube channel and hit the like button. Your support is the greatest motivation for our future development! 
▶️ [https://youtu.be/kNBWSCf2cIw](https://www.youtube.com/channel/UCj9OYwzMAIgYAeVkTV4wczw)

**Windows Only:** This tool currently operates only in a Windows environment (as it utilizes the Windows API). It does not work on macOS or Linux.

---

## Installation & Usage

### 1. Cascadeur Preparation
1. Download `GEC_TimeReceiver.py` from this repository.
2. Place it in the Cascadeur Python plugins folder: `[Cascadeur Install Directory]\resources\scripts\python\commands\`
3. Launch Cascadeur.
4. From the menu bar, click `Commands` > `GETL TimeLine Receiver`.
5. If the Event log displays `▶ [GETL TimeLine Receiver] Started syncing with Unity! (Port: 8991)`, it is ready for synchronization.
6. Click `Commands` > `GETL TimeLine Receiver` again from the menu bar to stop synchronization.

### 2. iClone Preparation
1. Download `GEi_TimeReceiver.py` from this repository.
2. Launch iClone.
3. From the menu bar, click `Script` > `Load Python`.
4. Load the `GEi_TimeReceiver.py` you downloaded in step 1.
5. A dialog will open; click the `▶ START SYNC (Port: 8992)` button to start synchronization.

### 3. Unity Preparation & Usage
1. Download `GETL_v1.0.0.unitypackage` from this repository.
2. Import it into your Unity project (`Assets` > `Import Package` > `Custom Package...`).
3. `GETL_Recorder.cs` will be placed in `Project` > `Assets` > `Gadget` > `Editor`.
4. `GETL_Broadcaster.cs` will be placed in `Project` > `Assets` > `Gadget` > `Scripts`.
5. Create an empty GameObject in the `Hierarchy`. You can name it whatever you like, and attach `GETL_Broadcaster.cs` to it.
6. Select the empty GameObject you created in step 5, then go to `Window` > `Sequencing` > `Timeline`.
7. Click `Create` in the Timeline window and give it any name. Moving the seek bar will now synchronize the tools.

### 4. How to Bake Animations
1. Open the GETL Recorder UI from the toolbar: `Gadget` > `GETL Recorder`.
2. Drag and drop the character you want to bake into the `Target Avatar` field.
3. Drag and drop the GameObject with `GETL_Broadcaster.cs` attached into the `Playable Director` field.
4. Drag and drop your `.anim` file into the `Overwrite Clip` field.
5. Set the `Target Frame Rate` to match your settings in Cascadeur or iClone (e.g., 30 or 60).
6. **Note:** Ensure you also set the `Target Frame Rate` in the Inspector of the GameObject where `GETL_Broadcaster.cs` is attached.
7. You can generally leave `Bake Delay(sec)` at its default setting.
8. If you are syncing and baking from iClone, please check the `Bake Facial(Blendshape)` box.
9. Finally, just press the `● START ANIMATION BAKE` button to begin baking.

---
This software is provided under a custom license. Modification is permitted, but unauthorized redistribution for sales or similar purposes is strictly prohibited. For details, please check the `LICENSE` file.
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
