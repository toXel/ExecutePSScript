using System;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using ProxySwitcher.Common;

namespace ClassLibrary1
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

        public override Stream IconResourceStream
        {
            get { return null; }
        }

        public override Guid Id
        {
            get { return new Guid("5010844E-AFB1-4048-8EAE-13EB0962B863"); }
        }

        public override string Group
        {
            // Actions which should shown together can be grouped by this property.
            get { return "Script"; }
        }

        public override void Activate(Guid networkId, string networkName)
        {
            string mysetting1 = Settings[networkId + "_Setting1"];

            // do something here
        }

        public override UserControl GetWindowControl(Guid networkId, string networkName)
        {
            return new ExecutePSScriptSettings(networkId, this);
        }

        internal void SaveSetting(Guid networkId, string settingValue)
        {
            this.Settings[networkId + "_Setting1"] = settingValue;

            // Call this after you have changed one or more settings
            this.OnSettingsChanged();
        }
    }
}