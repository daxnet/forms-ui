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
    public partial class WelcomePage : WizardPage
    {
        public WelcomePage(Wizards.Wizard wizard)
            : base(new Guid("{EB48E8A7-279D-4095-970D-F0241D2BCECE}"),
                  "Welcome",
                  "Welcome to the software installer.",
                  wizard,
                  WizardPageType.Expanded) => InitializeComponent();

        protected override Task ExecuteShowAsync(IWizardPage fromPage)
        {
            this.lblTitle.Text = this.Title;
            this.lblDescription.Text = this.Description;
            return base.ExecuteShowAsync(fromPage);
        }
    }
}
