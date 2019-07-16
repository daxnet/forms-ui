using FormsUI.Examples.DockableWindows.Models;
using FormsUI.Windows;
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

namespace FormsUI.Examples.DockableWindows
{
    public partial class EditorWindow : DockableWindow
    {
        private readonly Note note;

        public EditorWindow(IAppWindow appWindow, Note note)
            : base(appWindow, false)
        {
            InitializeComponent();
            this.note = note;
            this.txtEditor.Text = note.Content;
        }

        private void TxtEditor_TextChanged(object sender, EventArgs e)
        {
            note.Content = txtEditor.Text;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }
    }
}
