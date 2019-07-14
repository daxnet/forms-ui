using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    public class WorkspaceEventArgs : EventArgs
    {
        public WorkspaceEventArgs(string fileName, IWorkspaceModel model)
        {
            this.FileName = fileName;
            this.Model = model;
        }

        public string FileName { get; }

        public IWorkspaceModel Model { get; }
    }
}
