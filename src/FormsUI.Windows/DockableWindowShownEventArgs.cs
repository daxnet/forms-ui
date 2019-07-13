using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Windows
{
    public sealed class DockableWindowShownEventArgs<TWorkspaceModel> : DockableWindowEventArgs<TWorkspaceModel>
        where TWorkspaceModel : IWorkspaceModel
    {
        public DockableWindowShownEventArgs(DockableWindow<TWorkspaceModel> dockableWindow) : base(dockableWindow)
        {
        }
    }
}
