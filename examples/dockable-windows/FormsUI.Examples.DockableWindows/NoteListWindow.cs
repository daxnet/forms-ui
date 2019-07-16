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
    public partial class NoteListWindow : DockableWindow
    {
        public NoteListWindow(IAppWindow appWindow)
            : base(appWindow)
        {
            InitializeComponent();
        }

        protected override void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs e)
        {
            var noteEditorModel = e.Model as NoteEditorModel;
            if (noteEditorModel.Count > 0)
            {
                lst.Items.Clear();
                foreach (var note in noteEditorModel.Notes)
                {
                    lst.Items.Add(note.Title);
                }
            }
        }

        protected override void OnWorkspaceClosed(object sender, EventArgs e)
        {
            lst.Items.Clear();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }
    }
}
