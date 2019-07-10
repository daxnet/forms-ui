using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsUI.Windows
{
    /// <summary>
    /// Represents the tool strip merging strategy.
    /// </summary>
    public class ToolStripMerge
    {
        /// <summary>
        /// Initializes a new instance of the <c>ToolStripMerge</c> class.
        /// </summary>
        /// <param name="toolStrip">The <see cref="ToolStrip"/> to be merged into the main window.</param>
        public ToolStripMerge(ToolStrip toolStrip)
            : this(toolStrip, true)
        { }

        /// <summary>
        /// Initializes a new instance of the <c>ToolStripMerge</c> class.
        /// </summary>
        /// <param name="toolStrip">The <see cref="ToolStrip"/> to be merged into the main window.</param>
        /// <param name="autoHide">A <see cref="bool"/> value which indicates whether the merged tool strip should
        /// be hidden when the window that contains this merged tool strip is hidden.</param>
        public ToolStripMerge(ToolStrip toolStrip, bool autoHide)
        {
            ToolStrip = toolStrip;
            AutoHide = autoHide;
        }

        /// <summary>
        /// Gets a <see cref="bool"/> value which indicates whether the merged tool strip should
        /// be hidden when the window that contains this merged tool strip is hidden.
        /// </summary>
        public bool AutoHide { get; }

        /// <summary>
        /// Gets the <see cref="ToolStrip"/> instance that is to be merged into the main window.
        /// </summary>
        public ToolStrip ToolStrip { get; }
    }
}
