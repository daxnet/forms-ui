using FormsUI.Examples.DockableWindows.Models;
using FormsUI.Windows;
using FormsUI.Workspaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsUI.Examples.DockableWindows
{
    public partial class NoteListWindow : DockableWindow
    {
        public NoteListWindow(IAppWindow appWindow)
            : base(appWindow)
        {
            InitializeComponent();
        }

        protected override void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs e)
        {
            var noteEditorModel = e.Model as NoteEditorModel;
            InitializeNoteList(noteEditorModel);
        }

        protected override void OnWorkspaceClosed(object sender, EventArgs e)
        {
            lst.Items.Clear();
        }

        protected override void OnWorkspaceOpened(object sender, WorkspaceOpenedEventArgs e)
        {
            var noteEditorModel = e.Model as NoteEditorModel;
            InitializeNoteList(noteEditorModel);
        }

        private void InitializeNoteList(NoteEditorModel noteEditorModel)
        {
            if (noteEditorModel.Count > 0)
            {
                lst.Items.Clear();
                foreach (var note in noteEditorModel.Notes)
                {
                    var lvi = new ListViewItem(note.Title)
                    {
                        Tag = note,
                        ImageKey = "Note"
                    };

                    lst.Items.Add(lvi);
                }
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }

        private void Lst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var listViewItem = lst.GetItemAt(e.X, e.Y);
            if (listViewItem!=null)
            {
                var note = (Note)listViewItem.Tag;
                var editorWindow = AppWindow.WindowManager.GetFirstWindow<EditorWindow>(w => w.Note.Title == note.Title);
                if (editorWindow != null)
                {
                    editorWindow.Show();
                }
                else
                {
                    editorWindow = AppWindow.WindowManager.CreateWindow<EditorWindow>(note);
                    editorWindow.FormClosing += (fcSender, fsArgs) =>
                      {
                          note.Content = editorWindow.Note.Content;
                      };

                    editorWindow.Show(AppWindow.DockArea, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
            }
        }
    }
}
