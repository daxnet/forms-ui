using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

        private readonly List<ToolStrip> toolStrips = new List<ToolStrip>();
        private readonly Dictionary<Type, ToolAction> toolWindowRegistrations = new Dictionary<Type, ToolAction>();

        #endregion Private Fields

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppWindow"/> class.
        /// </summary>
        protected AppWindow()
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

        #endregion Protected Constructors

        #region Public Properties

        public virtual DockPanel DockArea { get; } = null;

        public DockableWindowManager WindowManager { get; }

        public Workspace Workspace { get; }

        #endregion Public Properties

        #region Private Properties

        private bool HasToolStrip => toolStrips.Count > 0;
        private bool HasMainMenu => !(MainMenuStrip == null);
        private ToolActionManager ToolActionManager { get; }

        #endregion Private Properties

        #region Public Methods

        public void MergeTools(WindowTools tools)
        {
            if (tools?.IsEmpty ?? true)
            {
                return;
            }

            if (HasToolStrip && tools.MergingToolbar != null)
            {
                var targetToolStrip = GetToolStripByName(tools.MergingToolbar.TargetToolStripName);
                if (targetToolStrip == null)
                {
                    return;
                }

                ToolStripManager.Merge(tools.MergingToolbar.ToolStrip, targetToolStrip);

                tools.MergingToolbar.ToolStrip.Hide();
            }

            if (HasMainMenu && tools.MergingMenus?.Count() > 0)
            {
                foreach (var mergingMenu in tools.MergingMenus)
                {
                    var matchingTargetMenus = MainMenuStrip.Items
                        .Find(mergingMenu.TargetName, true)
                        .Where(m => m is ToolStripMenuItem)
                        .Select(m => m as ToolStripMenuItem);

                    if (matchingTargetMenus?.Count() > 0)
                    {
                        foreach(var matchingTarget in matchingTargetMenus)
                        {
                            ToolStripManager.Merge(mergingMenu.MenuStrip, matchingTarget.DropDown);
                        }
                    }
                }
            }
        }

        public void RevertMerge(WindowTools tools)
        {
            if (tools?.IsEmpty ?? true)
            {
                return;
            }

            if (tools.MergingToolbar?.NeedHide ?? false)
            {
                tools.MergingToolbar.ToolStrip.Show();
                var targetToolStrip = GetToolStripByName(tools.MergingToolbar.TargetToolStripName);
                if (targetToolStrip == null)
                {
                    return;
                }

                ToolStripManager.RevertMerge(targetToolStrip, tools.MergingToolbar.ToolStrip);
            }

            if (HasMainMenu && tools.MergingMenus?.Count() > 0)
            {
                foreach (var mergingMenu in tools.MergingMenus)
                {
                    if (mergingMenu.NeedHide)
                    {
                        var matchingTargetMenus = MainMenuStrip.Items
                        .Find(mergingMenu.TargetName, true)
                        .Where(m => m is ToolStripMenuItem)
                        .Select(m => m as ToolStripMenuItem);

                        if (matchingTargetMenus?.Count() > 0)
                        {
                            foreach (var matchingTarget in matchingTargetMenus)
                            {
                                ToolStripManager.RevertMerge(matchingTarget.DropDown, mergingMenu.MenuStrip);
                            }
                        }
                    }
                }
            }
        }

        #endregion Public Methods

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
            if (Workspace?.IsActive ?? false)
            {
                e.Cancel = !Workspace.Close();
            }
            else
            {
                e.Cancel = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RegisterToolStrips();
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

        private ToolStrip GetToolStripByName(string name)
        {
            ToolStrip targetToolStrip = null;
            if (!string.IsNullOrEmpty(name))
            {
                targetToolStrip = toolStrips.FirstOrDefault(ts => string.Equals(ts.Name, name));
            }
            else
            {
                targetToolStrip = toolStrips.FirstOrDefault();
            }

            return targetToolStrip;
        }

        private void RegisterToolStrips()
        {
            var toolStripQuery = from f in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                                 where typeof(ToolStrip).IsAssignableFrom(f.FieldType) &&
                                 !typeof(MenuStrip).IsAssignableFrom(f.FieldType) &&
                                 !typeof(StatusStrip).IsAssignableFrom(f.FieldType)
                                 select f;

            foreach (var toolStripFieldInfo in toolStripQuery)
            {
                toolStrips.Add((ToolStrip)toolStripFieldInfo.GetValue(this));
            }
        }

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
