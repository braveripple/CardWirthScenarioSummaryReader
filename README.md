# CardWirthScenarioSummaryReader(CWSSR)
CardWirthのシナリオディレクトリやアーカイブファイルからシナリオ概要を取得するPowerShellモジュール

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
* ZIP拡張子で圧縮されたシナリオ
* CAB拡張子で圧縮されたシナリオ
* WSN拡張子で圧縮されたシナリオ

## 各コマンドレットの簡単な説明

### Get-CardWirthScenario
指定したパスのシナリオ概要を取得します。
* エイリアス：`gcw`
```powershell
Get-CardWirthScenario ゴブリンの洞窟
```

### Get-CardWirthScenarioList

指定したパスとパス以下のシナリオ概要一覧を取得します。

パスを省略した場合はカレントディレクトリとカレントディレクトリ以下のシナリオ概要一覧を取得します。

* エイリアス：`lscw`

```powershell
Get-CardWirthScenarioList
```

### Test-CardWirthScenario

指定したパスがCardWirthのシナリオかどうかを判定します。

シナリオの場合はTrueが返ります。

以下の場合はすべてFalseが返ります。
* シナリオが見つからない、Summaryファイルが見つからない、Summaryファイルの読み込みに失敗したなどの例外が発生したとき
* パラメーターに指定したシナリオ形式やシナリオ格納形式に一致しなかったとき

このコマンドレットはスクリプトで使用することを想定しているため、エイリアスはありません。

```powershell
Test-CardWirthScenario ゴブリンの洞窟
True
```
