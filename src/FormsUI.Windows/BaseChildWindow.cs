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
    public partial class BaseChildWindow : DockContent
    {
        protected BaseChildWindow(bool hideOnClose = true)
        {
            HideOnClose = hideOnClose;
        }

        private BaseChildWindow()
        {
            InitializeComponent();
        }

        public event EventHandler DockWindowHidden;

        public event EventHandler DockWindowShown;

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
