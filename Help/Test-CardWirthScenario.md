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

シナリオであり、かつ、指定条件にあてはまる場合はTrueが返ります。

以下の場合はすべてFalseが返ります。

* シナリオが見つからない、Summaryファイルが見つからない、Summaryファイルの読み込みに失敗したなどの例外が発生したとき

* パラメーターに指定したシナリオ形式やシナリオ格納形式に一致しなかったとき


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
シナリオ格納形式を指定します。

-Any シナリオ格納形式を考慮しません。

-Directory ディレクトリに格納されたシナリオかどうかを判定します。

-CabFile .cab拡張子で圧縮されたシナリオかどうかを判定します。

-ZipFile .zip拡張子で圧縮されたシナリオかどうかを判定します。

-WsnFile .wsn拡張子で圧縮されたシナリオかどうかを判定します。

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
シナリオ形式を指定します。

-Any シナリオ形式を考慮しません。

-Classic クラシック形式のシナリオかどうかを判定します。

-Next NEXT形式のシナリオかどうかを判定します。

-Wsn WSN形式のシナリオかどうかを判定します。

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
