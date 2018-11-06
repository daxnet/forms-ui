
using System.ComponentModel;

namespace FormsUI.Workspaces
{
    /// <summary>
    /// Represents that the implemented classes are workspace models.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface IWorkspaceModel : INotifyPropertyChanged
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the persisted version of the workspace model.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        WorkspaceModelVersion Version { get; set; }

        #endregion Public Properties
    }
}