using System;
using System.IO;
using System.Windows.Controls;
using ProxySwitcher.Common;
using System.Management.Automation;
using ExecutePSScriptAction;

namespace ExecutePSScript
{
    [SwitcherActionAddIn]
    public class ExecutePSScriptAction : SwitcherActionBase
    {

        private PowerShell ps;

        public override string Name
        {
            get { return DefaultResources.ExecutePSScript_Name ; }
        }

        public override string Description
        {
            // currently not used
            get { return string.Empty; }
        }

        public override string Group
        {
            get { return "Script"; }
        }

        public override Stream IconResourceStream
        {
            get { return null; }
        }

        public override Guid Id
        {
            get { return new Guid("5010844E-AFB1-4048-8EAE-13EB0962B863"); }
        }

        public override void Activate(Guid networkId, string networkName)
        {
            string scriptPath = Settings[networkId.ToString() + "_ScriptPath"];
            string scriptContent;

            // Cancel if script path is empty or script doesn't exist
            if (string.IsNullOrWhiteSpace(scriptPath) || !File.Exists(scriptPath))
            {
                HostApplication?.SetStatusText(this, "Script is empty or doesn't exist", true);

                return;
            }

            // Read script content
            using (var sr = new StreamReader(scriptPath))
            {
                scriptContent = sr.ReadToEnd();
            }

            // Invoke PowerShell script
            ps = PowerShell.Create();
            ps.AddScript(scriptContent);
            ps.BeginInvoke<PSObject>(null, null, AsyncInvoke, null);
        }

        void AsyncInvoke(IAsyncResult ar)
        {
            ps.EndInvoke(ar);
        }

        public override UserControl GetWindowControl(Guid networkId, string networkName)
        {
            string scriptPath = Settings[networkId.ToString() + "_ScriptPath"];

            // When no script is selected
            if (string.IsNullOrWhiteSpace(scriptPath))
            {
                return new ExecutePSScriptSettings(this, networkId);
            }

            return new ExecutePSScriptSettings(this, networkId, scriptPath);
        }

        internal void SaveSetting(Guid networkId, string scriptPath)
        {
            Settings[networkId.ToString() + "_ScriptPath"] = scriptPath;

            OnSettingsChanged();

            HostApplication?.SetStatusText(this, DefaultResources.Status_Saved);
        }
    }
}