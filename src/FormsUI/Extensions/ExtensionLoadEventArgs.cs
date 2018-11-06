using System;

namespace FormsUI.Extensions
{
    /// <summary>
    /// Represents the class that carries the event data when the extension is being loading or has
    /// been loaded.
    /// </summary>
    internal sealed class ExtensionLoadEventArgs : EventArgs
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionLoadEventArgs"/> class.
        /// </summary>
        public ExtensionLoadEventArgs() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionLoadEventArgs"/> class.
        /// </summary>
        /// <param name="extensionName">Name of the extension.</param>
        public ExtensionLoadEventArgs(string extensionName)
        {
            this.ExtensionName = extensionName;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the name of the extension.
        /// </summary>
        /// <value>
        /// The name of the extension.
        /// </value>
        public string ExtensionName { get; set; }
        #endregion

    }
}
