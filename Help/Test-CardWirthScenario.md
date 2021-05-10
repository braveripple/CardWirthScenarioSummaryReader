---
external help file: CardWirthScenarioSummaryReader.dll-Help.xml
Module Name: CardWirthScenarioSummaryReader
online version:
schema: 2.0.0
---

# Test-CardWirthScenario

## SYNOPSIS
指定したパスがCardWirthのシナリオかどうかを判定します。

## SYNTAX

### Path (Default)
```
Test-CardWirthScenario [-Path] <String[]> [-ScenarioType <ScenarioParameterType>]
 [-ContainerType <ContainerParameterType>] [<CommonParameters>]
```

### LiteralPath
```
Test-CardWirthScenario -LiteralPath <String[]> [-ScenarioType <ScenarioParameterType>]
 [-ContainerType <ContainerParameterType>] [<CommonParameters>]
```

## DESCRIPTION
指定したパスがCardWirthのシナリオかどうかを判定します。

シナリオである、かつ指定条件にあてはまる場合はTrueが返ります。それ以外の場合はFalseが返ります。

このコマンドレットはスクリプトで使うことを想定しているため、エイリアスはありません。

## EXAMPLES

### Example 1:指定したパスがシナリオかどうか判定する
```powershell
Test-CardWirthScenario .\ゴブリンの洞窟\
True
```

### Example 2:指定したパスがクラシック形式のシナリオかどうか判定する
```powershell
Test-CardWirthScenario .\ゴブリンの洞窟\ -ScenarioType Classic
True
```

### Example 3:指定したパスがディレクトリに格納されているシナリオかどうか判定する
```powershell
Test-CardWirthScenario .\ゴブリンの洞窟\ -ContainerType Directory
True
```

### Example 4:指定したパスがクラシック形式かつ、ディレクトリに格納されているシナリオかどうか判定する
```powershell
Test-CardWirthScenario .\ゴブリンの洞窟\ -ScenarioType Classic -ContainerType Directory
True
```

## PARAMETERS

### -ContainerType
{{ Fill ContainerType Description }}

```yaml
Type: ContainerParameterType
Parameter Sets: (All)
Aliases:
Accepted values: Any, Directory, CabFile, ZipFile, WsnFile

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

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

### -ScenarioType
{{ Fill ScenarioType Description }}

```yaml
Type: ScenarioParameterType
Parameter Sets: (All)
Aliases:
Accepted values: Any, Classic, Next, Wsn

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

### BraveRipple.CardWirthScenarioSummaryReaderTool.Enums.ScenarioParameterType

### BraveRipple.CardWirthScenarioSummaryReaderTool.Enums.ContainerParameterType

## OUTPUTS

### System.Boolean

## NOTES

## RELATED LINKS
