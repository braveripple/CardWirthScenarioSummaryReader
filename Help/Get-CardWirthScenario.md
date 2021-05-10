---
external help file: CardWirthScenarioSummaryReader.dll-Help.xml
Module Name: CardWirthScenarioSummaryReader
online version:
schema: 2.0.0
---

# Get-CardWirthScenario

## SYNOPSIS
指定したパスのシナリオ概要を取得します。

## SYNTAX

### Path (Default)
```
Get-CardWirthScenario [-Path] <String[]> [<CommonParameters>]
```

### LiteralPath
```
Get-CardWirthScenario -LiteralPath <String[]> [<CommonParameters>]
```

## DESCRIPTION
指定したパスのシナリオ概要を取得します。

基本的には１つのシナリオ概要を取得するためのコマンドレットですが、ワイルドカードを使用して複数のシナリオ概要を取得することができます。

## EXAMPLES

### Example 1:指定したパスのシナリオの概要を取得する
```powershell
Get-CardWirthScenario .\ゴブリンの洞窟\

ScenarioType ContainerType LevelMin LevelMax Name                     Author               Description
------------ ------------- -------- -------- ----                     ------               -----------
Classic      Directory            1        3 ゴブリンの洞窟           齋藤 洋              　町外れの洞窟にゴブリンと…
```

### Example 2:「者の」という文字が含まれるシナリオの概要を取得する
```powershell
Get-CardWirthScenario *者の*

ScenarioType ContainerType LevelMin LevelMax Name                     Author               Description
------------ ------------- -------- -------- ----                     ------               -----------
Classic      Directory            1        3 見えざる者の願い         Kuranuki@groupAsk    数年前、カルバチアの街の近…
Classic      Directory            3        5 賢者の選択               斎藤 洋              　如何なる時、如何なる場面…
```

## PARAMETERS

### -LiteralPath
シナリオのパスを指定します。LiteralPathの値は、入力されたとおりに使用されます。ワイルドカードとして解釈される文字はありません。

```yaml
Type: String[]
Parameter Sets: LiteralPath
Aliases: PSPath, LP

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Path
シナリオのパスを指定します。ワイルドカード文字を使用できます。このパラメーターは必須ですが、パラメーター名Pathは省略可能です。

```yaml
Type: String[]
Parameter Sets: Path
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

## OUTPUTS

### BraveRipple.CardWirthScenarioSummaryReaderTool.Entities.ScenarioSummary

## NOTES

## RELATED LINKS
