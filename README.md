# Gadget Entangle for TimeLine (GETL)

GETL is designed to be used independently without interfering with existing tools. It primarily offers two key features:

1. **Master Timeline Synchronization:** Uses Unity's Timeline as a master controller (one-way) to synchronize in real-time with the timelines of Cascadeur & iClone. (Simultaneous use is possible as they use separate ports).
2. **Animation Baking:** A one-click feature that records and bakes the character bone data sent in real-time.

---
**Windows Only:** This tool currently operates only in a Windows environment (as it utilizes the Windows API). It does not work on macOS or Linux.

## Installation and Usage Guide (Updated: 2026-08-05)<br>

### 1. Cascadeur Setup<br>
1. Download `GEC_TimeReceiver.py` from this repository.<br>
2. Copy and place it into Cascadeur's Python plugin folder `commands\`.<br>
   For a standard installation, this will be: `C:\Program Files\Cascadeur\resources\scripts\python\commands\`<br>
   Please adjust the destination according to your specific installation environment.<br>
3. Launch Cascadeur.<br>
4. Click `Menu Bar > Commands > GETL TimeLine Receiver`.<br>
5. If `▶️[GETL TimeLine Receiver]Started syncing with Unity!(Port:8991)` appears in the Event log, the sync preparation is complete.<br>
   <img width="1264" height="324" alt="image" src="https://github.com/user-attachments/assets/e8a23f6e-9a35-4aaf-b241-dce2a72bd42d" /><br>
6. Click `Menu Bar > Commands > GETL TimeLine Receiver` again to stop it.<br>
   Currently, Cascadeur does not support custom UIs, so you must toggle it on and off in this manner.<br>

### 2. iClone Setup<br>
1. Download `GEi_TimeReceiver.py` from this repository.<br>
2. Launch iClone.<br>
3. Click `Menu Bar > Script > Load Python`.<br>
4. Load the `GEi_TimeReceiver.py` downloaded in step 1.<br>
5. A dialog will open. Click the `▶ START SYNC(Port:8992)` button to start syncing.<br>

### 3. Unity Setup and Usage<br>
1. Download `GETL_v1.0.0.unitypackage` from this repository.<br>
2. Import it into your Unity project (`Assets > Import Package > Custom Package...`).<br>
3. `GETL_Recorder.cs` will be placed in `Project > Assets > Gadget > Editor`.<br>
   If you are setting it up manually, create an `Editor` folder anywhere and place `GETL_Recorder.cs` inside it.<br>
   This step is absolutely necessary. Once done, `Gadget` will appear in the Unity menu bar.<br>
   <img width="475" height="183" alt="image" src="https://github.com/user-attachments/assets/47f99d16-1c8d-4e25-9dfd-dd5606328242" /><br>
   Click `GETL Recorder`, and<br>
   <img width="501" height="501" alt="image" src="https://github.com/user-attachments/assets/09f818ed-1502-4907-84f9-4050734cc3d3" /><br>
   the GETL Recorder UI will open.<br>
4. `GETL_Broadcaster.cs` will be placed in `Project > Assets > Gadget > Scripts`.<br>
   If you are setting it up manually, create a `Scripts` folder anywhere and place `GETL_Broadcaster.cs` inside it.<br>
5. Create an empty GameObject in the Hierarchy. You can name it anything. Attach `GETL_Broadcaster.cs` to it.<br>
   You can attach it by dragging and dropping `GETL_Broadcaster.cs` onto the empty GameObject you just created.<br>
   <img width="839" height="732" alt="image" src="https://github.com/user-attachments/assets/6e45bb6a-ae3b-4269-bab6-41ddb2eb9ef8" /><br>
6. Select the empty GameObject from step 5 (or whatever you named it), and click `Window > Sequencing > Timeline`.<br>
7. In the Timeline window that appears, click `Create >` and save it with any name.<br>
8. At this point, if you move the seek bar in Unity, the timelines in Cascadeur and iClone should also move in sync.<br>
   Reference image:<br>
   <img width="1915" height="1930" alt="image" src="https://github.com/user-attachments/assets/c40dfb43-dd9e-4a69-9fbc-6a2f72e70a76" /><br>

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

## ⚠️ Important Notice: Limitations Regarding Unity's Timeline and Humanoid Avatar ##
Unlike conventional FBX workflows, GEC and GEi acquire bone data directly via scripts and apply it to characters in Unity.
During real-time synchronization, the script achieves this by completely ignoring the `Humanoid Avatar` control applied to the character and forcefully overwriting the bone transforms. 
However, when playing back baked animations using **Unity's native Timeline**, the internal influence of the `Humanoid Avatar` cannot be bypassed.
As a result, if you play the timeline while the `Humanoid Avatar` is still applied, the character's mesh will be horribly crushed or heavily distorted.
The only workaround to prevent this is to **remove the `Avatar` from the character's Animator component (set it to None)**.
While this workaround is perfectly fine for Video Production (VP) purposes, it causes **critical issues for Game Development**, where animation retargeting and state machines are essential.
Therefore, if your ultimate goal is game development, we highly recommend avoiding this tool for animation baking. Please consider this feature specifically designed for video production workflows.

---
This software is provided under a custom license. Modification is permitted, but unauthorized redistribution for sales or similar purposes is strictly prohibited. For details, please check the `LICENSE` file.
# Gadget Entangle for TimeLine (GETL)

GETLは今までのツールとは干渉することなく単体で使用することができます、GETLには主に2つの機能があります。
1. Unityのタイムラインをマスターコントローラー(片方向)として Cascadeur & iClone のタイムラインとリアルタイム同期する機能 (別々のポートを使用するため同時使用が可能)
2. リアルタイムに送られてくるキャラクターボーン情報をボタン一発でレコーディングするアニメーションベイク機能

---
**Windows専用:** 本ツールは現在、Windows環境でのみ動作します(Windows APIを使用しているため)。macOSやLinuxでは動作しません。<br>

## 導入手順と使用方法　2026-08-05更新<br>

### 1. Cascadeur側の準備<br>
1. このリポジトリから `GEC_TimeReceiver.py` をダウンロードします。<br>
2. CascadeurのPythonプラグインフォルダ`commands\`にコピー配置します。<br>
   通常インストールなら`C:\Program Files\Cascadeur\resources\scripts\python\commands\`<br>
   貴方のインストール環境に合わせてコピー配置します。<br>
4. Cascadeurを起動してください。<br>
5. `メニューバー > Commands > GETL TimeLine Receiver`をクリック。<br>
6. Event logに`▶️[GETL TimeLine Receiver]Started syncing with Unity!(Port:8991)`を表示されば同期準備OKです。<br>
   <img width="1264" height="324" alt="image" src="https://github.com/user-attachments/assets/e8a23f6e-9a35-4aaf-b241-dce2a72bd42d" /><br>
8. もう一度`メニューバー > Commands > GETL TimeLine Receiver`をクリックすれば停止します。<br>
   現在CascadeurはUIが作れないのでこの様なオン・オフ操作をしなければなりません。<br>

### 2. iClone側の準備<br>
1. このリポジトリから `GEi_TimeReceiver.py` をダウンロードします。<br>
2. iCloneを起動します。<br>
3. `メニューバー > Script > Load Python`をクリック。<br>
4. 1.でダウンロードした`GEi_TimeReceiver.py`を読込。<br>
5. ダイアログが開きますので`▶ START SYNC(Port:8992)`ボタンで同期開始。<br>

### 3. Unity側の準備と使用方法<br>
1. このリポジトリから `GETL_v1.0.0.unitypackage` をダウンロードします。<br>
2. Unityプロジェクトにインポートします（`Assets > Import Package > Custom Package...`）<br>
3. Project > Assets > Gadget > Editor に`GETL_Recorder.cs`が入ります。<br>
   手動でセットアップする場合は任意の場所に`Editor`というフォルダーを作りその中に`GETL_Recorder.cs`を配置します。<br>
   これは絶対に必要な作業です。そうしますとUnityのメニューバーに`Gadget`が現れます。<br>
   <img width="475" height="183" alt="image" src="https://github.com/user-attachments/assets/47f99d16-1c8d-4e25-9dfd-dd5606328242" /><br>
   `GETL Recorder`をクリックすると<br>
   <img width="501" height="501" alt="image" src="https://github.com/user-attachments/assets/09f818ed-1502-4907-84f9-4050734cc3d3" /><br>
   GETL RecorderのUIが開きます。<br>
5. Project > Assets > Gadget > Scripts に`GETL_Broadcaster.cs`が入ります。<br>
   手動でセットアップする場合は任意の場所に`Scripts`というフォルダーを作りその中に`GETL_Broadcaster.cs`を配置します。<br>
7. Hierarchy に空のゲームオブジェクトを制作します。名称は何でも良いです。そこに`GETL_Broadcaster.cs`をアタッチしてください。<br>
   作った空のゲームオブジェクトに`GETL_Broadcaster.cs`をドラッグ・アンド・ドロップでアタッチできます。<br>
   <img width="839" height="732" alt="image" src="https://github.com/user-attachments/assets/6e45bb6a-ae3b-4269-bab6-41ddb2eb9ef8" /><br>
9. 5.の空のゲームオブジェクト(名前をつけていればそれに)を選択して`Window > Sequencing > Timeline`をクリック<br>
10. 出てきたTimelineウィンドウの`Create > 任意の名前で作成`<br>
11. この時点でUnityのシークを動かせばCascadeurやiCloneのタイムラインも動くはずです。<br>
    参考画像<br>
    <img width="1915" height="1930" alt="image" src="https://github.com/user-attachments/assets/c40dfb43-dd9e-4a69-9fbc-6a2f72e70a76" /><br>

### 4. アニメーションベイクの仕方<br>
1. ツールバー`Gadget > GETL Recorder`でGETL Recorder UIが開きます。<br>
2. Target Avatar にアニメーションベイクしたいキャラクターをドラッグアンドドロップ。<br>
3. Playable Director に GETL_Broadcaster.csをアタッチしたゲームオブジェクトをドラッグアンドドロップ。<br>
4. Overwrite Clip に `.anim`をドラッグアンドドロップ。<br>
5. Target Frame Rate は Cascadeur や iClone で30だったり60だったりしますので、合わせて設定してください。
6. GETL_Broadcaster.csをアタッチしたゲームオブジェクトのInspector にも Target Frame Rate 項目がありますので忘れずにそちらも合わせてください。
7. `Bake Delay(sec)`はそのままでいいと思います。
8. iClone を同期してベイクする場合は`Bake Facial(Blendshape)`チェックを入れて下さい。
9. 後は`●START ANIMATION BAKE`ボタンを押すだけでベイクします。

## ⚠️ 重要事項：Unityのタイムライン仕様とHumanoid Avatarに関する制限 ##
GECやGEiは、従来のFBXファイルを経由するワークフローとは異なり、スクリプトを通じて直接ボーン情報を取得し、Unity上のキャラクターに適用しています。
リアルタイム同期の実行中は、スクリプトがキャラクターの `Humanoid Avatar` の制御を無視し、強制的にボーンのトランスフォームを上書きすることで同期を実現しています。
しかし、ベイクしたアニメーションを**Unity公式のタイムラインで再生する際**には、この `Humanoid Avatar` の影響を回避することができません。
そのため、`Humanoid Avatar` が適用されたままの状態でタイムラインを再生すると、キャラクターのメッシュが無惨に潰れたり、姿勢が破綻してしまいます。
これを回避する唯一の方法は、**キャラクターのAnimatorから `Avatar` を外す（Noneにする）こと**です。
映像制作（VP）目的であればこの運用で全く問題ありませんが、アニメーションの流用やステートマシンの利用が必須となる**ゲーム制作においては致命的な問題**となります。
したがって、最終的な目的がゲーム開発である場合、本ツールを使用したアニメーションのベイクは推奨しません。あくまで「映像制作向けの機能」としてご活用ください。

---
本ソフトウェアは独自ライセンスのもとで提供されています。改変は可としますが、販売等の目的での無断再配布は固く禁じられています。
詳細については、[LICENSE](./LICENSE) ファイルをご確認ください。
