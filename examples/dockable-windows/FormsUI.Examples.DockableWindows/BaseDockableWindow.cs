using FormsUI.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsUI.Examples.DockableWindows
{
    public partial class BaseDockableWindow : DockableWindow<TextEditorModel>
    {
        public BaseDockableWindow(IAppWindow<TextEditorModel> appWindow, bool hideOnClose = true)
            : base(appWindow, hideOnClose)
        {
            InitializeComponent();
        }

        protected BaseDockableWindow()
        {
            InitializeComponent();
        }
    }
}
