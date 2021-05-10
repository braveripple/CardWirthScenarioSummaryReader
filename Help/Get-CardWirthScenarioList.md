---
external help file: CardWirthScenarioSummaryReader.dll-Help.xml
Module Name: CardWirthScenarioSummaryReader
online version:
schema: 2.0.0
---

# Get-CardWirthScenarioList

## SYNOPSIS
指定したパスと、パス以下のシナリオ概要を取得します。

## SYNTAX

### Items (Default)
```
Get-CardWirthScenarioList [[-Path] <String[]>] [-Recurse] [-Classic] [-Next] [-Wsn] [-Directory] [-ZipFile]
 [-CabFile] [-WsnFile] [<CommonParameters>]
```

### LiteralItems
```
Get-CardWirthScenarioList -LiteralPath <String[]> [-Recurse] [-Classic] [-Next] [-Wsn] [-Directory] [-ZipFile]
 [-CabFile] [-WsnFile] [<CommonParameters>]
```

## DESCRIPTION
指定したパスと、パス以下のシナリオ概要を取得します。

パスを省略した場合は現在のディレクトリと、現在のディレクトリ以下のシナリオ概要を取得します。

## EXAMPLES

### Example 1:現在のディレクトリのシナリオの概要を取得する
```powershell
Get-CardWirthScenarioList

ScenarioType ContainerType LevelMin LevelMax Name                     Author               Description
------------ ------------- -------- -------- ----                     ------               -----------
Classic      Directory            1        3 ゴブリンの洞窟           齋藤 洋              　町外れの洞窟にゴブリンと…
Classic      Directory            0        0 交易都市リューン         斎藤 洋                冒険者よ、旅の準備は本当…
```

現在のディレクトリに「ゴブリンの洞窟」と「交易都市リューン」がある場合、上記のように出力されます。

### Example 2:指定したディレクトリのシナリオの概要を取得する
```powershell
Get-CardWirthScenarioList .\Ask\

ScenarioType ContainerType LevelMin LevelMax Name                     Author               Description
------------ ------------- -------- -------- ----                     ------               -----------
Classic      Directory            1        3 ゴブリンの洞窟           齋藤 洋              　町外れの洞窟にゴブリンと…
Classic      Directory            0        0 交易都市リューン         斎藤 洋                冒険者よ、旅の準備は本当…
```

「Ask」ディレクトリに「ゴブリンの洞窟」と「交易都市リューン」がある場合、上記のように出力されます。

### Example 3:指定したパスのシナリオの概要を取得する
```powershell
Get-CardWirthScenarioList .\ゴブリンの洞窟\

ScenarioType ContainerType LevelMin LevelMax Name                     Author               Description
------------ ------------- -------- -------- ----                     ------               -----------
Classic      Directory            1        3 ゴブリンの洞窟           齋藤 洋              　町外れの洞窟にゴブリンと…
```

`Get-CardWirthScenario .\ゴブリンの洞窟\`とほぼ同じですが、「.\ゴブリンの洞窟\」ディレクトリ以下にシナリオが存在する場合はそのシナリオ概要が一緒に出力されます。

### Example 4:現在のディレクトリのサブディレクトリ以下のシナリオ概要を取得する
```powershell
Get-CardWirthScenarioList -Recurse

ScenarioType ContainerType LevelMin LevelMax Name                     Author               Description
------------ ------------- -------- -------- ----                     ------               -----------
Classic      Directory            1        3 ゴブリンの洞窟           齋藤 洋              　町外れの洞窟にゴブリンと…
Classic      Directory            0        0 交易都市リューン         斎藤 洋                冒険者よ、旅の準備は本当…
```

現在のディレクトリにシナリオがなく、サブディレクトリ以下に「ゴブリンの洞窟」と「交易都市リューン」がある場合、上記のように出力されます。

## PARAMETERS

### -CabFile
CAB拡張子で圧縮されたシナリオ概要のみ出力します。

他のシナリオ格納形式のスイッチパラメーターとは併用できません。

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Classic
クラッシック形式のシナリオ概要のみ出力します。

他のシナリオ形式のスイッチパラメーターとは併用できません。

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Directory
ディレクトリに格納されたシナリオ概要のみ出力します。

他のシナリオ格納形式のスイッチパラメーターとは併用できません。

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LiteralPath
シナリオのパスまたは、シナリオを含むディレクトリのパスを指定します。LiteralPathの値は、入力されたとおりに使用されます。ワイルドカードとして解釈される文字はありません。

```yaml
Type: String[]
Parameter Sets: LiteralItems
Aliases: PSPath, LP

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Next
NEXT形式のシナリオ概要のみ出力します。

他のシナリオ形式のスイッチパラメーターとは併用できません。

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
シナリオのパスまたは、シナリオを含むディレクトリのパスを指定します。ワイルドカード文字を使用できます。このパラメーターを省略した場合、現在のディレクトリ(`.`)が指定されます。

```yaml
Type: String[]
Parameter Sets: Items
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Recurse
指定したディレクトリのサブディレクトリ以下のシナリオを取得します。

このパラメーターはシナリオのパスにディレクトリを指定したときのみ機能します。

サブディレクトリの階層指定はできません。

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: s

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Wsn
WSN形式のシナリオ概要のみ出力します。

他のシナリオ形式のスイッチパラメーターとは併用できません。

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: Py

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WsnFile
WSN拡張子で圧縮されたシナリオ概要のみ出力します。

他のシナリオ格納形式のスイッチパラメーターとは併用できません。

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ZipFile
ZIP拡張子で圧縮されたシナリオ概要のみ出力します。

他のシナリオ格納形式のスイッチパラメーターとは併用できません。

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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
