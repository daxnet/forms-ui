using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    public sealed class WorkspaceStateChangedEventArgs : EventArgs
    {
        public WorkspaceStateChangedEventArgs(WorkspaceState state) => State = state;
            

        public WorkspaceState State { get; }

        public override string ToString() => State.ToString();
    }
}
