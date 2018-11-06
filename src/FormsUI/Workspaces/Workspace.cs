
using System;
using System.Reflection;
using System.Windows.Forms;

namespace FormsUI.Workspaces
{
    /// <summary>
    /// Represents the workspace in a Windows Forms application.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class Workspace<TModel>
        where TModel : IWorkspaceModel
    {

        #region Private Fields

        private bool closed = true;
        private string workspaceFileName;

        #endregion Private Fields

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Workspace{TModel}"/> class.
        /// </summary>
        protected Workspace()
        {
        }

        #endregion Protected Constructors

        #region Public Events

        /// <summary>
        /// Occurs when the workspace has been modified.
        /// </summary>
        public event EventHandler WorkspaceChanged;

        /// <summary>
        /// Occurs when the workspace has been closed.
        /// </summary>
        public event EventHandler WorkspaceClosed;

        /// <summary>
        /// Occurs when a new workspace has been created.
        /// </summary>
        public event EventHandler<WorkspaceCreatedEventArgs<TModel>> WorkspaceCreated;

        /// <summary>
        /// Occurs when the workspace has been opened.
        /// </summary>
        public event EventHandler<WorkspaceOpenedEventArgs<TModel>> WorkspaceOpened;

        /// <summary>
        /// Occurs when the workspace has been saved.
        /// </summary>
        public event EventHandler<WorkspaceSavedEventArgs<TModel>> WorkspaceSaved;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether the current workspace has once been saved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the current workspace has been saved previously; otherwise, <c>false</c>.
        /// </value>
        public bool HasBeenSaved
        {
            get { return !string.IsNullOrEmpty(workspaceFileName); }
        }

        /// <summary>
        /// Gets a value indicating whether the current workspace has changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the current workspace has changed; otherwise, <c>false</c>.
        /// </value>
        public bool HasChanged { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the current workspace is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the current workspace is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive => !closed;
        /// <summary>
        /// Gets the model instance of the workspace.
        /// </summary>
        /// <value>
        /// The model instance of the workspace.
        /// </value>
        public TModel Model { get; private set; }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Gets the current version of the workspace model data object.
        /// </summary>
        /// <value>
        /// The current version of the workspace model.
        /// </value>
        protected WorkspaceModelVersion CurrentModelVersion
        {
            get
            {
                if (typeof(TModel).IsDefined(typeof(WorkspaceModelVersionAttribute), false))
                {
                    return typeof(TModel).GetCustomAttribute<WorkspaceModelVersionAttribute>().Version;
                }

                return WorkspaceModelVersion.One;
            }
        }

        /// <summary>
        /// Gets the description of the workspace file.
        /// </summary>
        /// <value>
        /// The workspace file description.
        /// </value>
        protected abstract string WorkspaceFileDescription { get; }

        /// <summary>
        /// Gets the file name extension of the workspace files. For example, "txt" or "csv", if
        /// the workspace is saved in .txt or .csv extensions.
        /// </summary>
        /// <value>
        /// The workspace file extension.
        /// </value>
        protected abstract string WorkspaceFileExtension { get; }

        #endregion Protected Properties

        #region Public Methods

        /// <summary>
        /// Closes the current workspace.
        /// </summary>
        /// <returns>True if the close was successful, otherwise, false.</returns>
        public bool Close()
        {
            if (HasChanged)
            {
                var dlg = MessageBox.Show("Workspace has changed, do you want to save the changes?",
                    "Confirmation",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);
                if (dlg == DialogResult.Cancel)
                {
                    return false;
                }
                else if (dlg == DialogResult.Yes)
                {
                    var savedResult = this.Save();
                    if (savedResult)
                    {
                        OnWorkspaceClosed(EventArgs.Empty);
                        HasChanged = false;
                    }

                    return savedResult;
                }
            }

            OnWorkspaceClosed(EventArgs.Empty);
            HasChanged = false;
            closed = true;
            return true;
        }

        /// <summary>
        /// Forcibly notify that the workspace has changed.
        /// </summary>
        public void ForceChange()
        {
            this.OnWorkspaceChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Creates a new workspace. If there is any active workspace, it will be closed before the new
        /// workspace is going to be created.
        /// </summary>
        /// <returns>True if the workspace has been created successfully, otherwise, false.</returns>
        public bool New()
        {
            // Creates the instance of the workspace model.
            var creatingModel = Create();

            // If the workspace model instance has been created successfully and there is no active workspace,
            // or the active workspace has been closed successfully, assign the newly created workspace instance
            // to the workspace model in the current workspace.
            if (creatingModel != null && (closed || this.Close()))
            {
                this.Model = creatingModel;

                this.Model.PropertyChanged += (ps, pe) =>
                  {
                      this.OnWorkspaceChanged(EventArgs.Empty);
                  };

                this.OnWorkspaceCreated(new WorkspaceCreatedEventArgs<TModel>(this.Model));
                this.OnWorkspaceChanged(EventArgs.Empty);

                this.closed = false;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Shows the Open File dialog and opens the selected file as a workspace.
        /// </summary>
        /// <returns>True if the open was successful, otherwise, false.</returns>
        public bool Open()
        {
            using (var openFileDialog = new OpenFileDialog
            {
                Filter = $"{WorkspaceFileDescription}(*.{WorkspaceFileExtension})|*.{WorkspaceFileExtension}",
                AddExtension = true,
                DefaultExt = WorkspaceFileExtension,
                Title = "Open Workspace"
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return Open(openFileDialog.FileName);
                }

                return false;
            }
        }
        /// <summary>
        /// Opens the workspace with the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>True if the open was successful, otherwise, false.</returns>
        public bool Open(string fileName)
        {
            if (!closed)
            {
                if (!Close())
                {
                    return false;
                }
            }

            this.Model = OpenFromFile(fileName);
            this.Model.PropertyChanged += (ps, pe) =>
            {
                this.OnWorkspaceChanged(EventArgs.Empty);
            };

            this.workspaceFileName = fileName;
            this.OnWorkspaceOpened(new WorkspaceOpenedEventArgs<TModel>(fileName, this.Model));
            this.closed = false;
            return true;
        }

        /// <summary>
        /// Saves the current workspace.
        /// </summary>
        /// <param name="saveAs">The <see cref="bool"/> value which indicates whether the SaveAs operation should be performed.</param>
        /// <returns>True if the save was successful, otherwise, false.</returns>
        public bool Save(bool saveAs = false)
        {
            if (HasBeenSaved && !saveAs)
            {
                try
                {
                    this.SaveToFile(this.Model, workspaceFileName);
                    this.OnWorkspaceSaved(new WorkspaceSavedEventArgs<TModel>(workspaceFileName, this.Model));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                using (var saveFileDialog = new SaveFileDialog
                {
                    Filter = $"{WorkspaceFileDescription}(*.{WorkspaceFileExtension})|*.{WorkspaceFileExtension}",
                    AddExtension = true,
                    DefaultExt = WorkspaceFileExtension,
                    Title = "Save Workspace"
                })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            this.SaveToFile(this.Model, saveFileDialog.FileName);
                            this.OnWorkspaceSaved(new WorkspaceSavedEventArgs<TModel>(saveFileDialog.FileName, this.Model));
                            this.workspaceFileName = saveFileDialog.FileName;
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Creates the workspace.
        /// </summary>
        /// <returns>The workspace model that was created.</returns>
        protected abstract TModel Create();

        /// <summary>
        /// Raises the <see cref="E:WorkspaceChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnWorkspaceChanged(EventArgs e)
        {
            this.WorkspaceChanged?.Invoke(this, e);
            HasChanged = true;
        }

        /// <summary>
        /// Raises the <see cref="E:WorkspaceClosed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnWorkspaceClosed(EventArgs e)
        {
            this.WorkspaceClosed?.Invoke(this, e);

            this.HasChanged = false;
            this.workspaceFileName = null;
        }

        /// <summary>
        /// Raises the <see cref="E:WorkspaceCreated" /> event.
        /// </summary>
        /// <param name="e">The <see cref="WorkspaceCreatedEventArgs{TModel}"/> instance containing the event data.</param>
        protected virtual void OnWorkspaceCreated(WorkspaceCreatedEventArgs<TModel> e)
        {
            this.WorkspaceCreated?.Invoke(this, e);
            HasChanged = true;
        }

        /// <summary>
        /// Raises the <see cref="E:WorkspaceOpened" /> event.
        /// </summary>
        /// <param name="e">The <see cref="WorkspaceOpenedEventArgs{TModel}"/> instance containing the event data.</param>
        protected virtual void OnWorkspaceOpened(WorkspaceOpenedEventArgs<TModel> e)
        {
            this.WorkspaceOpened?.Invoke(this, e);
            // changed = false;
        }

        /// <summary>
        /// Raises the <see cref="E:WorkspaceSaved" /> event.
        /// </summary>
        /// <param name="e">The <see cref="WorkspaceSavedEventArgs{TModel}"/> instance containing the event data.</param>
        protected virtual void OnWorkspaceSaved(WorkspaceSavedEventArgs<TModel> e)
        {
            this.WorkspaceSaved?.Invoke(this, e);
            HasChanged = false;
        }

        /// <summary>
        /// Opens the workspace from the given file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The workspace model that was opened from the specified file.</returns>
        protected abstract TModel OpenFromFile(string fileName);

        /// <summary>
        /// Saves the given workspace model to the file.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="fileName">Name of the file.</param>
        protected abstract void SaveToFile(TModel model, string fileName);

        #endregion Protected Methods

    }
}