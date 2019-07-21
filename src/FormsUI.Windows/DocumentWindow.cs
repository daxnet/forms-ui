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
    public partial class DocumentWindow : DockableWindow
    {
        public DocumentWindow(IAppWindow appWindow)
            : base(appWindow, false)
        {
            InitializeComponent();
        }

        protected DocumentWindow()
        {
            InitializeComponent();
        }
    }
}
