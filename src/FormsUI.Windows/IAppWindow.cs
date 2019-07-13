using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Windows
{
    /// <summary>
    /// Represents that the implemented classes are application windows.
    /// An application window is a window instance which manages workspaces.
    /// Usually a Windows Forms application will have only one <c>IAppWindow</c>
    /// instance.
    /// </summary>
    /// <typeparam name="TWorkspaceModel">The type of the workspace model.</typeparam>
    public interface IAppWindow<TWorkspaceModel>
        where TWorkspaceModel : IWorkspaceModel
    {
        Workspace<TWorkspaceModel> Workspace { get; }

        DockableWindowManager<TWorkspaceModel> WindowManager { get; }
    }
}
