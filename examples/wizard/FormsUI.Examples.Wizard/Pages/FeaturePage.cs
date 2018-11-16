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

            return base.ExecuteShowAsync(fromPage);
        }

        protected override Task<bool> ExecuteBeforeLeavingAsync()
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

            Wizard.AddParameter("feature.install-destination", txtInstallDest.Text);
            return base.ExecuteBeforeLeavingAsync();
        }
    }
}
