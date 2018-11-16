namespace FormsUI.Examples.Wizard.Pages
{
    partial class FeaturePage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbFullInstall = new System.Windows.Forms.RadioButton();
            this.rbStandardInstall = new System.Windows.Forms.RadioButton();
            this.rbMinInstall = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtInstallDest = new System.Windows.Forms.TextBox();
            this.btnChoose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbFullInstall);
            this.groupBox1.Controls.Add(this.rbStandardInstall);
            this.groupBox1.Controls.Add(this.rbMinInstall);
            this.groupBox1.Location = new System.Drawing.Point(18, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 165);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Feature Selection";
            // 
            // rbFullInstall
            // 
            this.rbFullInstall.AutoSize = true;
            this.rbFullInstall.Location = new System.Drawing.Point(6, 130);
            this.rbFullInstall.Name = "rbFullInstall";
            this.rbFullInstall.Size = new System.Drawing.Size(93, 17);
            this.rbFullInstall.TabIndex = 5;
            this.rbFullInstall.TabStop = true;
            this.rbFullInstall.Text = "Full installation";
            this.rbFullInstall.UseVisualStyleBackColor = true;
            // 
            // rbStandardInstall
            // 
            this.rbStandardInstall.AutoSize = true;
            this.rbStandardInstall.Location = new System.Drawing.Point(6, 77);
            this.rbStandardInstall.Name = "rbStandardInstall";
            this.rbStandardInstall.Size = new System.Drawing.Size(120, 17);
            this.rbStandardInstall.TabIndex = 4;
            this.rbStandardInstall.TabStop = true;
            this.rbStandardInstall.Text = "Standard installation";
            this.rbStandardInstall.UseVisualStyleBackColor = true;
            // 
            // rbMinInstall
            // 
            this.rbMinInstall.AutoSize = true;
            this.rbMinInstall.Location = new System.Drawing.Point(6, 30);
            this.rbMinInstall.Name = "rbMinInstall";
            this.rbMinInstall.Size = new System.Drawing.Size(112, 17);
            this.rbMinInstall.TabIndex = 3;
            this.rbMinInstall.TabStop = true;
            this.rbMinInstall.Text = "Minimal installation";
            this.rbMinInstall.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnChoose);
            this.groupBox2.Controls.Add(this.txtInstallDest);
            this.groupBox2.Location = new System.Drawing.Point(18, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(498, 55);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Installation Destination";
            // 
            // txtInstallDest
            // 
            this.txtInstallDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInstallDest.Location = new System.Drawing.Point(6, 23);
            this.txtInstallDest.Name = "txtInstallDest";
            this.txtInstallDest.Size = new System.Drawing.Size(390, 20);
            this.txtInstallDest.TabIndex = 0;
            // 
            // btnChoose
            // 
            this.btnChoose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnChoose.Location = new System.Drawing.Point(402, 20);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(90, 23);
            this.btnChoose.TabIndex = 1;
            this.btnChoose.Text = "Choose...";
            this.btnChoose.UseVisualStyleBackColor = true;
            // 
            // FeaturePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FeaturePage";
            this.Size = new System.Drawing.Size(535, 460);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFullInstall;
        private System.Windows.Forms.RadioButton rbStandardInstall;
        private System.Windows.Forms.RadioButton rbMinInstall;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtInstallDest;
        private System.Windows.Forms.Button btnChoose;
    }
}
