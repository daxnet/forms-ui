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

namespace FormsUI.Windows
{
    public abstract partial class AppWindow<TWorkspaceModel> : Form, IAppWindow<TWorkspaceModel>
        where TWorkspaceModel : IWorkspaceModel
    {

        #region Public Constructors

        public AppWindow()
        {
            InitializeComponent();

            #region Initialize Workspace
            Workspace = CreateWorkspace();
            Workspace.WorkspaceChanged += OnWorkspaceChanged;
            Workspace.WorkspaceClosed += OnWorkspaceClosed;
            Workspace.WorkspaceCreated += OnWorkspaceCreated;
            Workspace.WorkspaceOpened += OnWorkspaceOpened;
            Workspace.WorkspaceSaved += OnWorkspaceSaved;
            #endregion
        }

        #endregion Public Constructors

        #region Public Properties

        public Workspace<TWorkspaceModel> Workspace { get; }

        #endregion Public Properties

        #region Protected Methods

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Workspace.WorkspaceChanged -= OnWorkspaceChanged;
            Workspace.WorkspaceClosed -= OnWorkspaceClosed;
            Workspace.WorkspaceCreated -= OnWorkspaceCreated;
            Workspace.WorkspaceOpened -= OnWorkspaceOpened;
            Workspace.WorkspaceSaved -= OnWorkspaceSaved;
            base.OnFormClosed(e);
        }

        protected abstract Workspace<TWorkspaceModel> CreateWorkspace();

        protected virtual void OnWorkspaceChanged(object sender, EventArgs e) { }

        protected virtual void OnWorkspaceClosed(object sender, EventArgs e) { }

        protected virtual void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs<TWorkspaceModel> e) { }

        protected virtual void OnWorkspaceOpened(object sender, WorkspaceOpenedEventArgs<TWorkspaceModel> e) { }

        protected virtual void OnWorkspaceSaved(object sender, WorkspaceSavedEventArgs<TWorkspaceModel> e) { }

        #endregion Protected Methods

    }
}
