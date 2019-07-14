using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Windows
{
    public sealed class DockableWindowHiddenEventArgs : DockableWindowEventArgs
    { 
        public DockableWindowHiddenEventArgs(DockableWindow dockableWindow) : base(dockableWindow)
        {
        }
    }
}
