
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsUI.Wizards
{
    /// <summary>
    /// Represents a wizard page. All custom wizard pages should inherit from this class, instead of
    /// <see cref="WizardPageBase" />.
    /// </summary>
    /// <remarks>
    /// Although this class is not marked as abstract, it still should be the base class for all the
    /// wizard pages. Marking this class as abstract will prevent developers from customizing the
    /// wizard page by using Windows Forms Designer.
    /// </remarks>
    public partial class WizardPage : WizardPageBase
    {

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardPage" /> class.
        /// </summary>
        /// <param name="title"> The title of the current wizard page. </param>
        /// <param name="description"> The description of the current wizard page. </param>
        /// <param name="wizard">
        /// The <see cref="Wizard" /> instance which contains the current wizard page.
        /// </param>
        /// <param name="model"> The data model of the current wizard page. </param>
        protected WizardPage(Guid pageId, string title, string description, Wizard wizard)
            : this(pageId, title, description, wizard, WizardPageType.Standard)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardPage" /> class.
        /// </summary>
        /// <param name="title"> The title of the current wizard page. </param>
        /// <param name="description"> The description of the current wizard page. </param>
        /// <param name="wizard">
        /// The <see cref="Wizard" /> instance which contains the current wizard page.
        /// </param>
        /// <param name="model"> The data model of the current wizard page. </param>
        /// <param name="type"> The type of the current wizard page. </param>
        protected WizardPage(Guid pageId, string title, string description, Wizard wizard, WizardPageType type)
            : base(title, description, wizard, type) => PageId = pageId;

        /// <summary>
        /// Prevents a default instance of the <see cref="WizardPage" /> class from being created.
        /// </summary>
        protected WizardPage()
        {
            InitializeComponent();
        }

        #endregion Protected Constructors

        #region Public Properties

        public override Guid PageId { get; }

        #endregion Public Properties

        #region Protected Internal Properties

        /// <summary>
        /// Gets the focusing control, which will be focused when the wizard page is showing.
        /// </summary>
        /// <value>
        /// The focusing control.
        /// </value>
        protected internal override Control FocusingControl
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the logo image to be shown on the title area of the wizard.
        /// </summary>
        /// <value>
        /// The logo image.
        /// </value>
        protected internal override System.Drawing.Image Logo
        {
            get { return null; }
        }

        #endregion Protected Internal Properties

        #region Internal Methods

        /// <summary>
        /// Gets the empty page.
        /// </summary>
        /// <param name="wizard">The wizard.</param>
        /// <returns></returns>
        internal static WizardPage GetEmptyPage(Wizard wizard)
        {
            return new WizardPage(Guid.Empty, "", "", wizard)
            {
                CanGoCancel = false,
                CanGoFinishPage = false,
                CanGoNextPage = false,
                CanGoPreviousPage = false
            };
        }

        #endregion Internal Methods

        #region Protected Internal Methods

        /// <summary>
        /// Clean up the current wizard page before the wizard is going to be closed.
        /// </summary>
        protected internal virtual void CleanUp() { }

        /// <summary>
        /// Executes the after shown asynchronous.
        /// </summary>
        /// <returns></returns>
        protected internal override Task ExecuteAfterShownAsync() => Task.CompletedTask;

        /// <summary>
        /// The callback method being executed when user clicks the Finish button on the wizard,
        /// but before the wizard is going to finish and close.
        /// </summary>
        /// <returns>
        ///   <c>True</c> if the wizard can go to the finish page, otherwise, <c>false</c>.
        /// </returns>
        protected internal override Task<bool> ExecuteBeforeGoingFinishAsync() => Task.FromResult(true);

        /// <summary>
        /// The callback method being executed when user clicks the Next button on the wizard, but
        /// before the wizard is opening the next page.
        /// </summary>
        /// <returns>
        ///   <c>True</c> if the wizard can go to the next page, otherwise, <c>false</c>.
        /// </returns>
        protected internal override Task<bool> ExecuteBeforeGoingNextAsync() => Task.FromResult(true);

        /// <summary>
        /// The callback method being executed when user clicks the Previous button on the wizard,
        /// but before the wizard is opening the previous page.
        /// </summary>
        /// <returns>
        ///   <c>True</c> if the wizard can go to the previous page, otherwise, <c>false</c>.
        /// </returns>
        protected internal override Task<bool> ExecuteBeforeGoingPreviousAsync() => Task.FromResult(true);

        /// <summary>
        /// The callback method being executed when the wizard is going to leave the current page.
        /// </summary>
        /// <returns></returns>
        protected internal override Task<bool> ExecuteBeforeLeavingAsync() => Task.FromResult(true);

        /// <summary>
        /// The callback method being executed when the current wizard page is showing.
        /// </summary>
        protected internal override Task ExecuteShowAsync(IWizardPage fromPage) => Task.CompletedTask;

        protected internal override Task<bool> ValidateParametersAsync() => Task.FromResult(true);

        #endregion Protected Internal Methods
    }
}