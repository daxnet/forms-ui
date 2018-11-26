using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FormsUI.Wizards;

namespace FormsUI.Examples.Wizard.Pages
{
    public partial class FeaturePage : WizardPage
    {
        public FeaturePage(Wizards.Wizard wizard)
            : base(new Guid(), "Features", "Choose the feature set you'd like to install, as well as the installation destination.", wizard) => InitializeComponent();

        protected override Task ExecuteShowAsync(IWizardPage fromPage)
        {
            var installDestination = Wizard.GetWizardParameter<string>("feature.install-destination");
            if (string.IsNullOrEmpty(installDestination))
            {
                txtInstallDest.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "FormsUIExample");
                Wizard.AddParameter("feature.install-destination", txtInstallDest.Text);
            }
            else
            {
                txtInstallDest.Text = installDestination;
            }

            var feature = Wizard.GetWizardParameter<int?>("feature.feature");
            if (!feature.HasValue)
            {
                feature = 1;
                Wizard.AddParameter("feature.feature", feature.Value);
            }

            foreach (Control c in grpFeatureSelection.Controls)
            {
                if (c is RadioButton rb && Convert.ToInt32(rb.Tag) == feature.Value)
                {
                    rb.Checked = true;
                }
            }

            return base.ExecuteShowAsync(fromPage);
        }

        protected override Task<bool> ValidateParametersAsync()
        {
            if (string.IsNullOrEmpty(txtInstallDest.Text))
            {
                MessageBox.Show("Please specify the installation destination.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return Task.FromResult(false);
            }

            if (!Directory.Exists(txtInstallDest.Text))
            {
                MessageBox.Show("The specified installation destination does not exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return Task.FromResult(false);
            }

            return base.ValidateParametersAsync();
        }

        protected override Task<bool> ExecuteBeforeLeavingAsync()
        {
            Wizard.AddParameter("feature.install-destination", txtInstallDest.Text);
            foreach (Control c in grpFeatureSelection.Controls)
            {
                if (c is RadioButton rb && rb.Checked)
                {
                    Wizard.AddParameter("feature.feature", Convert.ToInt32(rb.Tag));
                }
            }

            return base.ExecuteBeforeLeavingAsync();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtInstallDest.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
