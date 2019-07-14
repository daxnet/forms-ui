using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Examples.DockableWindows.Models
{
    [WorkspaceModelVersion(1, 0)]
    public sealed class NoteEditorModel : PropertyChangedNotifier, IWorkspaceModel
    {
        private ObservableCollection<Note> notes = new ObservableCollection<Note>();

        public WorkspaceModelVersion Version { get; set; }

        public NoteEditorModel()
        {
            BindObservableCollection(notes);
        }

        public void Add(Note note)
        {
            notes.Add(note);
        }

        public int Count => notes.Count;

        public IEnumerable<Note> Notes => notes;
    }
}
