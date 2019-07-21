using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace FormsUI.Windows
{
    /// <summary>
    /// Represents that the implemented classes are application windows.
    /// An application window is a window instance which manages workspaces.
    /// Usually a Windows Forms application will have only one <c>IAppWindow</c>
    /// instance.
    /// </summary>
    /// <typeparam name="TWorkspaceModel">The type of the workspace model.</typeparam>
    public interface IAppWindow
    {
        Workspace Workspace { get; }

        DockableWindowManager WindowManager { get; }

        DockPanel DockArea { get; }

        void MergeTools(WindowTools tools);

        void RevertMerge(WindowTools tools);
    }
}
