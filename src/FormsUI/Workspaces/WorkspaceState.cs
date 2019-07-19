using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    [Flags]
    public enum WorkspaceState
    {
        Inactive = 0B0000_0000,
        Active = 0B0000_0001,
        Clean = 0B0000_0011,
        Modified = 0B0000_0101
    }
}
