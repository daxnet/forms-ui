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

namespace FormsUI.Windows
{
    public partial class DockableWindow : DockContent
    {
        #region Public Events

        public event EventHandler DockWindowHidden;

        public event EventHandler DockWindowShown;

        #endregion Public Events

        #region Protected Constructors

        protected DockableWindow(IAppWindow appWindow, bool hideOnClose = true)
            : this()
        {
            HideOnClose = hideOnClose;
            AppWindow = appWindow;

            if (AppWindow.Workspace != null)
            {
                AppWindow.Workspace.WorkspaceChanged += OnWorkspaceChanged;
                AppWindow.Workspace.WorkspaceClosed += OnWorkspaceClosed;
                AppWindow.Workspace.WorkspaceCreated += OnWorkspaceCreated;
                AppWindow.Workspace.WorkspaceOpened += OnWorkspaceOpened;
                AppWindow.Workspace.WorkspaceSaved += OnWorkspaceSaved;
                AppWindow.Workspace.WorkspaceStateChanged += OnWorkspaceStateChanged;
            }
        }

        #endregion Protected Constructors

        #region Private Constructors

        protected DockableWindow()
        {
            InitializeComponent();
        }

        #endregion Private Constructors

        #region Protected Properties

        protected IAppWindow AppWindow { get; }

        #endregion Protected Properties

        #region Public Methods

        public override string ToString() => Text;

        #endregion Public Methods

        #region Protected Methods

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (!HideOnClose)
            {
                Cleanup();
            }

            base.OnFormClosed(e);
        }

        protected override void OnDockStateChanged(EventArgs e)
        {
            switch (DockState)
            {
                case DockState.Hidden:
                    this.OnDockWindowHidden(e);
                    break;
                case DockState.Unknown:
                    break;
                default:
                    this.OnDockWindowShown(e);
                    break;
            }
        }

        protected virtual void OnDockWindowHidden(EventArgs e)
        {
            DockWindowHidden?.Invoke(this, e);
        }

        protected virtual void OnDockWindowShown(EventArgs e)
        {
            DockWindowShown?.Invoke(this, e);
        }

        internal protected virtual void Cleanup()
        {
            if (AppWindow.Workspace != null)
            {
                AppWindow.Workspace.WorkspaceChanged -= OnWorkspaceChanged;
                AppWindow.Workspace.WorkspaceClosed -= OnWorkspaceClosed;
                AppWindow.Workspace.WorkspaceCreated -= OnWorkspaceCreated;
                AppWindow.Workspace.WorkspaceOpened -= OnWorkspaceOpened;
                AppWindow.Workspace.WorkspaceSaved -= OnWorkspaceSaved;
                AppWindow.Workspace.WorkspaceStateChanged -= OnWorkspaceStateChanged;
            }
        }

        protected virtual void OnWorkspaceChanged(object sender, EventArgs e) { }

        protected virtual void OnWorkspaceClosed(object sender, EventArgs e) { }

        protected virtual void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs e) { }

        protected virtual void OnWorkspaceOpened(object sender, WorkspaceOpenedEventArgs e) { }

        protected virtual void OnWorkspaceSaved(object sender, WorkspaceSavedEventArgs e) { }

        protected virtual void OnWorkspaceStateChanged(object sender, WorkspaceStateChangedEventArgs e) { }

        #endregion Protected Methods
    }
}
