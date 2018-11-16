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

namespace FormsUI.Examples.Wizard.Pages
{
    public partial class InstallationPage : WizardPage
    {
        public InstallationPage(Wizards.Wizard wizard)
            : base(new Guid(), "Installing In Progress", "", wizard)
        {
            InitializeComponent();
        }
    }
}
