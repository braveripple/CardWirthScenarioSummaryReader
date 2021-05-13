# CardWirthScenarioSummaryReader(CWSSR)
CardWirthのシナリオディレクトリや圧縮ファイルからシナリオ概要を取得するPowerShellモジュール

***デモ***

![デモ](https://github.com/braveripple/CardWirthScenarioSummaryReader/blob/master/Assets/demo.gif?raw=true)

## 動作環境
* Windows
  * Windows PowerShell 5.1
  * PowerShell Core

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
* CardWirthのシナリオエディタで作成したシナリオ（以下、クラシック形式）
* CardWirthNextのシナリオエディタで作成したシナリオ（以下、NEXT形式）
* CardWirthPy Reboot のシナリオエディタで作成したシナリオ（以下、WSN形式）

## 対応シナリオ格納形式
以下のシナリオ格納形式に対応しています。
* ディレクトリに格納されたシナリオ
* CAB拡張子で圧縮されたシナリオ
* ZIP拡張子で圧縮されたシナリオ
* WSN拡張子で圧縮されたシナリオ

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
> Get-CardWirthScenario .\ゴブリンの洞窟\

ScenarioType ContainerType LevelMin LevelMax Name                     Author               Description
------------ ------------- -------- -------- ----                     ------               -----------
Classic      Directory            1        3 ゴブリンの洞窟           齋藤 洋              　町外れの洞窟にゴブリンと…
```

### Get-CardWirthScenarioList

指定したパスとパス以下のシナリオ概要一覧を取得します。

パスを省略した場合は現在のディレクトリと現在のディレクトリ以下のシナリオ概要一覧を取得します。

#### エイリアス
`lscw`

#### 使用例
```powershell
> Get-CardWirthScenarioList

ScenarioType ContainerType LevelMin LevelMax Name                     Author               Description
------------ ------------- -------- -------- ----                     ------               -----------
Classic      Directory            1        3 ゴブリンの洞窟           齋藤 洋              　町外れの洞窟にゴブリンと…
Classic      Directory            0        0 交易都市リューン         斎藤 洋                冒険者よ、旅の準備は本当…
```

#### 構文
```powershell
    Get-CardWirthScenarioList 
        [-CabFile]
        [-Classic]
        [-Directory]
        -LiteralPath <String[]>
        [-Next]
        [-Recurse]
        [-Wsn]
        [-WsnFile]
        [-ZipFile]
        [<CommonParameters>]

    Get-CardWirthScenarioList
       [[-Path] <String[]>]
       [-CabFile]
       [-Classic]
       [-Directory]
       [-Next]
       [-Recurse]
       [-Wsn]
       [-WsnFile]
       [-ZipFile]
       [<CommonParameters>]
```


### Test-CardWirthScenario

指定したパスがCardWirthのシナリオかどうかを判定します。

シナリオであり、かつ、指定条件にあてはまる場合はTrueが返ります。それ以外の場合はFalseが返ります。

このコマンドレットはスクリプトで使うことを想定しているため、エイリアスはありません。

```powershell
Test-CardWirthScenario ゴブリンの洞窟
True
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

## 一歩踏み込んだ使い方

現在のディレクトリにあるディレクトリに格納されたシナリオをZIP圧縮するワンライナー
```powershell
lscw -Directory | % { Compress-Archive -LiteralPath $_.FullName -DestinationPath ($_.FullName + ".zip") -Force }
```

現在のディレクトリにあるシナリオを対象レベル別のディレクトリに分類するワンライナー
```powershell
lscw | Group-Object -Property Level | % { $dir = mkdir $_.Name -Force; $_.Group | % { Move-Item -LiteralPath $_.FullName -Destination $dir.FullName } }
```

## 想定される質問

### シナリオ概要が取得できない
  * パスに`[]`の文字が含まれている場合、-LiteralPathパラメーターを使わないと`[]`の文字がワイルドカードとして認識され、意図したシナリオが取得できなくなります。
  * Get-CardWirthScenarioコマンドレットを使用してエラーメッセージを確認してください。主なシナリオ概要が取得できない原因は以下の通りです。
    * 未対応のシナリオ格納形式だった。(ディレクトリ、.cab、.zip、.wsnファイル以外をパスに指定した）
    * ディレクトリ、圧縮ファイルの中にSummary.wsm、Summary.xmlファイルがどちらも存在しなかった。
    * 圧縮ファイルがパスワード付きZIPで解析できなかった。
    * Summary.wsm、Summary.xmlの読み込みに失敗した。

## ライセンス

MITライセンスです。

