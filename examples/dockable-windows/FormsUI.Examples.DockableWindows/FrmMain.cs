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
using WeifenLuo.WinFormsUI.Docking;

namespace FormsUI.Examples.DockableWindows
{
    public partial class FrmMain : AppWindow
    {
        public FrmMain()
        {
            InitializeComponent();

            RegisterToolWindow<NoteListWindow>(new ToolStripItem[] { mnuNoteList, tbtnNodeList }, DockState.DockLeft, false);

            ResetMenuStates();
        }

        private void ResetMenuStates()
        {
            mnuNew.Enabled = true;
            mnuOpen.Enabled = true;
            mnuSave.Enabled = false;
            mnuSaveAs.Enabled = false;
            mnuClose.Enabled = false;
        }

        protected override Workspace CreateWorkspace() => new NoteEditorWorkspace();

        protected override DockPanel DockArea => dockPanel;

        protected override void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs e)
        {
            WindowManager.GetWindows<NoteListWindow>().First().Show();

            var editorModel = e.Model as NoteEditorModel;

            var editorWindow = WindowManager.CreateWindow<EditorWindow>(editorModel.Notes.First(n => n.Title == "Note1"));
            editorWindow.Show(DockArea, DockState.Document);

            mnuSave.Enabled = true;
            mnuSaveAs.Enabled = true;
            mnuClose.Enabled = true;
        }

        protected override void OnWorkspaceChanged(object sender, EventArgs e)
        {
            mnuSave.Enabled = true;
            mnuSaveAs.Enabled = true;
        }

        protected override void OnWorkspaceSaved(object sender, WorkspaceSavedEventArgs e)
        {
            mnuSave.Enabled = false;
            mnuSaveAs.Enabled = false;
        }

        protected override void OnWorkspaceClosed(object sender, EventArgs e)
        {
            WindowManager.CloseWindows<EditorWindow>();
            ResetMenuStates();
        }

        private void Action_New(object sender, EventArgs e)
        {
            using (new LengthyOperation(this))
            {
                Workspace.New(model =>
                {
                    var noteEditorModel = model as NoteEditorModel;
                    noteEditorModel.Add(new Note { Title = "Note1", Content = string.Empty });
                    return (true, noteEditorModel);
                });
            }
        }

        private void Action_Open(object sender, EventArgs e)
        {
            using (new LengthyOperation(this))
            {
                Workspace.Open();
            }
        }

        private void Action_Save(object sender, EventArgs e)
        {
            using (new LengthyOperation(this))
            {
                Workspace.Save();
            }
        }

        private void Action_SaveAs(object sender, EventArgs e)
        {
            using (new LengthyOperation(this))
            {
                Workspace.Save(true);
            }
        }

        private void Action_Close(object sender, EventArgs e)
        {
            using (new LengthyOperation(this))
            {
                Workspace.Close();
            }
        }
    }
}
