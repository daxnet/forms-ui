using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsUI.Windows
{
    /// <summary>
    /// Represents the dockable window manager that manages the lifetime of the dockable windows in the app.
    /// </summary>
    /// <typeparam name="TWorkspaceModel">The type of the workspace model.</typeparam>
    /// <seealso cref="FormsUI.ComponentManager{DockableWindow{TWorkspaceModel}}" />
    public sealed class DockableWindowManager<TWorkspaceModel> : ComponentManager<DockableWindow<TWorkspaceModel>>
        where TWorkspaceModel : IWorkspaceModel
    {

        #region Private Fields

        private readonly IAppWindow<TWorkspaceModel> appWindow;
        private bool disposed;

        #endregion Private Fields

        #region Public Events

        public event EventHandler<DockableWindowHiddenEventArgs<TWorkspaceModel>> WindowHidden;

        public event EventHandler<DockableWindowShownEventArgs<TWorkspaceModel>> WindowShown;

        #endregion Public Events

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DockableWindowManager{TWorkspaceModel}"/> class.
        /// </summary>
        /// <param name="appWindow">The application window.</param>
        public DockableWindowManager(IAppWindow<TWorkspaceModel> appWindow)
        {
            this.appWindow = appWindow;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Closes the windows by the specified type.
        /// </summary>
        /// <typeparam name="TDockableWindow">The type of the dockable window to be closed.</typeparam>
        public void CloseWindows<TDockableWindow>()
            where TDockableWindow : DockableWindow<TWorkspaceModel>
        {
            var windowSelector = from window in components
                                 where window is TDockableWindow
                                 select window as TDockableWindow;

            foreach (var dockableWindow in windowSelector)
            {
                dockableWindow.Close();
            }
        }

        public TDockableWindow CreateWindow<TDockableWindow>(params object[] args)
                    where TDockableWindow : DockableWindow<TWorkspaceModel>
        {
            var parms = new List<object> { appWindow };
            if (args != null && args.Length > 0)
            {
                parms.AddRange(args);
            }

            var dockableWindow = (TDockableWindow)Activator.CreateInstance(typeof(TDockableWindow), parms.ToArray());
            dockableWindow.DockWindowShown += DockableWindow_DockWindowShown;
            dockableWindow.DockWindowHidden += DockableWindow_DockWindowHidden;
            dockableWindow.FormClosed += DockableWindow_FormClosed;
            this.Add(dockableWindow);

            return dockableWindow;
        }
        public IEnumerable<TDockableWindow> GetWindows<TDockableWindow>()
            where TDockableWindow : DockableWindow<TWorkspaceModel> 
            => GetWindows(typeof(TDockableWindow)).Select(w => w as TDockableWindow);

        public IEnumerable<DockableWindow<TWorkspaceModel>> GetWindows(Type dockableWindowType) 
            => from window in components
               where window.GetType() == dockableWindowType
               select window;

        public IEnumerable<TDockableWindow> GetWindows<TDockableWindow>(Func<TDockableWindow, bool> predicate)
            where TDockableWindow : DockableWindow<TWorkspaceModel>
            => GetWindows<TDockableWindow>().Where(predicate);

        public IEnumerable<DockableWindow<TWorkspaceModel>> GetWindows(Type dockableWindowType, Func<DockableWindow<TWorkspaceModel>, bool> predicate)
            => GetWindows(dockableWindowType).Where(predicate);

        #endregion Public Methods

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                foreach (var window in components)
                {
                    window.DockWindowHidden -= DockableWindow_DockWindowHidden;
                    window.DockWindowShown -= DockableWindow_DockWindowShown;
                    window.FormClosed -= DockableWindow_FormClosed;
                    if (window.HideOnClose)
                    {
                        // window.Cleanup();
                    }
                }

                base.Dispose(disposing);
                disposed = true;
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void DockableWindow_DockWindowHidden(object sender, EventArgs e)
        {
            WindowHidden?.Invoke(this, new DockableWindowHiddenEventArgs<TWorkspaceModel>((DockableWindow<TWorkspaceModel>)sender));
        }

        private void DockableWindow_DockWindowShown(object sender, EventArgs e)
        {
            WindowShown?.Invoke(this, new DockableWindowShownEventArgs<TWorkspaceModel>((DockableWindow<TWorkspaceModel>)sender));
        }

        private void DockableWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            var dockableWindow = (DockableWindow<TWorkspaceModel>)sender;
            if (!dockableWindow?.HideOnClose ?? false)
            {
                components.Remove(dockableWindow);
            }
        }

        #endregion Private Methods
    }
}
