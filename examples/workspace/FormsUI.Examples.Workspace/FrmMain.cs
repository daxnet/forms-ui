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

namespace FormsUI.Examples.Workspace
{
    public partial class FrmMain : Form
    {
        private readonly TextEditorWorkspace workspace = new TextEditorWorkspace();

        public FrmMain()
        {
            InitializeComponent();

            // Subscribe workspace events.
            workspace.WorkspaceCreated += Workspace_WorkspaceCreated;
            workspace.WorkspaceOpened += Workspace_WorkspaceOpened;
            workspace.WorkspaceChanged += Workspace_WorkspaceChanged;
            workspace.WorkspaceSaved += Workspace_WorkspaceSaved;
            workspace.WorkspaceClosed += Workspace_WorkspaceClosed;

            workspace.Close();
        }

        private void Action_New(object sender, EventArgs e)
        {
            using (new LengthyOperation(this))
            {
                workspace.New();
            }
        }

        private void Action_Open(object sender, EventArgs e)
        {
            using (new LengthyOperation(this))
            {
                workspace.Open();
            }
        }

        private void Action_Save(object sender, EventArgs e)
        {
            using (new LengthyOperation(this))
            {
                workspace.Save();
            }
        }

        private void Action_SaveAs(object sender, EventArgs e)
        {
            using (new LengthyOperation(this))
            {
                workspace.Save(true);
            }
        }

        private void Action_Close(object sender, EventArgs e)
        {
            using (new LengthyOperation(this))
            {
                workspace.Close();
            }
        }

        private void Workspace_WorkspaceClosed(object sender, EventArgs e)
        {
            txtMain.Enabled = false;
            txtMain.Text = string.Empty;

            mnuClose.Enabled = false;
            mnuSave.Enabled = false;
            mnuSaveAs.Enabled = false;
            tbtnSave.Enabled = false;
            statusLabel.Text = string.Empty;
        }

        private void Workspace_WorkspaceSaved(object sender, WorkspaceSavedEventArgs e)
        {
            mnuSave.Enabled = false;
            tbtnSave.Enabled = false;
            statusLabel.Text = e.FileName;
        }

        private void Workspace_WorkspaceChanged(object sender, EventArgs e)
        {
            mnuSave.Enabled = true;
            tbtnSave.Enabled = true;
        }

        private void Workspace_WorkspaceOpened(object sender, WorkspaceOpenedEventArgs e)
        {
            txtMain.Enabled = true;
            txtMain.Text = (e.Model as TextEditorModel).Text;

            mnuSaveAs.Enabled = true;
            mnuClose.Enabled = true;
            statusLabel.Text = e.FileName;
        }

        private void Workspace_WorkspaceCreated(object sender, WorkspaceCreatedEventArgs e)
        {
            txtMain.Enabled = true;
            txtMain.Text = string.Empty;
            txtMain.Focus();

            mnuClose.Enabled = true;
            mnuSave.Enabled = true;
            tbtnSave.Enabled = true;
            mnuSaveAs.Enabled = true;
            statusLabel.Text = string.Empty;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = !this.workspace.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Unsubscribe the workspace events.
            workspace.WorkspaceCreated -= Workspace_WorkspaceCreated;
            workspace.WorkspaceOpened -= Workspace_WorkspaceOpened;
            workspace.WorkspaceChanged -= Workspace_WorkspaceChanged;
            workspace.WorkspaceSaved -= Workspace_WorkspaceSaved;
            workspace.WorkspaceClosed -= Workspace_WorkspaceClosed;
            base.OnClosed(e);
        }

        private void txtMain_TextChanged(object sender, EventArgs e)
        {
            (workspace.Model as TextEditorModel).Text = txtMain.Text;
        }
    }
}
