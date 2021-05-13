using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using BraveRipple.CardWirthScenarioSummaryReaderTool;
using BraveRipple.CardWirthScenarioSummaryReaderTool.Entities;
using BraveRipple.CardWirthScenarioSummaryReaderTool.Enums;

namespace CardWirthScenarioSummaryReader
{
    /// <summary>
    /// パスと、パス以下のシナリオ情報を取得する
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CardWirthScenarioList", DefaultParameterSetName = "Items")]
    [OutputType(typeof(ScenarioSummary))]
    public class GetCardWirthScenarioListCommand : PSCmdlet
    {
        /// <summary>
        /// The string declaration for the Items parameter set in this command.
        /// </summary>
        /// <remarks>
        /// The "Items" parameter set includes the following parameters:
        ///     -filter
        ///     -recurse
        /// </remarks>
        private const string childrenSet = "Items";
        private const string literalChildrenSet = "LiteralItems";

        #region command parameters

        /// <summary>
        /// Gets or sets the path for the operation.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = childrenSet,
                   ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string[] Path
        {
            get
            {
                return _paths;
            }

            set
            {
                _paths = value;
            }
        }

        /// <summary>
        /// Gets or sets the literal path parameter to the command.
        /// </summary>
        [Parameter(ParameterSetName = literalChildrenSet,
                   Mandatory = true, ValueFromPipeline = false, ValueFromPipelineByPropertyName = true)]
        [Alias("PSPath", "LP")]
        public string[] LiteralPath
        {
            get
            {
                return _paths;
            }

            set
            {
                _suppressWildcardExpansion = true;
                _paths = value;
            }
        }

        /// <summary>
        /// Gets or sets the recurse switch.
        /// </summary>
        [Parameter]
        [Alias("s")]
        public SwitchParameter Recurse
        {
            get
            {
                return _recurse;
            }

            set
            {
                _recurse = value;
            }
        }

        [Parameter]
        public SwitchParameter Classic { get; set; }

        [Parameter]
        public SwitchParameter Next { get; set; }

        [Alias("Py")]
        [Parameter]
        public SwitchParameter Wsn { get; set; }

        [Parameter]
        public SwitchParameter Directory { get; set; }

        [Parameter]
        public SwitchParameter ZipFile { get; set; }

        [Parameter]
        public SwitchParameter CabFile { get; set; }

        [Parameter]
        public SwitchParameter WsnFile { get; set; }

        #endregion command parameters

        #region parameter data

        /// <summary>
        /// The path for the get-location operation.
        /// </summary>
        private string[] _paths;

        /// <summary>
        /// Determines if the command should do recursion.
        /// </summary>
        private bool _recurse;

        private bool _suppressWildcardExpansion;

        #endregion parameter data

        #region command code

