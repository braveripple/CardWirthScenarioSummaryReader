# CardWirthScenarioSummaryReader(CWSSR)
CardWirthのシナリオディレクトリや圧縮ファイルからシナリオ概要を取得するPowerShellモジュール


ここは本文です[^anchor]


***デモ***

![デモ](https://github.com/braveripple/CardWirthScenarioSummaryReader/blob/master/Assets/demo.gif?raw=true)

## 動作環境
* Windows
  * Windows PowerShell 5.1 (32bit/64bit)
  * PowerShell Core (32bit/64bit)

### 使用ライブラリ

* ZIPファイルの解析にSharpZipLibを使用しています。
* CABファイルの解析にMSFTCompressionCabを使用しています。

それぞれのライセンスについてはLICENSE.txtを参照してください。

## 機能

以下の３つのコマンドレットがあります。
* Get-CardWirthScenarioコマンドレットによるシナリオ概要の取得
* Get-CardWirthScenarioListコマンドレットによるシナリオ概要一覧の取得
* Test-CardWirthScenarioコマンドレットによるシナリオの判定

また、PowerShellのコマンドレットと組み合わせることで以下のことが行えます。
* シナリオ概要の閲覧、検索、集計、グルーピング
* シナリオのコピー、移動、圧縮、解凍
* etc

## 対応シナリオ形式
以下のシナリオ形式に対応しています。
* CardWirthのシナリオエディタで作成したシナリオ（クラシック形式）
* CardWirthNextのシナリオエディタで作成したシナリオ（NEXT形式）
* CardWirthPy Reboot のシナリオエディタで作成したシナリオ（WSN形式）

## 対応シナリオ格納形式
以下のシナリオ格納形式に対応しています。
* ディレクトリに格納されたシナリオ
* CAB拡張子で圧縮されたシナリオ
* ZIP拡張子で圧縮されたシナリオ
* WSN拡張子で圧縮されたシナリオ

## シナリオ概要取得・判定方法
ディレクトリや圧縮ファイルの中にあるSummary.wsmファイル、Summary.xmlファイルを解析してシナリオ概要を取得、およびシナリオかどうかを判定しています。

このため、Test-CardWirthScenarioコマンドレットについては厳密なシナリオの判定ではないことにご注意ください。

## インストール方法

PowerShell Galleryで公開する予定
```powershell
Install-Module -Name CardWirthScenarioSummaryReader -Scope CurrentUser
```

## 各コマンドレットの簡単な説明

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

シナリオであり、かつ、指定条件にあてはまる場合はTrueが返ります。それ以外の場合はFalseが返ります。

このコマンドレットはスクリプトで使うことを想定しているため、エイリアスはありません。

#### 使用例
```powershell
Test-CardWirthScenario ゴブリンの洞窟
True
```

#### 構文
```powershell
Test-CardWirthScenario ([-Path] <String[]> | -LiteralPath <String[]>)
    [-ContainerType {Any | Directory | CabFile | ZipFile | WsnFile}] 
    [-ScenarioType {Any | Classic | Next | Wsn}]
    [<CommonParameters>]
```


## 出力形式の説明

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
|LastWriteTime|シナリオ格納場所の更新日時|
|FullName|シナリオ格納場所の絶対パス|
|Level*|対象レベル|
|PSPath*|シナリオ格納場所の絶対パス(FullNameと同じ)|

## 想定される質問

### 日本語が文字化けする
  * 日本語対応フォントを使用してください。
    * MS ゴシック
    * RictyDiminished (https://github.com/edihbrandon/RictyDiminished)

### シナリオ概要が取得できない
  * パスに`[]`の文字が含まれている場合、-LiteralPathパラメーターを使わないと`[]`の文字がワイルドカードとして認識され、意図したシナリオが取得できなくなります。
    * -LiteralPathパラメーターを使用してください。
  * Get-CardWirthScenarioコマンドレットを使用してエラーメッセージを確認してください。主なシナリオ概要が取得できない原因は以下の通りです。
    * 未対応のシナリオ格納形式だった。(ディレクトリ、.cab、.zip、.wsnファイル以外をパスに指定した）
    * ディレクトリ、圧縮ファイルの中にSummary.wsm、Summary.xmlファイルがどちらも存在しなかった。
    * 圧縮ファイルがパスワード付きZIPで解析できなかった。
    * Summary.wsm、Summary.xmlの読み込みに失敗した。

## 一歩踏み込んだ使い方

現在のディレクトリにあるディレクトリに格納されたシナリオをZIP圧縮するワンライナー
```powershell
lscw -Directory | % { Compress-Archive -LiteralPath $_.FullName -DestinationPath ($_.FullName + ".zip") -Force }
```

現在のディレクトリにあるシナリオを対象レベル別のディレクトリに分類するワンライナー
```powershell
lscw | Group-Object -Property Level | % { $dir = mkdir $_.Name -Force; $_.Group | % { Move-Item -LiteralPath $_.FullName -Destination $dir.FullName } }
```

## ライセンス

MITライセンスです。



[^anchor]: 脚注の本文。本文に設置したアンカーと同じ文字列段落をには改行を入れないでください。
