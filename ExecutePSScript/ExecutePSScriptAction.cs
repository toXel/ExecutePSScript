using System;
using System.IO;
using System.Windows.Controls;
using ProxySwitcher.Common;
using System.Management.Automation;

namespace ExecutePSScript
{
    [SwitcherActionAddIn]
    public class ExecutePSScriptAction : SwitcherActionBase
    {
        public override string Name
        {
            get { return "Execute PowerShell script"; }
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
            string scriptPath = Settings[networkId + "_ScriptPath"];
            string scriptContent;

            // Cancel if script path is empty or script doesn't exist
            if (string.IsNullOrWhiteSpace(scriptPath) || !File.Exists(scriptPath))
            {
                if (HostApplication != null)
                {
                    HostApplication.SetStatusText(this, "Script is empty or doesn't exist", true);
                }

                return;
            }

            // Read script content
            using (var sr = new StreamReader(scriptPath))
            {
                scriptContent = sr.ReadToEnd();
            }

            // Invoke PowerShell script
            using (var ps = PowerShell.Create())
            {
                ps.AddScript(scriptContent);
                ps.Invoke();
            }
        }

        public override UserControl GetWindowControl(Guid networkId, string networkName)
        {
            string scriptPath = Settings[networkId + "_ScriptPath"];

            // When no script is selected
            if (string.IsNullOrWhiteSpace(scriptPath))
            {
                return new ExecutePSScriptSettings(this, networkId);
            }

            return new ExecutePSScriptSettings(this, networkId, scriptPath);
        }

        internal void SaveSetting(Guid networkId, string scriptPath)
        {
            Settings[networkId + "_ScriptPath"] = scriptPath;

            OnSettingsChanged();

            if (HostApplication != null)
            {
                HostApplication.SetStatusText(this, "Saved");
            }
        }
    }
}