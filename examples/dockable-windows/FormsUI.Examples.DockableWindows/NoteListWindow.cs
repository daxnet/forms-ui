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
    public partial class NoteListWindow : ToolWindow
    {

        #region Private Fields

        private readonly WindowTools windowTools;
        private NoteEditorModel appModel;

        #endregion Private Fields

        #region Public Constructors

        public NoteListWindow(IAppWindow appWindow)
            : base(appWindow)
        {
            InitializeComponent();
            windowTools = new WindowTools(new ToolStripMerge( toolStrip1, true));
        }

        #endregion Public Constructors

        #region Protected Properties

        protected override WindowTools WindowTools => windowTools;

        #endregion Protected Properties

        #region Protected Methods

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            tbtnAddNote.Enabled = false;
            tbtnDeleteNote.Enabled = false;
        }


        protected override void OnWorkspaceClosed(object sender, EventArgs e)
        {
            lst.Items.Clear();
            tbtnDeleteNote.Enabled = false;
            tbtnAddNote.Enabled = false;
        }

        protected override void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs e)
        {
            appModel = e.Model as NoteEditorModel;
            InitializeNoteList(appModel);
            var note = appModel.Notes.First();
            OpenWindowForNote(note);
            tbtnAddNote.Enabled = true;
        }
        protected override void OnWorkspaceOpened(object sender, WorkspaceOpenedEventArgs e)
        {
            appModel = e.Model as NoteEditorModel;
            InitializeNoteList(appModel);
            var note = appModel.Notes.First();
            OpenWindowForNote(note);
            tbtnAddNote.Enabled = true;
        }

        #endregion Protected Methods

        #region Private Methods

        private void CmnuAddNewNote_Click(object sender, EventArgs e)
        {
            var note = new Note { Title = FindAvailableId() };
            var lvi = new ListViewItem(note.Title)
            {
                Tag = note,
                ImageKey = "Note"
            };

            lst.Items.Add(lvi);
            appModel.Add(note);
            OpenWindowForNote(note);
        }

        private void CmnuDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                var selectedNote = lst.SelectedItems?[0]?.Tag as Note;
                if (selectedNote != null)
                {
                    lst.Items.Remove(lst.SelectedItems?[0]);
                    appModel.Delete(selectedNote);
                    var editorWindow = AppWindow.WindowManager.GetFirstWindow<EditorWindow>(w => w.Note.Equals(selectedNote));
                    editorWindow?.Close();
                }
            }
        }

        private void CmnuRename_Click(object sender, EventArgs e)
        {
            lst.SelectedItems?[0]?.BeginEdit();
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!AppWindow.Workspace.IsActive)
            {
                cmnuAddNewNote.Enabled = false;
                cmnuRename.Enabled = false;
                cmnuDelete.Enabled = false;
            }
            else
            {
                cmnuAddNewNote.Enabled = true;
                cmnuRename.Enabled = lst.SelectedItems.Count == 1;
                cmnuDelete.Enabled = lst.SelectedItems.Count > 0;
            }
        }

        private string FindAvailableId()
        {
            var idx = 1;
            while (true)
            {
                if (!appModel.Notes.Any(x => x.Title.Equals($"Note{idx}")))
                {
                    break;
                }
                idx++;
            }

            return $"Note{idx}";
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
        private void Lst_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            // If empty, reject
            if (string.IsNullOrWhiteSpace(e.Label))
            {
                e.CancelEdit = true;
                return;
            }

            // Check whether the note with the name already exists. If yes, reject the edit.
            for (var idx = 0; idx < lst.Items.Count; idx++)
            {
                if (idx != e.Item && lst.Items[idx].Text == e.Label)
                {
                    MessageBox.Show($"The name \"{e.Label}\" already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.CancelEdit = true;
                    return;
                }
            }

            // Change the title of the note.
            var listViewItem = lst.Items[e.Item];
            if (listViewItem != null)
            {
                var selectedNote = listViewItem.Tag as Note;
                AppWindow.WindowManager.GetFirstWindow<EditorWindow>(w => w.Note.Title.Equals(selectedNote.Title)).Text = e.Label;
                selectedNote.Title = e.Label;
                listViewItem.Text = e.Label;
            }
        }

        private void Lst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var listViewItem = lst.GetItemAt(e.X, e.Y);
            if (listViewItem != null)
            {
                var note = (Note)listViewItem.Tag;
                OpenWindowForNote(note);
            }
        }

        private void Lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbtnDeleteNote.Enabled = lst.SelectedItems.Count > 0;
        }

        private void OpenWindowForNote(Note note)
        {
            var editorWindow = AppWindow.WindowManager.GetFirstWindow<EditorWindow>(w => w.Note.Title == note.Title);
            if (editorWindow != null)
            {
                editorWindow.Show();
            }
            else
            {
                editorWindow = AppWindow.WindowManager.CreateWindow<EditorWindow>(note);
                editorWindow.Text = note.Title;

                editorWindow.FormClosing += (fcSender, fsArgs) =>
                {
                    note.Content = editorWindow.Note.Content;
                };

                editorWindow.Show(AppWindow.DockArea, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            }
        }

        #endregion Private Methods
    }
}
