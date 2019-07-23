namespace FormsUI.Examples.DockableWindows
{
    partial class NoteListWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoteListWindow));
            this.lst = new System.Windows.Forms.ListView();
            this.colNoteTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuAddNewNote = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtnAddNote = new System.Windows.Forms.ToolStripButton();
            this.tbtnDeleteNote = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lst
            // 
            this.lst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNoteTitle});
            this.lst.ContextMenuStrip = this.contextMenuStrip1;
            this.lst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst.HideSelection = false;
            this.lst.LabelEdit = true;
            this.lst.Location = new System.Drawing.Point(0, 25);
            this.lst.MultiSelect = false;
            this.lst.Name = "lst";
            this.lst.Size = new System.Drawing.Size(396, 428);
            this.lst.SmallImageList = this.imageList1;
            this.lst.TabIndex = 0;
            this.lst.UseCompatibleStateImageBehavior = false;
            this.lst.View = System.Windows.Forms.View.Details;
            this.lst.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.Lst_AfterLabelEdit);
            this.lst.SelectedIndexChanged += new System.EventHandler(this.Lst_SelectedIndexChanged);
            this.lst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Lst_MouseDoubleClick);
            // 
            // colNoteTitle
            // 
            this.colNoteTitle.Text = "Title";
            this.colNoteTitle.Width = 180;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuAddNewNote,
            this.toolStripMenuItem1,
            this.cmnuRename,
            this.cmnuDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 98);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // cmnuAddNewNote
            // 
            this.cmnuAddNewNote.Image = global::FormsUI.Examples.DockableWindows.Properties.Resources.page_white_add;
            this.cmnuAddNewNote.Name = "cmnuAddNewNote";
            this.cmnuAddNewNote.Size = new System.Drawing.Size(180, 22);
            this.cmnuAddNewNote.Text = "Add New Note";
            this.cmnuAddNewNote.Click += new System.EventHandler(this.CmnuAddNewNote_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // cmnuRename
            // 
            this.cmnuRename.Name = "cmnuRename";
            this.cmnuRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.cmnuRename.Size = new System.Drawing.Size(180, 22);
            this.cmnuRename.Text = "Rename";
            this.cmnuRename.Click += new System.EventHandler(this.CmnuRename_Click);
            // 
            // cmnuDelete
            // 
            this.cmnuDelete.Image = global::FormsUI.Examples.DockableWindows.Properties.Resources.page_white_delete;
            this.cmnuDelete.Name = "cmnuDelete";
            this.cmnuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.cmnuDelete.Size = new System.Drawing.Size(180, 22);
            this.cmnuDelete.Text = "Delete...";
            this.cmnuDelete.Click += new System.EventHandler(this.CmnuDelete_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Note");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnAddNote,
            this.tbtnDeleteNote,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(396, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbtnAddNote
            // 
            this.tbtnAddNote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnAddNote.Image = global::FormsUI.Examples.DockableWindows.Properties.Resources.page_white_add;
            this.tbtnAddNote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnAddNote.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.tbtnAddNote.MergeIndex = 4;
            this.tbtnAddNote.Name = "tbtnAddNote";
            this.tbtnAddNote.Size = new System.Drawing.Size(23, 22);
            this.tbtnAddNote.Text = "toolStripButton2";
            this.tbtnAddNote.Click += new System.EventHandler(this.CmnuAddNewNote_Click);
            // 
            // tbtnDeleteNote
            // 
            this.tbtnDeleteNote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnDeleteNote.Image = global::FormsUI.Examples.DockableWindows.Properties.Resources.page_white_delete;
            this.tbtnDeleteNote.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnDeleteNote.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.tbtnDeleteNote.MergeIndex = 5;
            this.tbtnDeleteNote.Name = "tbtnDeleteNote";
            this.tbtnDeleteNote.Size = new System.Drawing.Size(23, 22);
            this.tbtnDeleteNote.Text = "toolStripButton3";
            this.tbtnDeleteNote.Click += new System.EventHandler(this.CmnuDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.MergeIndex = 6;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // NoteListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 453);
            this.Controls.Add(this.lst);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "NoteListWindow";
            this.Text = "My Notes";
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lst;
        private System.Windows.Forms.ColumnHeader colNoteTitle;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmnuAddNewNote;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cmnuDelete;
        private System.Windows.Forms.ToolStripMenuItem cmnuRename;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbtnAddNote;
        private System.Windows.Forms.ToolStripButton tbtnDeleteNote;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}