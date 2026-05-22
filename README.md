# Gadget Entangle for TimeLine (GETL)

GETLは今までのツールとは干渉することなく単体で使用することができます、GETL二は主に2つの機能があります。
1. Unityのタイムラインをマスターコントローラー(片方向)として Cascadeur & iClone のタイムラインとリアルタイム同期する機能 (別々のポートを使用するため同時使用が可能)
2. リアルタイムに送られてくるキャラクターボーン情報をボタン一発で記録するアニメーションベイク機能

##  開発の目的そして今後の方針(OSS化)
GEC、GEP、GTransporter、そして今回のGETLはUnityをベースとした映像制作環境(VP)をなんとかして個人ベースで実現できないか？というのが最大の開発動機です。
そしてそれを実現することを目標にツールの制作をしています、ですから、その他の用途でご使用される方は意図する動作をしてくれないケースもあるかと思います、
運用方法の工夫などで解決できれば良いのですが、スクリプトを直接いじらないと駄目なこともあるでしょう、そこで今回の公開からOSSとしました。
また、いままでリリーズ済みのツールも開発元ライセンスを遵守しなければならない物以外は今後順次OSS公開していきます。
これからもTeamGadgetのツールをご活用ください。

##  重要な注意事項（必ずお読みください）
* **サポートなし（現状渡し）:** 開発者は普段、別の本業を抱えるFA系個人エンジニアです。そのため、個別の環境に合わせた技術サポートを提供することは事実上不可能です。本ツールは「完全無料・サポート対象外」として提供されます。
* しかしながら、バグフィックスやバージョンアップは不定期ながらも行って行くつもりです、皆様からのフィードバックは大歓迎です。些細なことでもどんどん書き込んじゃってください！

##  開発者からのお願い
もし皆様の制作のお役に立てましたら、ぜひ**YouTubeチャンネルの登録と高評価**をお願いいたします
皆様からの応援が、今後の開発の最大のモチベーションになります
▶️ [https://youtu.be/kNBWSCf2cIw](https://www.youtube.com/channel/UCj9OYwzMAIgYAeVkTV4wczw)

---
**Windows専用:** 本ツールは現在、Windows環境でのみ動作します。macOSやLinuxには対応しておりません。

## 導入手順と使用方法

### 1. Cascadeur側の準備
1. このリポジトリから `GEC_TimeReceiver.py` をダウンロードします。
2. CascadeurのPythonプラグインフォルダに配置します：`[Cascadeurインストール先]\resources\scripts\python\commands\`
3. Cascadeurを起動します。
4. `メニューバー > Commands > GETL TimeLine Receiver`をクリック。
5. Event logに`▶️[GETL TimeLine Receiver]Started syncing with Unity!(Port:8991)`を表示されば同期準備OKです。
6. もう一度`メニューバー > Commands > GETL TimeLine Receiver`をクリックして同期停止です。
   (Cascadeurで常時表示できるUIの制作方法がわかりません、知っている方いましたら教えてください)

### 2. iClone側の準備
1. このリポジトリから `GEi_TimeReceiver.py` をダウンロードします。
2. iCloneを起動します。
3. `メニューバー > Script > Load Python`をクリック。
4. 1.でダウンロードした`GEi_TimeReceiver.py`を読込。
5. ダイアログが開きますので`▶ START SYNC(Port:8992)`ボタンで同期開始。

### 3. Unity側の準備
1. このリポジトリから `GETL_v1.0.0.unitypackage` をダウンロードします。
2. Unityプロジェクトにインポートします（`Assets > Import Package > Custom Package...`）。
3. 


本ソフトウェアは独自ライセンスのもとで提供されています。無断再配布、改変、および逆コンパイル（リバースエンジニアリング）は固く禁じられています。
詳細については、[LICENSE](./LICENSE) ファイルをご確認ください。
