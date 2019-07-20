using FormsUI.Examples.DockableWindows.Models;
using FormsUI.Examples.DockableWindows.Properties;
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
        #region Private Fields

        private ToolAction closeTool;
        private ToolAction newTool;
        private ToolAction openTool;
        private ToolAction saveAsTool;
        private ToolAction saveTool;

        #endregion Private Fields

        #region Public Constructors

        public FrmMain()
        {
            InitializeComponent();
            RegisterTools();
            RegisterToolWindows();
            ResetMenuStates();
        }

        #endregion Public Constructors

        #region Public Properties

        public override DockPanel DockArea => dockPanel;

        #endregion Public Properties

        #region Protected Methods

        protected override Workspace CreateWorkspace() => new NoteEditorWorkspace();

        protected override void OnWorkspaceClosed(object sender, EventArgs e)
        {
            WindowManager.CloseWindows<EditorWindow>();
        }

        protected override void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs e)
        {
            WindowManager.GetFirstWindow<NoteListWindow>()?.Show();
        }

        protected override void OnWorkspaceStateChanged(object sender, WorkspaceStateChangedEventArgs e)
        {
            switch (e.State)
            {
                case WorkspaceState.Inactive:
                    ResetMenuStates();
                    break;

                case WorkspaceState.Clean:
                    saveTool.Enabled = false;
                    break;

                case WorkspaceState.Modified:
                    saveTool.Enabled = true;
                    break;

                case WorkspaceState.Opened:
                case WorkspaceState.Created:
                    closeTool.Enabled = true;
                    break;
            }

            base.OnWorkspaceStateChanged(sender, e);
        }

        #endregion Protected Methods

        #region Private Methods

        private void Action_Close(ToolAction toolAction)
        {
            Workspace.Close();
        }

        private void Action_New(ToolAction toolAction)
        {
            Workspace.New(model =>
            {
                var noteEditorModel = model as NoteEditorModel;
                noteEditorModel.Add(new Note { Title = "Note1", Content = string.Empty });
                return (true, noteEditorModel);
            });
        }

        private void Action_Open(ToolAction toolAction)
        {
            Workspace.Open();
        }

        private void Action_Save(ToolAction toolAction)
        {
            Workspace.Save();
        }

        private void Action_SaveAs(ToolAction toolAction)
        {
            Workspace.Save(true);
        }

        private void RegisterTools()
        {
            newTool = RegisterTool("newTool", "New", new ToolStripItem[] { mnuNew, tbtnNew },
                Action_New, tooltipText: "Creates a new workspace.", image: Resources.page_white, shortcutKeys: Keys.Control | Keys.N);

            openTool = RegisterTool("openTool", "&Open...", new ToolStripItem[] { mnuOpen, tbtnOpen },
                Action_Open, tooltipText: "Opens an existing workspace.", image: Resources.folder_page, shortcutKeys: Keys.Control | Keys.O);

            saveTool = RegisterTool("saveTool", "&Save", new ToolStripItem[] { mnuSave, tbtnSave },
                Action_Save, tooltipText: "Saves the current workspace.", image: Resources.disk, shortcutKeys: Keys.Control | Keys.S);

            saveAsTool = RegisterTool("saveAsTool", "Save &As...", new ToolStripItem[] { mnuSaveAs },
                Action_SaveAs, tooltipText: "Saves the current workspace as another file.", shortcutKeys: Keys.Control | Keys.Shift | Keys.S);

            closeTool = RegisterTool("closeTool", "Close", new ToolStripItem[] { mnuClose },
                Action_Close, tooltipText: "Closes the current workspace.", image: Resources.cross);
        }

        private void RegisterToolWindows()
        {
            RegisterToolWindow<NoteListWindow>(new ToolStripItem[] { mnuNoteList, tbtnNodeList },
                "Note List",
                Resources.application_side_boxes,
                DockState.DockLeft, true, "Show or hide the Note List window.", Keys.F12);
        }
        private void ResetMenuStates()
        {
            newTool.Enabled = true;
            openTool.Enabled = true;
            saveTool.Enabled = false;
            saveAsTool.Enabled = false;
            closeTool.Enabled = false;
        }

        #endregion Private Methods
    }
}
