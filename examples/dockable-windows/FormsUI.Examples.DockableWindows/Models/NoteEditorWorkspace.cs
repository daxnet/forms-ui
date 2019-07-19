using FormsUI.Workspaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Examples.DockableWindows.Models
{
    public sealed class NoteEditorWorkspace : Workspace
    {
        private static readonly JsonSerializerSettings serializerSettings =
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

        protected override string WorkspaceFileDescription => "Note Editor File";

        protected override string WorkspaceFileExtension => "nef";

        protected override string SaveDialogTitle => "Save Notes";

        protected override string OpenDialogTitle => "Open Notes";

        protected override IWorkspaceModel Create()
        {
            return new NoteEditorModel();
        }

        protected override IWorkspaceModel OpenFromFile(string fileName)
        {
            var json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<NoteEditorModel>(json);
        }

        protected override void SaveToFile(IWorkspaceModel model, string fileName)
        {
            var json = JsonConvert.SerializeObject(model, serializerSettings);
            File.WriteAllText(fileName, json);
        }
    }
}
