using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    /// <summary>
    /// Represents different states of a workspace.
    /// </summary>
    [Flags]
    public enum WorkspaceState
    {
        /// <summary>
        /// Indicates that the workspace is inactive, usually when the application has just started without a
        /// workspace opened, or a workspace has been closed.
        /// </summary>
        Inactive = 0B0000_0000,

        /// <summary>
        /// Indicates that the workspace is active, which means it can either be in the Clean state or in the
        /// Modified state.
        /// </summary>
        Active = 0B0000_0001,

        /// <summary>
        /// Indicates that the workspace is clean, which means the user has saved the changes and the workspace
        /// model is consistent with the model saved on the disk.
        /// </summary>
        Clean = 0B0000_0011,

        /// <summary>
        /// Indicates that the workspace has been modified.
        /// </summary>
        Modified = 0B0000_0101,

        /// <summary>
        /// Indicates that the workspace has just been created.
        /// </summary>
        Created = 0B0000_1000,

        /// <summary>
        /// Indicates that the workspace was just opened.
        /// </summary>
        Opened = 0B0001_0000
    }
}
