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
    public partial class EditorWindow : DocumentWindow
    {
        public EditorWindow(IAppWindow appWindow, Note note)
            : base(appWindow)
        {
            InitializeComponent();
            this.Note = note;
            this.txtEditor.Text = note.Content;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            txtEditor.SelectionStart = 0;
            txtEditor.SelectionLength = 0;
            txtEditor.Focus();
        }

        public Note Note { get; }

        private void TxtEditor_TextChanged(object sender, EventArgs e)
        {
            Note.Content = txtEditor.Text;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }
    }
}
