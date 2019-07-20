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
    /// <summary>
    /// Represents the base class for Windows Forms apps.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    /// <seealso cref="FormsUI.Windows.IAppWindow" />
    public partial class AppWindow : Form, IAppWindow
    {

        #region Private Fields

        private readonly Dictionary<Type, ToolAction> toolWindowRegistrations = new Dictionary<Type, ToolAction>();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppWindow"/> class.
        /// </summary>
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
                Workspace.WorkspaceStateChanged += OnWorkspaceStateChanged;
            }
            #endregion

            #region Initialize Window Manager
            WindowManager = new DockableWindowManager(this);
            WindowManager.WindowHidden += OnWindowHidden;
            WindowManager.WindowShown += OnWindowShown;
            #endregion

            #region Initialize Tool Action Manager
            ToolActionManager = new ToolActionManager();
            #endregion
        }

        #endregion Public Constructors

        #region Public Properties

        public virtual DockPanel DockArea { get; } = null;

        public DockableWindowManager WindowManager { get; }

        private ToolActionManager ToolActionManager { get; }

        public Workspace Workspace { get; }

        #endregion Public Properties

        #region Protected Methods

        protected virtual Workspace CreateWorkspace() => null;

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (Workspace != null)
            {
                Workspace.WorkspaceChanged -= OnWorkspaceChanged;
                Workspace.WorkspaceClosed -= OnWorkspaceClosed;
                Workspace.WorkspaceCreated -= OnWorkspaceCreated;
                Workspace.WorkspaceOpened -= OnWorkspaceOpened;
                Workspace.WorkspaceSaved -= OnWorkspaceSaved;
                Workspace.WorkspaceStateChanged -= OnWorkspaceStateChanged;
            }

            WindowManager.WindowHidden -= OnWindowHidden;
            WindowManager.WindowShown -= OnWindowShown;
            WindowManager.Dispose();

            ToolActionManager.Dispose();

            toolWindowRegistrations.Clear();

            base.OnFormClosed(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (Workspace.IsActive)
            {
                e.Cancel = !Workspace.Close();
            }
            else
            {
                e.Cancel = false;
            }
        }

        protected virtual void OnWindowHidden(object sender, DockableWindowHiddenEventArgs e)
        {
            ToggleToolWindowToolStripCheckedState(e.DockableWindow.GetType(), false);
        }

        protected virtual void OnWindowShown(object sender, DockableWindowShownEventArgs e)
        {
            ToggleToolWindowToolStripCheckedState(e.DockableWindow.GetType(), true);
        }

        protected virtual void OnWorkspaceChanged(object sender, EventArgs e) { }

        protected virtual void OnWorkspaceClosed(object sender, EventArgs e) { }

        protected virtual void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs e) { }

        protected virtual void OnWorkspaceOpened(object sender, WorkspaceOpenedEventArgs e) { }

        protected virtual void OnWorkspaceSaved(object sender, WorkspaceSavedEventArgs e) { }

        protected virtual void OnWorkspaceStateChanged(object sender, WorkspaceStateChangedEventArgs e) { }

        protected bool RegisterTool(ToolAction toolAction)
        {
            if (!ToolActionManager.Contains(toolAction))
            {
                ToolActionManager.Add(toolAction);
                return true;
            }

            return false;
        }

        protected ToolAction RegisterTool(string id, string text,
            IEnumerable<ToolStripItem> associatedToolStrips,
            Action<ToolAction> clickAction = null,
            bool enabled = true,
            bool visible = true,
            string tooltipText = null,
            Image image = null,
            object tag = null,
            Keys? shortcutKeys = null)
        {
            var toolAction = new ToolAction(id, this, text, associatedToolStrips, clickAction, enabled, visible, tooltipText, image, tag, shortcutKeys);
            if (RegisterTool(toolAction))
            {
                return toolAction;
            }

            throw new InvalidOperationException($"Tool Action {id} already exists.");
        }

        protected void RegisterToolWindow<TToolWindow>(IEnumerable<ToolStripItem> toolStripItems,
            string text,
            Image toolImage,
            DockState dockState,
            bool windowShow = true,
            string toolTipText = null,
            Keys? shortcutKeys = null,
            Action<TToolWindow> registeredCallback = null)
            where TToolWindow : DockableWindow
        {
            RegisterToolWindow(new ToolAction($"ToolWindowAction-{Guid.NewGuid()}", this, text, toolStripItems, tooltipText: toolTipText, image: toolImage, shortcutKeys: shortcutKeys),
                dockState,
                windowShow,
                registeredCallback);
        }

        protected void RegisterToolWindow<TToolWindow>(ToolAction toolAction, DockState dockState, bool show = true, Action<TToolWindow> registeredCallback = null)
            where TToolWindow : DockableWindow
        {
            var toolWindow = WindowManager.GetWindows<TToolWindow>().FirstOrDefault();
            if (toolWindow == null && !this.toolWindowRegistrations.ContainsKey(typeof(TToolWindow)))
            {
                try
                {
                    this.toolWindowRegistrations.Add(typeof(TToolWindow), toolAction);
                    ToolActionManager.Add(toolAction);
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

                    toolAction.Tag = typeof(TToolWindow);
                    toolAction.ClickAction = ToggleToolWindowAction;

                    registeredCallback?.Invoke(toolWindow);
                }
                catch
                {
                    if (this.toolWindowRegistrations.ContainsKey(typeof(TToolWindow)))
                    {
                        this.toolWindowRegistrations.Remove(typeof(TToolWindow));
                    }
                }
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void ToggleToolWindowAction(ToolAction toolAction)
        {
            using (new LengthyOperation(this))
            {
                if (toolAction.Tag is Type windowType)
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

        private void ToggleToolWindowToolStripCheckedState(Type windowType, bool @checked)
        {
            if (toolWindowRegistrations.ContainsKey(windowType))
            {
                var toolAction = toolWindowRegistrations[windowType];
                toolAction.Checked = @checked;
            }
        }

        #endregion Private Methods

    }
}
