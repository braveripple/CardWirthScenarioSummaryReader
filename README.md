# CardWirthScenarioSummaryReader(CWSSR)
[CardWirth](https://cardwirth.net/)のシナリオディレクトリや圧縮ファイルからシナリオ概要を取得するPowerShellモジュール

***デモ***

![デモ](https://github.com/braveripple/CardWirthScenarioSummaryReader/blob/master/Assets/demo.gif?raw=true)

## ⚠️注意

CardWirthScenarioSummaryReader(以下、CWSSR)はCardWirth非公式のツールです。

## 💻 動作環境
* Windows
  * Windows PowerShell 5.1 (32bit/64bit)
  * PowerShell Core 6 (32bit/64bit)
  * PowerShell 7 (32bit/64bit)

最新のPowerShellは以下のリンクからダウンロードできます。
* https://github.com/PowerShell/PowerShell

## インストール方法

[PowerShell Gallery](https://www.powershellgallery.com/packages/CardWirthScenarioSummaryReader/)で公開する予定
```powershell
Install-Module -Name CardWirthScenarioSummaryReader -Scope CurrentUser
```

## 🌟 機能

以下の３つのコマンドレットがあります。
* **[Get-CardWirthScenario](#Get-CardWirthScenario)** コマンドレットによるシナリオ概要の取得
* **[Get-CardWirthScenarioList](#Get-CardWirthScenarioList)** コマンドレットによるシナリオ概要一覧の取得
* **[Test-CardWirthScenario](#Test-CardWirthScenario)** コマンドレットによるシナリオの判定

また、PowerShellのコマンドレットと組み合わせることで以下のことが行えます。
* シナリオ概要の閲覧(Out-GridView)、検索(Where-Object)、集計(Measure-Object)、グルーピング(Group-Object)
* シナリオのコピー、移動、圧縮、解凍
* etc

詳しくは[一歩踏み込んだ使い方](#一歩踏み込んだ使い方)を確認してください。

### 対応シナリオ形式
以下のシナリオ形式に対応しています。
* [CardWirth](https://cardwirth.net/)のシナリオエディタ(CardWirthEditor、WirthBuilder)で作成したシナリオ
  * クラシック形式(データバージョン～4)
* [CardWirthNext](http://lyna.sexy/cardwirth/)のシナリオエディタ(CardWirthNext版WirthBuilder)で作成したシナリオ
  * NEXT形式(データバージョン7)
  * > ※対象レベル下限値、上限値などの一部の情報は取得できません。シナリオ名、制作者、解説は取得できます。
* [CardWirthPy Reboot](https://bitbucket.org/k4nagatsuki/cardwirthpy-reboot/wiki/Home)のシナリオエディタ(CWXEditor)で作成したシナリオ
  * WSN形式、クラシック形式

### 対応シナリオ格納形式
以下のシナリオ格納形式に対応しています。
* ディレクトリに格納されたシナリオ
* CAB拡張子で圧縮されたシナリオ
* ZIP拡張子で圧縮されたシナリオ
  * > ※パスワード付きzipファイルには対応していません。
* WSN拡張子で圧縮されたシナリオ

### シナリオ概要取得・判定方法
ディレクトリや圧縮ファイルの中にあるSummary.wsmファイル、Summary.xmlファイルを解析してシナリオ概要を取得、およびシナリオかどうかを判定しています。

このため、Test-CardWirthScenarioコマンドレットについては厳密なシナリオの判定ではないことにご注意ください。
  * > Summary.wsmファイル、Summary.xmlファイルだけが存在するディレクトリ、圧縮ファイルもシナリオと判定されるため

## 📕 各コマンドレットの簡単な説明

### Get-CardWirthScenario
指定したパスのシナリオ概要を取得します。

#### エイリアス
`gcw`

#### 使用例
```powershell
Get-CardWirthScenario .\ゴブリンの洞窟\

ScenarioType ContainerType LevelMin LevelMax Name                     Author    Description
------------ ------------- -------- -------- ----                     ------    -----------
Classic      Directory            1        3 ゴブリンの洞窟           齋藤 洋     町外れの洞窟にゴブリンと…
```

#### 構文
```powershell
Get-CardWirthScenario ([-Path] <String[]> | -LiteralPath <String[]>) [<CommonParameters>]
```

### Get-CardWirthScenarioList

指定したパスとパス以下のシナリオ概要一覧を取得します。

パスを省略した場合は現在のディレクトリと現在のディレクトリ以下のシナリオ概要一覧を取得します。

#### エイリアス
`lscw`

#### 使用例
```powershell
Get-CardWirthScenarioList

ScenarioType ContainerType LevelMin LevelMax Name                     Author    Description
------------ ------------- -------- -------- ----                     ------    -----------
Classic      Directory            1        3 ゴブリンの洞窟           齋藤 洋     町外れの洞窟にゴブリンと…
Classic      Directory            0        0 交易都市リューン         斎藤 洋     冒険者よ、旅の準備は本当…
```

#### 構文
```powershell
Get-CardWirthScenarioList ([[-Path] <String[]>] | -LiteralPath <String[]>)
    [-CabFile] [-Classic] [-Directory] [-Next] [-Recurse] [-Wsn] [-WsnFile] [-ZipFile]
    [<CommonParameters>]
```

### Test-CardWirthScenario

指定したパスがCardWirthのシナリオかどうかを判定します。

シナリオであり、かつ、指定条件にあてはまる場合は`True`が返ります。それ以外の場合は`False`が返ります。

このコマンドレットはスクリプトで使うことを想定しているため、エイリアスはありません。

#### 使用例
```powershell
Test-CardWirthScenario .\ゴブリンの洞窟\
True
```

#### 構文
```powershell
Test-CardWirthScenario ([-Path] <String[]> | -LiteralPath <String[]>)
    [-ContainerType {Any | Directory | CabFile | ZipFile | WsnFile}] 
    [-ScenarioType {Any | Classic | Next | Wsn}]
    [<CommonParameters>]
```

## 📃 出力形式の説明

* TypeName: BraveRipple.CardWirthScenarioSummaryReaderTool.Entities.ScenarioSummary

`*`がついているプロパティはPowerShell用に追加されたプロパティです。

|プロパティ名|説明|
|:---|:---|
|ScenarioType|シナリオ形式<br>Classic, Next, Wsnのいずれか|
|ContainerType|シナリオ格納形式<br>Directory, CabFile, ZipFile, WsnFileのいずれか|
|LevelMin|対象レベル下限値|
|LevelMax|対象レベル上限値|
|Name|シナリオ名|
|Author|制作者|
|Description|解説|
|SummaryInfo|Summaryファイルの情報|
|LastWriteTime|シナリオ格納場所(ディレクトリ、圧縮ファイル)の更新日時|
|FullName|シナリオ格納場所の絶対パス|
|Level *|対象レベル|
|PSPath *|シナリオ格納場所の絶対パス(FullNameと同じ)|

## 🍀一歩踏み込んだ使い方

CWSSRのコマンドレットとPowerShellのコマンドレットを組み合わせることで色々できます。

以下はその一例です。

* 現在のディレクトリからゴブ洞改変シナリオらしきものを探すワンライナー(1行プログラム)
> `?`は`Where-Object`のエイリアスです。
```powershell
lscw | ? { $_.Name -like "*洞*" -or $_.Description -like "*ゴブ洞改変*" }
```

* 現在のディレクトリから6人用シナリオを探すワンライナー
```powershell
lscw | ? { $_.Description -match "[6６六]人(専)?用" }
```

* 現在のディレクトリにあるディレクトリに格納されたシナリオをZIP圧縮するワンライナー
> `%`は`ForEach-Object`のエイリアスです。
```powershell
lscw -Directory | % { Compress-Archive -LiteralPath $_.FullName -DestinationPath ($_.FullName + ".zip") }
```

* 現在のディレクトリにあるシナリオを対象レベル別のディレクトリに分類するワンライナー
```powershell
lscw | Group-Object Level | % { $dir = mkdir $_.Name -Force; $_.Group | Move-Item -Destination $dir }
```

* 現在のディレクトリ以下にあるシナリオを現在のディレクトリに持ってくるワンライナー
```powershell
lscw -Recurse | Move-Item
```
> * もし、同じ名前のディレクトリや圧縮ファイルが異なるディレクトリに別々に存在する場合でも、Move-Itemに-Forceオプションをつけない限りは上書きされることはありません。

## ❓ 想定される質問

### 日本語が文字化けする
  * 日本語対応フォントを使用してください。
    * MS ゴシック
    * [RictyDiminished](https://github.com/edihbrandon/RictyDiminished)

### シナリオ概要が取得できない
  * パスに`[]`の文字が含まれている場合、-LiteralPathパラメーターを使わないと`[]`の文字がワイルドカードとして認識され、意図したシナリオが取得できなくなります。
    * -LiteralPathパラメーターを使用してください。

  * Get-CardWirthScenarioコマンドレットを使用してエラーメッセージを確認してください。主なシナリオ概要が取得できない原因は以下の通りです。
    * 未対応のシナリオ格納形式だった。(ディレクトリ、.cab、.zip、.wsnファイル以外をパスに指定した）
    * ディレクトリ、圧縮ファイルの中にSummary.wsm、Summary.xmlファイルがどちらも存在しなかった。
    * 圧縮ファイルがパスワード付きZIPで解析できなかった。
    * Summary.wsm、Summary.xmlの読み込みに失敗した。

### ワンライナーが想定通りに動くかどうか確認したい
  * Move-ItemやCopy-Itemなど変更を伴うコマンドレットは`-WhatIf`パラメータをつけることでどのような変更が起こるか確認できます。（実際の操作は行われません）
    * 例えば現在のディレクトリにあるシナリオを対象レベル別のディレクトリに分類するワンライナーに-WhatIfをつけて実行すると以下のようなメッセージが出力されます。（実際の移動は行われません）
```powershell
lscw | Group-Object Level | % { $dir = mkdir $_.Name -Force; $_.Group | Move-Item -Destination $dir -WhatIf }
```
```txt
What if: Performing the operation "Move Directory" on target "Item: C:\Game\CardWirth\Scenario\Ask\ゴブリンの洞窟 Destination: C:\Game\CardWirth\Scenario\Ask\対象レベル：01～03\ゴブリンの洞窟".
What if: Performing the operation "Move Directory" on target "Item: C:\Game\CardWirth\Scenario\Ask\交易都市リューン Destination: C:\Game\CardWirth\Scenario\Ask\対象レベル：なし\交易都市リューン".
```

## 作成者

[@braveripple](https://github.com/braveripple)

## ライセンス

[MITライセンス](./LICENSE)です。

## 使用ライブラリについて

以下のライブラリを使用しました。

* [braveripple/CardWirthScenarioSummaryReaderTool](https://github.com/braveripple/CardWirthScenarioSummaryReaderTool)
* [icsharpcode/SharpZipLib](https://github.com/icsharpcode/SharpZipLib)
* [MSFTCompressionCab](https://www.nuget.org/packages/MSFTCompressionCab)
