# CardWirthScenarioSummaryReader(CWSSR)
CardWirthのシナリオディレクトリや圧縮ファイルからシナリオ概要を取得するPowerShellモジュール

***デモ***

![デモ](https://github.com/braveripple/CardWirthScenarioSummaryReader/blob/master/Assets/demo.gif?raw=true)

## 動作環境
* Windows
  * Windows PowerShell 5.1
  * PowerShell Core

## 機能
以下の３つのコマンドレットがあります
* Get-CardWirthScenarioコマンドレットによるシナリオ概要の取得
* Get-CardWirthScenarioListコマンドレットによるシナリオ概要一覧の取得
* Test-CardWirthScenarioコマンドレットによるシナリオの判定

## 対応しているシナリオ形式
以下のシナリオ形式に対応しています
* CardWirthのシナリオエディタで作成したシナリオ（以下、Classic形式）
* CardWirthNextのシナリオエディタで作成したシナリオ（以下、Next形式）
* CardWirthPy Reboot のシナリオエディタで作成したシナリオ（以下、WSN形式）

## 対応しているシナリオ格納形式
以下のシナリオ格納形式に対応しています
* ディレクトリに格納されたシナリオ
* CAB拡張子で圧縮されたシナリオ
* ZIP拡張子で圧縮されたシナリオ
* WSN拡張子で圧縮されたシナリオ

## 各コマンドレットの簡単な説明

### Get-CardWirthScenario
指定したパスのシナリオ概要を取得します。
* エイリアス：`gcw`
```powershell
> Get-CardWirthScenario .\ゴブリンの洞窟\

ScenarioType ContainerType LevelMin LevelMax Name                     Author               Description
------------ ------------- -------- -------- ----                     ------               -----------
Classic      Directory            1        3 ゴブリンの洞窟           齋藤 洋              　町外れの洞窟にゴブリンと…
```

### Get-CardWirthScenarioList

指定したパスとパス以下のシナリオ概要一覧を取得します。

パスを省略した場合はカレントディレクトリとカレントディレクトリ以下のシナリオ概要一覧を取得します。

* エイリアス：`lscw`

```powershell
> Get-CardWirthScenarioList

ScenarioType ContainerType LevelMin LevelMax Name                     Author               Description
------------ ------------- -------- -------- ----                     ------               -----------
Classic      Directory            1        3 ゴブリンの洞窟           齋藤 洋              　町外れの洞窟にゴブリンと…
Classic      Directory            0        0 交易都市リューン         斎藤 洋                冒険者よ、旅の準備は本当…
```

### Test-CardWirthScenario

指定したパスがCardWirthのシナリオかどうかを判定します。

シナリオである、かつ指定条件にあてはまる場合はTrueが返ります。それ以外の場合はFalseが返ります。

このコマンドレットはスクリプトで使用することを想定しているため、エイリアスはありません。

```powershell
Test-CardWirthScenario ゴブリンの洞窟
True
```

## 出力形式の説明

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
|LevelAverage*|対象レベル平均値|
|PSPath*|シナリオ格納場所の絶対パス(FullNameと同じ)|

以下の場合はすべてFalseが返ります。
* シナリオが見つからない、Summaryファイルが見つからない、Summaryファイルの読み込みに失敗したなどの例外が発生したとき
* パラメーターに指定したシナリオ形式やシナリオ格納形式に一致しなかったとき
