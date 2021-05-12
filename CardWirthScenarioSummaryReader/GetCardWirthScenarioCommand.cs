using System;
using System.Linq;
using System.Management.Automation;
using BraveRipple.CardWirthScenarioSummaryReaderTool;
using BraveRipple.CardWirthScenarioSummaryReaderTool.Entities;
using BraveRipple.CardWirthScenarioSummaryReaderTool.Exceptions;

namespace CardWirthScenarioSummaryReader
{
    /// <summary>
    /// パスからCardWirthのシナリオ情報を取得する
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CardWirthScenario", DefaultParameterSetName = "Path")]
    [OutputType(typeof(ScenarioSummary))]
    [Alias("gcw")]
    public class GetCardWirthScenarioCommand : PSCmdlet
    {

        #region command parameters

        /// <summary>
        /// Gets or sets the path parameter to the command.
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "Path",
                   Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
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
        [Parameter(ParameterSetName = "LiteralPath",
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

        #endregion command parameters

        #region parameter data

        /// <summary>
        /// The properties to be retrieved.
        /// </summary>
        private string[] _paths;

        private bool _suppressWildcardExpansion = false;

        #endregion parameter data

        #region command code

        protected override void ProcessRecord()
        {
            WriteVerbose("Path:" + String.Join(",", Path));
            WriteVerbose("LiteralPath:" + String.Join(",", LiteralPath));
            WriteVerbose("_paths:" + String.Join(",", _paths));
            WriteVerbose("_suppressWildcardExpansion:" + _suppressWildcardExpansion.ToString());

            foreach (string path in _paths)
            {

                WriteVerbose("path:" + path);

                var items = Enumerable.Empty<PSObject>();

                try
                {
                    // Get-Item的なものを呼ぶ
                    items = InvokeProvider.Item.Get(new string[] { path }, true, _suppressWildcardExpansion) ?? Enumerable.Empty<PSObject>();

                    // Pathの場合、結果は複数返ってくる可能性がある
                    // /path/to/NotExist* -> /path/to/にNotExistが存在しなくてもエラーにはならない
                    // /path/to/NotExist  -> /path/to/にNotExistが存在しないとエラーを返す
                    // /path/to/Exist     -> /path/to/にExistがあれば、PSObjectが1個入ったEnumerable(リストみたいなもの)が返る
                    // /path/to/Exist*    -> /path/to/にExistA, ExistB, ExistCが存在すればPSObjectが3個入ったEnumerableが返る

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

                foreach (var item in items)
                {
                    // System.Management.Automation.PSObject
                    WriteVerbose("item:" + item.ToString());

                    // ファイルとディレクトリ両方扱いたいのでFileSystemInfoに変換する
                    if (item.BaseObject is System.IO.FileSystemInfo info)
                    {
                        try
                        {
                            // フルパスからシナリオ情報を取得する
                            WriteVerbose("fullName:" + info.FullName);
                            WriteObject(CardWirthScenario.GetScenarioSummary(info.FullName));
                        }
                        catch (ScenarioNotFoundException ex)
                        {
                            WriteError(new ErrorRecord(ex, "ScenarioNotFoundException", ErrorCategory.ObjectNotFound, info.FullName));
                            continue;
                        }
                        catch (UnsupportedContainerTypeException ex)
                        {
                            WriteError(new ErrorRecord(ex, "UnsupportedContainerTypeException", ErrorCategory.InvalidArgument, info.FullName));
                            continue;
                        }
                        catch (InvalidScenarioException ex)
                        {
                            WriteError(new ErrorRecord(ex, "InvalidScenarioException", ErrorCategory.InvalidData, info.FullName));
                            continue;
                        }
                        catch (Exception ex)
                        {
                            WriteError(new ErrorRecord(ex, "Exception", ErrorCategory.WriteError, info.FullName));
                            continue;
                        }
                    }
                }
            }
        }

        #endregion command code

    }
}
