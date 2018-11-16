namespace FormsUI.Examples.Wizard
{
    partial class FrmMain
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
            this.btnOpenWizard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenWizard
            // 
            this.btnOpenWizard.Location = new System.Drawing.Point(76, 42);
            this.btnOpenWizard.Name = "btnOpenWizard";
            this.btnOpenWizard.Size = new System.Drawing.Size(109, 23);
            this.btnOpenWizard.TabIndex = 0;
            this.btnOpenWizard.Text = "Open Wizard...";
            this.btnOpenWizard.UseVisualStyleBackColor = true;
            this.btnOpenWizard.Click += new System.EventHandler(this.btnOpenWizard_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 98);
            this.Controls.Add(this.btnOpenWizard);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenWizard;
    }
}

