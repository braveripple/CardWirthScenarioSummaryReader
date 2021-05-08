using System;
using System.Linq;
using System.Management.Automation;
using BraveRipple.CardWirthScenarioSummaryReaderTool;
using BraveRipple.CardWirthScenarioSummaryReaderTool.Enums;

namespace CardWirthScenarioSummaryReader
{
    /// <summary>
    /// パスがCardWirthのシナリオかどうかを判定する
    /// </summary>
    [Cmdlet(VerbsDiagnostic.Test, "CardWirthScenario", DefaultParameterSetName = "Path")]
    [OutputType(typeof(Boolean))]
    public class TestCardWirthScenarioCommand : PSCmdlet
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

        [Parameter(Mandatory = false, ValueFromPipeline = false, ValueFromPipelineByPropertyName = true)]
        public ScenarioParameterType ScenarioType { get; set; } = ScenarioParameterType.Any;

        [Parameter(Mandatory = false, ValueFromPipeline = false, ValueFromPipelineByPropertyName = true)]
        public ContainerParameterType ContainerType { get; set; } = ContainerParameterType.Any;

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
            WriteVerbose("ScenarioType:" + ScenarioType.ToString());
            WriteVerbose("ContaierType:" + ContainerType.ToString());

            foreach (string path in _paths)
            {

                WriteVerbose("path:" + path);

                if (!InvokeProvider.Item.Exists(path, true, _suppressWildcardExpansion))
                {
                    WriteObject(false);
                    continue;
                }
                
                var items = Enumerable.Empty<PSObject>();

                try
                {
                    items = InvokeProvider.Item.Get(new string[] { path }, true, _suppressWildcardExpansion) ?? Enumerable.Empty<PSObject>();
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
                    WriteVerbose("item:" + item.ToString());

                    if (item.BaseObject is System.IO.FileSystemInfo info)
                    {
                        try
                        {
                            // フルパスからシナリオかどうか判定する
                            WriteVerbose("fullName:" + info.FullName);
                            WriteObject(CardWirthScenario.IsScenarioSummary(info.FullName, ScenarioType, ContainerType));
                        }
                        catch (Exception)
                        {
                            WriteObject(false);
                        }
                    }
                }
            }
        }

        #endregion command code

    }
}
