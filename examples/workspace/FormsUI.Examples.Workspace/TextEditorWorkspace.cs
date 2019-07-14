using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Examples.Workspace
{
    public sealed class TextEditorWorkspace : Workspaces.Workspace
    {
        protected override string WorkspaceFileDescription => "Text Files";

        protected override string WorkspaceFileExtension => "txt";

        protected override IWorkspaceModel Create()
        {
            return new TextEditorModel();
        }

        protected override IWorkspaceModel OpenFromFile(string fileName)
        {
            return new TextEditorModel(File.ReadAllText(fileName));
        }

        protected override void SaveToFile(IWorkspaceModel model, string fileName)
        {
            if (model is TextEditorModel textEditorModel)
            {
                File.WriteAllText(fileName, textEditorModel.Text);
            }
        }
    }
}
