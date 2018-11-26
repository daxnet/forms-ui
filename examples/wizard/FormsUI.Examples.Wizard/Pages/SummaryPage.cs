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
    public partial class SummaryPage : WizardPage
    {
        public SummaryPage(Wizards.Wizard wizard)
            : base(new Guid(), "Summary", "Please review the summary info.", wizard)
        {
            InitializeComponent();
        }

        protected override Task ExecuteShowAsync(IWizardPage fromPage)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Installation Path:");
            sb.AppendLine($"    {Wizard.GetWizardParameter<string>("feature.install-destination")}");
            sb.AppendLine();
            sb.AppendLine("Feature Selection:");
            switch (Wizard.GetWizardParameter<int>("feature.feature"))
            {
                case 1:
                    sb.AppendLine("    Minimal installation");
                    break;
                case 2:
                    sb.AppendLine("    Standard installation");
                    break;
                case 3:
                    sb.AppendLine("    Full installation");
                    break;
            }

            txt.Text = sb.ToString();

            this.Wizard.NextButtonText = "Install";

            return base.ExecuteAfterShownAsync();
        }

        protected override Task<bool> ExecuteBeforeGoingPreviousAsync()
        {
            this.Wizard.NextButtonText = "Next";
            return base.ExecuteBeforeGoingPreviousAsync();
        }
    }
}