        /// <summary>
        /// The main execution method for the get-childitem command.
        /// </summary>
        protected override void ProcessRecord()
        {

            if (_paths == null || _paths.Length == 0)
            {
                WriteVerbose("path is null or 0 length.");
                _paths = new string[] { string.Empty };
            }

            WriteVerbose("Path:" + String.Join(",", Path));
            WriteVerbose("LiteralPath:" + String.Join(",", LiteralPath));
            WriteVerbose("_paths:" + String.Join(",", _paths));
            WriteVerbose("_suppressWildcardExpansion:" + _suppressWildcardExpansion.ToString());
            WriteVerbose("Classic:" + Classic.ToString());
            WriteVerbose("Next:" + Next.ToString());
            WriteVerbose("Wsn:" + Wsn.ToString());
            WriteVerbose("Directory:" + Directory.ToString());
            WriteVerbose("ZipFile:" + ZipFile.ToString());
            WriteVerbose("CabFile:" + CabFile.ToString());
            WriteVerbose("WsnFile:" + WsnFile.ToString());

            foreach (string path in _paths)
            {
                WriteVerbose("path:" + path);

                // バイナリモジュールから呼べるTest-PathやChildItem的なメソッドでLiteralPathフラグが使えないので、
                // あらかじめエスケープしたパスを用意しておき必要な場合はそれを使う
                var escapedPath = System.Text.RegularExpressions.Regex.Replace(path, "([\\[\\]])", "`$1");
                WriteVerbose("escapedPath:" + escapedPath);

                switch (ParameterSetName)
                {
                    case childrenSet:
                    case literalChildrenSet:

                        var scenarioFullNames = new List<string>();

                        // `lscw ゴブリンの洞窟`と指定したとき、ChildItemだけだとゴブリンの洞窟以下のファイルに対して
                        // GetScenarioSummaryをしてしまうため、フォルダ自体にGetScenarioSummaryする処理
                        // -----------------------👇ここから👇-----------------------
                        if (InvokeProvider.Item.Exists(path, true, _suppressWildcardExpansion))
                        {
                            WriteVerbose("path is exists.");

                            if (InvokeProvider.Item.IsContainer((_suppressWildcardExpansion ? escapedPath : path)))
                            {
                                WriteVerbose("path is directory.");

                                // 上の条件分岐で存在しているディレクトリに対してしかGet-Itemは動かないのでここで例外は起こらないはず
                                var items = InvokeProvider.Item.Get(new string[] { path }, true, _suppressWildcardExpansion) ?? Enumerable.Empty<PSObject>();

                                foreach (var item in items)
                                {
                                    if (item.BaseObject is System.IO.FileSystemInfo info)
                                    {
                                        try
                                        {
                                            // フルパスからシナリオ情報を取得する
                                            WriteVerbose("fullName:" + info.FullName);
                                            var scenarioSummary = CardWirthScenario.GetScenarioSummary(info.FullName);

                                            // 次のChildItemで同じフォルダを見る可能性があるので
                                            // 処理対象外にするためにシナリオのパスを覚えておく
                                            scenarioFullNames.Add(scenarioSummary.FullName);

                                            // シナリオ形式のフィルタリング
                                            if (Classic && scenarioSummary.ScenarioType != ScenarioType.Classic)
                                            {
                                                continue;
                                            }
                                            else if (Next && scenarioSummary.ScenarioType != ScenarioType.Next)
                                            {
                                                continue;
                                            }
                                            else if (Wsn && scenarioSummary.ScenarioType != ScenarioType.Wsn)
                                            {
                                                continue;
                                            }
                                            // 格納形式のフィルタリング
                                            if (Directory && scenarioSummary.ContainerType != ContainerType.Directory)
                                            {
                                                continue;
                                            }
                                            else if (ZipFile && scenarioSummary.ContainerType != ContainerType.ZipFile)
                                            {
                                                continue;
                                            }
                                            else if (CabFile && scenarioSummary.ContainerType != ContainerType.CabFile)
                                            {
                                                continue;
                                            }
                                            else if (WsnFile && scenarioSummary.ContainerType != ContainerType.WsnFile)
                                            {
                                                continue;
                                            }

                                            WriteObject(scenarioSummary);
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteVerbose("error:" + ex.Message);
                                            continue;
                                        }
                                    }
                                }
                            }
                        }
                        // -----------------------👆ここまで👆-----------------------


                        // ここから指定したパス以下のファイル・フォルダを調査する
                        var childItems = Enumerable.Empty<PSObject>();
                        try
                        {
                            childItems = InvokeProvider.ChildItem.Get((_suppressWildcardExpansion ? escapedPath : path), _recurse) ?? Enumerable.Empty<PSObject>();
                        }
                        catch (PSNotSupportedException notSupported)
                        {
                            WriteError(new ErrorRecord(notSupported.ErrorRecord, notSupported));
                            continue;
                        }
                        catch (DriveNotFoundException driveNotFound)
                        {
                            WriteError(new ErrorRecord(driveNotFound.ErrorRecord, driveNotFound));
                            continue;
                        }
                        catch (ProviderNotFoundException providerNotFound)
                        {
                            WriteError(new ErrorRecord(providerNotFound.ErrorRecord, providerNotFound));
                            continue;
                        }
                        catch (ItemNotFoundException pathNotFound)
                        {
                            WriteError(new ErrorRecord(pathNotFound.ErrorRecord, pathNotFound));
                            continue;
                        }

                        foreach (var childItem in childItems)
                        {

                            WriteVerbose("childItem:" + childItem);

                            if (childItem.BaseObject is System.IO.FileSystemInfo info)
                            {

                                // Get-ChildItemでとってきた要素すべてを調査していてはきりがないのである程度選別する;
                                if (info is System.IO.FileInfo file)
                                {
                                    // 拡張子がサポート対象外のファイルの場合、シナリオ情報取得をスキップする
                                    var extension = System.IO.Path.GetExtension(file.FullName).ToLower();
                                    if (extension != ".zip" && extension != ".cab" && extension != ".wsn")
                                    {
                                        WriteVerbose("path is not supported archive file. skipped acquisition of scenario information.");
                                        continue;
                                    }
                                }

                                // 調査済みパスの場合、シナリオ情報取得をスキップする
                                if (scenarioFullNames.Contains(info.FullName))
                                {
                                    WriteVerbose("path has been investigated. skipped acquisition of scenario information.");
                                    continue;
                                }

                                try
                                {

                                    // フルパスからシナリオ情報を取得する
                                    // TODO:ディレクトリの場合、シナリオではないのならエラー表示を無視したい
                                    // 再帰のとき、エラー処理をどうするか考え中
                                    var scenarioSummary = CardWirthScenario.GetScenarioSummary(info.FullName);

                                    // シナリオ形式のフィルタリング
                                    if (Classic && scenarioSummary.ScenarioType != ScenarioType.Classic)
                                    {
                                        continue;
                                    }
                                    else if (Next && scenarioSummary.ScenarioType != ScenarioType.Next)
                                    {
                                        continue;
                                    }
                                    else if (Wsn && scenarioSummary.ScenarioType != ScenarioType.Wsn)
                                    {
                                        continue;
                                    }
                                    // 格納形式のフィルタリング
                                    if (Directory && scenarioSummary.ContainerType != ContainerType.Directory)
                                    {
                                        continue;
                                    }
                                    else if (ZipFile && scenarioSummary.ContainerType != ContainerType.ZipFile)
                                    {
                                        continue;
                                    }
                                    else if (CabFile && scenarioSummary.ContainerType != ContainerType.CabFile)
                                    {
                                        continue;
                                    }
                                    else if (WsnFile && scenarioSummary.ContainerType != ContainerType.WsnFile)
                                    {
                                        continue;
                                    }

                                    WriteObject(scenarioSummary);
                                }
                                catch (Exception ex)
                                {
                                    WriteVerbose("error:" + ex.Message);
                                    continue;
                                }
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion command code
    }
}