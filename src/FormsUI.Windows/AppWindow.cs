﻿using FormsUI.Workspaces;
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
    public partial class AppWindow : Form, IAppWindow
    {

        #region Public Constructors

        public AppWindow()
        {
            InitializeComponent();

            IsMdiContainer = true;

            #region Initialize Workspace
            Workspace = CreateWorkspace();
            if (Workspace != null)
            {
                Workspace.WorkspaceChanged += OnWorkspaceChanged;
                Workspace.WorkspaceClosed += OnWorkspaceClosed;
                Workspace.WorkspaceCreated += OnWorkspaceCreated;
                Workspace.WorkspaceOpened += OnWorkspaceOpened;
                Workspace.WorkspaceSaved += OnWorkspaceSaved;
            }
            #endregion

            #region Initialize Window Manager
            WindowManager = new DockableWindowManager(this);
            WindowManager.WindowHidden += OnWindowHidden;
            WindowManager.WindowShown += OnWindowShown;
            #endregion
        }

        protected virtual void OnWindowShown(object sender, DockableWindowShownEventArgs e) { }

        protected virtual void OnWindowHidden(object sender, DockableWindowHiddenEventArgs e) { }

        #endregion Public Constructors

        #region Public Properties

        public Workspace Workspace { get; }

        public DockableWindowManager WindowManager { get; }

        #endregion Public Properties

        #region Protected Methods

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (Workspace != null)
            {
                Workspace.WorkspaceChanged -= OnWorkspaceChanged;
                Workspace.WorkspaceClosed -= OnWorkspaceClosed;
                Workspace.WorkspaceCreated -= OnWorkspaceCreated;
                Workspace.WorkspaceOpened -= OnWorkspaceOpened;
                Workspace.WorkspaceSaved -= OnWorkspaceSaved;
            }

            WindowManager.WindowHidden -= OnWindowHidden;
            WindowManager.WindowShown -= OnWindowShown;
            WindowManager.Dispose();

            base.OnFormClosed(e);
        }

        protected virtual Workspace CreateWorkspace() => null;

        public virtual DockPanel DockArea { get; } = null;

        protected virtual void OnWorkspaceChanged(object sender, EventArgs e) { }

        protected virtual void OnWorkspaceClosed(object sender, EventArgs e) { }

        protected virtual void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs e) { }

        protected virtual void OnWorkspaceOpened(object sender, WorkspaceOpenedEventArgs e) { }

        protected virtual void OnWorkspaceSaved(object sender, WorkspaceSavedEventArgs e) { }

        protected void RegisterToolWindow<TToolWindow>(IEnumerable<ToolStripItem> toolStripItems, DockState dockState, bool show = true, Action<TToolWindow> registeredCallback = null)
            where TToolWindow : DockableWindow
        {
            var toolWindow = WindowManager.GetWindows<TToolWindow>().FirstOrDefault();
            if (toolWindow == null)
            {
                toolWindow = WindowManager.CreateWindow<TToolWindow>();
                if (DockArea != null)
                {
                    if (dockState != DockState.Hidden &&
                        dockState != DockState.Unknown && dockState != DockState.Hidden)
                    {
                        toolWindow.Show(DockArea, dockState);
                        if (!show)
                        {
                            toolWindow.Hide();
                        }
                    }
                }

                foreach (var toolStripItem in toolStripItems)
                {
                    toolStripItem.Tag = typeof(TToolWindow);
                    toolStripItem.Click += ToggleToolWindowAction;
                }

                registeredCallback?.Invoke(toolWindow);
            }
        }

        #endregion Protected Methods

        private void ToggleToolWindowAction(object sender, EventArgs e)
        {
            if (sender is ToolStripItem tsi &&
                tsi.Tag is Type windowType)
            {
                var windows = WindowManager.GetWindows(windowType);
                foreach (var window in windows)
                {
                    if (window.DockState == DockState.Hidden)
                    {
                        window.Show();
                    }
                    else
                    {
                        window.Hide();
                    }
                }
            }
        }
    }
}