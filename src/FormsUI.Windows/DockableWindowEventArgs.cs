using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Windows
{
    /// <summary>
    /// Represents the base class of the event data that holds an instance
    /// of a dockable window.
    /// </summary>
    public abstract class DockableWindowEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DockableWindowEventArgs"/> class.
        /// </summary>
        /// <param name="dockableWindow">The dockable window.</param>
        protected DockableWindowEventArgs(DockableWindow dockableWindow)
        {
            DockableWindow = dockableWindow;
        }

        /// <summary>
        /// Gets the instance of the dockable window.
        /// </summary>
        /// <value>
        /// The dockable window.
        /// </value>
        public DockableWindow DockableWindow { get; }
    }
}
