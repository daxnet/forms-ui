using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    public sealed class WorkspaceSavedEventArgs : WorkspaceEventArgs
    {
        public WorkspaceSavedEventArgs(string fileName, IWorkspaceModel model) : base(fileName, model)
        {
        }
    }
}
