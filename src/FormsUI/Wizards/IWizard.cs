
using System.Collections.Generic;

namespace FormsUI.Wizards
{
    /// <summary>
    /// Represents that the implemented classes are wizards.
    /// </summary>
    public interface IWizard : ICollection<WizardPageBase>
    {
        #region Public Properties

        /// <summary>
        /// Gets the parameters of the wizard.
        /// </summary>
        IEnumerable<KeyValuePair<string, object>> Parameters { get; }

        /// <summary>
        /// Gets or sets the text of the wizard. Usually this text will be displayed as the title of
        /// the wizard form.
        /// </summary>
        /// <value> The text of the wizard. </value>
        string Text { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// The factory method for creating a wizard page with specific wizard type.
        /// </summary>
        /// <typeparam name="T"> The type of the wizard page to be created. </typeparam>
        /// <returns> A <see cref="WizardPage" /> instance that is created. </returns>
        T CreatePage<T>() where T : WizardPageBase;

        #endregion Public Methods
    }
}