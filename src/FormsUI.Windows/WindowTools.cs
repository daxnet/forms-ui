using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Windows
{
    public sealed class WindowTools
    {
        public static readonly WindowTools Empty = new WindowTools();

        private readonly List<MenuStripMerge> mergingMenus = new List<MenuStripMerge>();

        private WindowTools() { }

        public WindowTools(ToolStripMerge mergingToolbar)
        {
            this.MergingToolbar = mergingToolbar;
        }

        public WindowTools(ToolStripMerge mergingToolbar, IEnumerable<MenuStripMerge> mergingMenus)
            : this(mergingToolbar)
        {
            this.mergingMenus.AddRange(mergingMenus);
        }

        public ToolStripMerge MergingToolbar { get; }

        public IEnumerable<MenuStripMerge> MergingMenus => mergingMenus;

        public bool IsEmpty => MergingToolbar == null && mergingMenus?.Count == 0;
    }
}
