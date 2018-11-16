namespace FormsUI.Examples.Wizard.Pages
{
    partial class LicensePage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.rbAccept = new System.Windows.Forms.RadioButton();
            this.rbNotAccept = new System.Windows.Forms.RadioButton();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.rbNotAccept);
            this.pnlBottom.Controls.Add(this.rbAccept);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 367);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(711, 69);
            this.pnlBottom.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(711, 367);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // rbAccept
            // 
            this.rbAccept.AutoSize = true;
            this.rbAccept.Location = new System.Drawing.Point(20, 16);
            this.rbAccept.Name = "rbAccept";
            this.rbAccept.Size = new System.Drawing.Size(138, 17);
            this.rbAccept.TabIndex = 0;
            this.rbAccept.TabStop = true;
            this.rbAccept.Text = "I accept the agreement.";
            this.rbAccept.UseVisualStyleBackColor = true;
            this.rbAccept.Click += new System.EventHandler(this.OnRadioButtonClicked);
            // 
            // rbNotAccept
            // 
            this.rbNotAccept.AutoSize = true;
            this.rbNotAccept.Location = new System.Drawing.Point(20, 39);
            this.rbNotAccept.Name = "rbNotAccept";
            this.rbNotAccept.Size = new System.Drawing.Size(171, 17);
            this.rbNotAccept.TabIndex = 1;
            this.rbNotAccept.TabStop = true;
            this.rbNotAccept.Text = "I do not accept the agreement.";
            this.rbNotAccept.UseVisualStyleBackColor = true;
            this.rbNotAccept.Click += new System.EventHandler(this.OnRadioButtonClicked);
            // 
            // LicensePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.pnlBottom);
            this.Name = "LicensePage";
            this.Size = new System.Drawing.Size(711, 436);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RadioButton rbNotAccept;
        private System.Windows.Forms.RadioButton rbAccept;
    }
}
