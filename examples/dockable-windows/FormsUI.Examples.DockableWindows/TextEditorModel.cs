using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Examples.DockableWindows
{
    public sealed class TextEditorModel : IWorkspaceModel
    {
        public WorkspaceModelVersion Version { get; set; }

        public TextEditorModel() { }

        public TextEditorModel(string text)
        {
            this.text = text;
        }

        private string text;

        public string Text
        {
            get => text;
            set
            {
                if (!string.Equals(text, value))
                {
                    text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
