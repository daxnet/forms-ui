
using System;

namespace FormsUI.Extensions
{
    /// <summary>
    /// Represents that the decorated classes are extensions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public sealed class ExtensionAttribute : Attribute
    {

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionAttribute"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the extension.</param>
        /// <param name="name">The name of the extension.</param>
        public ExtensionAttribute(string id, string name)
        {
            this.Id = new Guid(id);
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionAttribute"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the extension.</param>
        /// <param name="name">The name of the extension.</param>
        /// <param name="settingProviderType">Type of the provider which provides the settings capability for the decorated extension.</param>
        public ExtensionAttribute(string id, string name, Type settingProviderType)
            : this(id, name)
        {
            this.SettingProviderType = settingProviderType;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the unique identifier of the decorated extension.
        /// </summary>
        /// <value>
        /// The unique identifier of the decorated extension.
        /// </value>
        public Guid Id { get; }

        /// <summary>
        /// Gets the name of the decorated extension.
        /// </summary>
        /// <value>
        /// The name of the decorated extension.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the provider which provides the settings capability for the decorated extension.
        /// </summary>
        /// <value>
        /// The type of the setting provider.
        /// </value>
        public Type SettingProviderType { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Id.ToString();
        }

        #endregion Public Methods

    }
}
