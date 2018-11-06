
using System;

namespace FormsUI.Workspaces
{
    /// <summary>
    /// Represents the version of the decorated workspace model.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class WorkspaceModelVersionAttribute : Attribute
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceModelVersionAttribute"/> class.
        /// </summary>
        /// <param name="major">The major version of the workspace model.</param>
        /// <param name="minor">The minor version of the workspace model.</param>
        public WorkspaceModelVersionAttribute(int major, int minor)
        {
            Version = new WorkspaceModelVersion(major, minor);
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the version of the workspace model.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public WorkspaceModelVersion Version { get; }

        #endregion Public Properties
    }
}