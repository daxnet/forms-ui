using System;
using System.Reflection;
using System.Drawing;
using System.Collections.Generic;

namespace FormsUI.Extensions
{
    /// <summary>
    /// Represents the base class for all of the extensions.
    /// </summary>
    public abstract class Extension : IResource
    {

        #region Private Fields

        private ExtensionAttribute extensionAttribute;

        private ExtensionSettingsProvider settingProvider;

        #endregion Private Fields

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Extension"/> class.
        /// </summary>
        protected Extension() { }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// Gets the description of the extension.
        /// </summary>
        /// <value>
        /// The description of the extension.
        /// </value>
        public abstract string Description { get; }

        /// <summary>
        /// Gets the display name of the extension.
        /// </summary>
        /// <value>
        /// The display name of the extension.
        /// </value>
        public virtual string DisplayName => Name;

        /// <summary>
        /// Gets the icon which represents the current extension.
        /// </summary>
        /// <value>
        /// The icon of the extension.
        /// </value>
        public virtual Image Icon => null;

        /// <summary>
        /// Gets the unique identifier of the current extension.
        /// </summary>
        /// <value>
        /// The unique identifier of the extension.
        /// </value>
        /// <exception cref="ExtensionException">The extension was not decorated with ExtensionAttribute attribute.</exception>
        public Guid Id
        {
            get
            {
                if (this.ExtensionAttribute != null)
                {
                    return ExtensionAttribute.Id;
                }

                throw new InvalidOperationException("Current extension instance has not been decorated with ExtensionAttribute.");
            }
            set
            {
                // No setter operation is applicable to the current Extension instance.
            }
        }

        /// <summary>
        /// Gets the name of the manufacturer of the current extension.
        /// </summary>
        /// <value>
        /// The manufacturer of the extension.
        /// </value>
        public abstract string Manufacturer { get; }

        /// <summary>
        /// Gets the name of the current extension.
        /// </summary>
        /// <value>
        /// The name of the extension.
        /// </value>
        /// <exception cref="ExtensionException">The extension was not decorated with ExtensionAttribute attribute.</exception>
        public string Name
        {
            get
            {
                if (this.ExtensionAttribute != null)
                {
                    return this.ExtensionAttribute.Name;
                }

                throw new InvalidOperationException("Current extension instance has not been decorated with ExtensionAttribute.");
            }
        }

        /// <summary>
        /// Gets <see cref="ExtensionSettingsProvider"/> which provides the setting capability for the current extension.
        /// </summary>
        /// <value>
        /// The setting provider.
        /// </value>
        public ExtensionSettingsProvider SettingProvider
        {
            get
            {
                if (this.settingProvider == null)
                {
                    if (this.ExtensionAttribute == null)
                    {
                        throw new InvalidOperationException("Current extension instance has not been decorated with ExtensionAttribute.");
                    }

                    if (this.ExtensionAttribute.SettingProviderType != null &&
                        this.ExtensionAttribute.SettingProviderType.IsSubclassOf(typeof(ExtensionSettingsProvider)))
                    {
                        this.settingProvider = (ExtensionSettingsProvider)Activator.CreateInstance(this.ExtensionAttribute.SettingProviderType, new[] { this });
                        return this.settingProvider;
                    }
                    return null;
                }
                return this.settingProvider;
            }
        }

        /// <summary>
        /// Gets the version of the extension.
        /// </summary>
        /// <value>
        /// The version of the extension.
        /// </value>
        public virtual Version Version => this.GetType().Assembly.GetName().Version;

        #endregion Public Properties

        #region Private Properties

        private ExtensionAttribute ExtensionAttribute
        {
            get
            {
                if (extensionAttribute == null)
                {
                    var currentExtensionType = this.GetType();
                    if (currentExtensionType.IsDefined(typeof(ExtensionAttribute), false))
                    {
                        extensionAttribute = currentExtensionType.GetCustomAttribute<ExtensionAttribute>();
                    }
                }
                
                return extensionAttribute;
            }
        }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Implements the operator !=, which identifies whether the extension instance at the left hand side does not equal to the one at the right hand side.
        /// </summary>
        /// <param name="a">The first extension instance.</param>
        /// <param name="b">The second extension instance.</param>
        /// <returns>
        /// <c>true</c> if the two are not identical, otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(Extension a, Extension b) => !(a == b);

        /// <summary>
        /// Implements the operator ==, which identifies whether the extension instance at the left hand side equals to the one at the right hand side.
        /// </summary>
        /// <param name="a">The first extension instance.</param>
        /// <param name="b">The second extension instance.</param>
        /// <returns>
        /// <c>true</c> if the two are identical, otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(Extension a, Extension b) => Equals(a, b);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            return obj is Extension extension && extension.Id.Equals(Id);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = -286358620;
            hashCode = hashCode * -1521134295 + EqualityComparer<ExtensionAttribute>.Default.GetHashCode(extensionAttribute);
            hashCode = hashCode * -1521134295 + EqualityComparer<ExtensionSettingsProvider>.Default.GetHashCode(settingProvider);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DisplayName);
            hashCode = hashCode * -1521134295 + EqualityComparer<Image>.Default.GetHashCode(Icon);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Manufacturer);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<ExtensionSettingsProvider>.Default.GetHashCode(SettingProvider);
            hashCode = hashCode * -1521134295 + EqualityComparer<Version>.Default.GetHashCode(Version);
            hashCode = hashCode * -1521134295 + EqualityComparer<ExtensionAttribute>.Default.GetHashCode(ExtensionAttribute);
            return hashCode;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => this.Name;

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Gets the settings value of the current extension.
        /// </summary>
        /// <typeparam name="TSettings">The type of the settings.</typeparam>
        /// <returns>The settings configured for the current extension.</returns>
        protected TSettings GetExtensionSetting<TSettings>()
            where TSettings : class, IExtensionSettings
            => this.SettingProvider?.GetExtensionSettings<TSettings>();

        #endregion Protected Methods

    }
}
