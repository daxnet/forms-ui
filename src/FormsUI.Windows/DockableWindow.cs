using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace FormsUI.Windows
{
    public partial class DockableWindow<TWorkspaceModel> : DockContent
        where TWorkspaceModel : IWorkspaceModel
    {
        protected DockableWindow(IAppWindow<TWorkspaceModel> appWindow, bool hideOnClose = true)
        {
            HideOnClose = hideOnClose;
            AppWindow = appWindow;
        }

        protected DockableWindow()
        {
            InitializeComponent();
        }

        public event EventHandler DockWindowHidden;

        public event EventHandler DockWindowShown;

        protected IAppWindow<TWorkspaceModel> AppWindow { get; }

        public override string ToString() => Text;

        protected override void OnDockStateChanged(EventArgs e)
        {
            
        }

        protected virtual void OnDockWindowHidden(EventArgs e)
        {
            DockWindowHidden?.Invoke(this, e);
        }

        protected virtual void OnDockWindowShown(EventArgs e)
        {
            DockWindowShown?.Invoke(this, e);
        }
    }
}
