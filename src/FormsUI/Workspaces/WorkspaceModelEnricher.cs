using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    /// <summary>
    /// Represents a group of methods that enrich the specified workspace model.
    /// </summary>
    /// <param name="input">The workspace model to be enriched.</param>
    /// <returns>The enriched workspace model.</returns>
    public delegate (bool, IWorkspaceModel) WorkspaceModelEnricher(IWorkspaceModel input);
}
