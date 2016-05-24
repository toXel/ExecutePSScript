using System;
using System.Windows.Controls;

namespace ClassLibrary1
{
    public partial class ExecutePSScriptSettings : UserControl
    {
        private Guid networkId;
        private ExecutePSScriptAction myAction;

        public ExecutePSScriptSettings()
        {
            InitializeComponent();
        }

        public ExecutePSScriptSettings(Guid networkId, ExecutePSScriptAction myAction)
            : this()
        {
            this.networkId = networkId;
            this.myAction = myAction;
        }
    }
}
