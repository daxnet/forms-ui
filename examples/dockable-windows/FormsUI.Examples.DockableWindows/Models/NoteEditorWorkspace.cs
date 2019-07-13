using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Examples.DockableWindows.Models
{
    public sealed class NoteEditorWorkspace : Workspace<NoteEditorModel>
    {
        protected override string WorkspaceFileDescription => throw new NotImplementedException();

        protected override string WorkspaceFileExtension => throw new NotImplementedException();

        protected override NoteEditorModel Create()
        {
            throw new NotImplementedException();
        }

        protected override NoteEditorModel OpenFromFile(string fileName)
        {
            throw new NotImplementedException();
        }

        protected override void SaveToFile(NoteEditorModel model, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
