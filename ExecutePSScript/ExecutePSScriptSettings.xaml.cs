using System;
using System.Windows.Controls;
using Microsoft.Win32;

namespace ExecutePSScript
{
    public partial class ExecutePSScriptSettings : UserControl
    {
        Guid networkId;
        ExecutePSScriptAction executePSScriptAction;

        public ExecutePSScriptSettings()
        {
            InitializeComponent();
        }

        public ExecutePSScriptSettings(ExecutePSScriptAction executePSScriptAction, Guid networkId) : this()
        {
            this.networkId = networkId;
            this.executePSScriptAction = executePSScriptAction;
        }

        public ExecutePSScriptSettings(ExecutePSScriptAction executePSScriptAction, Guid networkId, string scriptPath) : this()
        {
            this.networkId = networkId;
            this.executePSScriptAction = executePSScriptAction;

            txtScriptPath.Text = scriptPath;
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            executePSScriptAction.SaveSetting(networkId, txtScriptPath.Text);
        }

        private void btnBrowse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var fd = new OpenFileDialog();
            fd.AddExtension = false;
            fd.CheckFileExists = true;
            fd.CheckPathExists = true;
            fd.Filter = "*.*|*.*";
            fd.Multiselect = false;
            fd.ShowReadOnly = false;
            fd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fd.FileName))
            {
                txtScriptPath.Text = fd.FileName;
            }
        }
    }
}
