using System;
using System.Windows.Controls;
using Microsoft.Win32;
using ExecutePSScriptAction;

namespace ExecutePSScript
{
    public partial class ExecutePSScriptSettings : UserControl
    {
        private Guid networkId;
        private ExecutePSScriptAction executePSScriptAction;

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

        private void BtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            executePSScriptAction.SaveSetting(networkId, txtScriptPath.Text);
        }

        private void BtnBrowse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var fd = new OpenFileDialog()
            {
                AddExtension = false,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "*.*|*.*",
                Multiselect = false,
                ShowReadOnly = false,
                Title = DefaultResources.FileDialog_Title
            };
            fd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fd.FileName))
            {
                txtScriptPath.Text = fd.FileName;
            }
        }
    }
}
