using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    public sealed class WorkspaceCreatedEventArgs : EventArgs
    {
        public WorkspaceCreatedEventArgs(IWorkspaceModel model)
        {
            this.Model = model;
        }

        public IWorkspaceModel Model { get; }
    }
}
