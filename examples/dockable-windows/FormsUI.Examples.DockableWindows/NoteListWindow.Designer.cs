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
            this.contextMenuStrip1.SuspendLayout();
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
            this.lst.Location = new System.Drawing.Point(0, 0);
            this.lst.MultiSelect = false;
            this.lst.Name = "lst";
            this.lst.Size = new System.Drawing.Size(484, 732);
            this.lst.SmallImageList = this.imageList1;
            this.lst.TabIndex = 0;
            this.lst.UseCompatibleStateImageBehavior = false;
            this.lst.View = System.Windows.Forms.View.Details;
            this.lst.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.Lst_AfterLabelEdit);
            this.lst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Lst_MouseDoubleClick);
            // 
            // colNoteTitle
            // 
            this.colNoteTitle.Text = "Title";
            this.colNoteTitle.Width = 180;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuAddNewNote,
            this.toolStripMenuItem1,
            this.cmnuRename,
            this.cmnuDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(277, 130);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // cmnuAddNewNote
            // 
            this.cmnuAddNewNote.Image = global::FormsUI.Examples.DockableWindows.Properties.Resources.page_white_add;
            this.cmnuAddNewNote.Name = "cmnuAddNewNote";
            this.cmnuAddNewNote.Size = new System.Drawing.Size(276, 40);
            this.cmnuAddNewNote.Text = "Add New Note";
            this.cmnuAddNewNote.Click += new System.EventHandler(this.CmnuAddNewNote_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(273, 6);
            // 
            // cmnuRename
            // 
            this.cmnuRename.Name = "cmnuRename";
            this.cmnuRename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.cmnuRename.Size = new System.Drawing.Size(276, 40);
            this.cmnuRename.Text = "Rename";
            this.cmnuRename.Click += new System.EventHandler(this.CmnuRename_Click);
            // 
            // cmnuDelete
            // 
            this.cmnuDelete.Image = global::FormsUI.Examples.DockableWindows.Properties.Resources.page_white_delete;
            this.cmnuDelete.Name = "cmnuDelete";
            this.cmnuDelete.Size = new System.Drawing.Size(276, 40);
            this.cmnuDelete.Text = "Delete...";
            this.cmnuDelete.Click += new System.EventHandler(this.CmnuDelete_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Note");
            // 
            // NoteListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 732);
            this.Controls.Add(this.lst);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "NoteListWindow";
            this.Text = "My Notes";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

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
    }
}