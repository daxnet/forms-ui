using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsUI.Windows
{
    public partial class ToolWindow : DockableWindow
    {
        public ToolWindow(IAppWindow appWindow)
            : base(appWindow)
        {
            InitializeComponent();
        }

        protected ToolWindow()
        {
            InitializeComponent();
        }

        protected virtual WindowTools WindowTools => null;

        protected override void OnDockWindowHidden(EventArgs e)
        {
            base.OnDockWindowHidden(e);
            if (!(WindowTools?.IsEmpty ?? true))
            {
                AppWindow.RevertMerge(WindowTools);
            }
        }

        protected override void OnDockWindowShown(EventArgs e)
        {
            base.OnDockWindowShown(e);
            if (!(WindowTools?.IsEmpty ?? true))
            {
                AppWindow.MergeTools(WindowTools);
            }
        }
    }
}
