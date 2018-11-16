using FormsUI.Examples.Wizard.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsUI.Examples.Wizard
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnOpenWizard_Click(object sender, EventArgs e)
        {
            using (var wizard = new Installer())
            {
                wizard.Add(wizard.CreatePage<WelcomePage>());
                wizard.Add(wizard.CreatePage<LicensePage>());
                wizard.Add(wizard.CreatePage<FeaturePage>());
                wizard.Add(wizard.CreatePage<InstallationPage>());
                wizard.ShowDialog();
            }
        }
    }
}
