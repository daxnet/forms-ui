using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormsUI.Wizards;
using FormsUI.Examples.Wizard.Properties;
using System.IO;

namespace FormsUI.Examples.Wizard.Pages
{
    public partial class LicensePage : WizardPage
    {
        public LicensePage(Wizards.Wizard wizard)
            : base(new Guid("{45AB2D37-3B81-4191-92EF-03134072C71B}"), "License Agreement", "Please read the license terms before installing the software.",
                  wizard) => InitializeComponent();

        protected override Task ExecuteShowAsync(IWizardPage fromPage)
        {
            var license = Encoding.ASCII.GetBytes(Resources.Apache20);
            using (var memoryStream = new MemoryStream(license))
            {
                this.richTextBox1.LoadFile(memoryStream, RichTextBoxStreamType.RichText);
            }

            rbNotAccept.Checked = true;
            UpdateControls();

            return base.ExecuteShowAsync(fromPage);
        }

        private void OnRadioButtonClicked(object sender, EventArgs e) => UpdateControls();

        private void UpdateControls() => this.CanGoNextPage = rbAccept.Checked;
    }
}
