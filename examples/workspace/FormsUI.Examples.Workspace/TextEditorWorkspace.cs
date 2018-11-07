using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Examples.Workspace
{
    public sealed class TextEditorWorkspace : Workspace<TextEditorModel>
    {
        protected override string WorkspaceFileDescription => "Text Files";

        protected override string WorkspaceFileExtension => "txt";

        protected override TextEditorModel Create()
        {
            return new TextEditorModel();
        }

        protected override TextEditorModel OpenFromFile(string fileName)
        {
            return new TextEditorModel(File.ReadAllText(fileName));
        }

        protected override void SaveToFile(TextEditorModel model, string fileName)
        {
            File.WriteAllText(fileName, model.Text);
        }
    }
}
