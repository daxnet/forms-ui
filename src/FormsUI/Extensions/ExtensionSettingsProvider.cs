
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace FormsUI.Extensions
{
    /// <summary>
    /// Represents that the derived classes are the providers which provides the settings capabilities for the extensions.
    /// </summary>
    public abstract class ExtensionSettingsProvider
    {

        #region Private Fields

        private const string SettingsFolder = "settings";
        private readonly Extension extension;
        private UserControl settingsControl;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes the <see cref="ExtensionSettingsProvider"/> class.
        /// </summary>
        static ExtensionSettingsProvider()
        {
            ExtensionSettingsPath = Path.Combine(Application.StartupPath, SettingsFolder);

            // If no write permission on the current settings folder, then use the AppData to store
            // the settings files.
            if (!Utils.HasWritePermission(ExtensionSettingsPath))
            {
                var applicationName = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
                var settingsPathRelative = Path.Combine(applicationName, SettingsFolder);
                ExtensionSettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), settingsPathRelative);
            }

            if (!Directory.Exists(ExtensionSettingsPath))
            {
                Directory.CreateDirectory(ExtensionSettingsPath);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionSettingsProvider"/> class.
        /// </summary>
        /// <param name="extension">The extension to which the current <c>ExtensionSettingProvider</c> serves.</param>
        public ExtensionSettingsProvider(Extension extension)
        {
            this.extension = extension;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the path in which the extension settings are stored.
        /// </summary>
        /// <value>
        /// The settings path.
        /// </value>
        public static string ExtensionSettingsPath { get; private set; }

        ///// <summary>
        ///// Gets the setting collected from the <see cref="UserControl"/>.
        ///// </summary>
        ///// <value>
        ///// The collected setting.
        ///// </value>
        //public IExtensionSettings CollectedSetting
        //{
        //    get
        //    {
        //        return this.DoCollectSettingsFromControl();
        //    }
        //}

        /// <summary>
        /// Gets the settings of the current extension.
        /// </summary>
        /// <value>
        /// The settings of the extension.
        /// </value>
        public IExtensionSettings ExtensionSettings
        {
            get
            {
                var setting = ReadSettings(this.extension, this.ExtensionSettingsType);
                if (setting == null)
                    setting = this.DefaultSettings;
                else
                    setting = FixSetting(setting);
                return setting;
            }
        }

        /// <summary>
        /// Gets the settings <see cref="UserControl"/> which provides the user interface for user to manipulate the settings.
        /// </summary>
        /// <value>
        /// The settings <see cref="UserControl"/>.
        /// </value>
        public UserControl SettingControl
        {
            get
            {
                if (settingsControl == null)
                {
                    if (this.SettingsControlType == null)
                    {
                        throw new ArgumentNullException("SettingControlType");
                    }

                    if (!this.SettingsControlType.IsSubclassOf(typeof(UserControl)))
                    {
                        throw new InvalidOperationException("");
                    }

                    settingsControl = (UserControl)Activator.CreateInstance(this.SettingsControlType);
                    settingsControl.Tag = this.extension;
                    settingsControl.Dock = DockStyle.Fill;
                    return settingsControl;

                }

                return settingsControl;
            }
        }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Gets the default settings of the current extension.
        /// </summary>
        /// <value>
        /// The default settings of the current extension.
        /// </value>
        protected abstract IExtensionSettings DefaultSettings { get; }

        /// <summary>
        /// Gets the <see cref="Type"/> of the extension settings data.
        /// </summary>
        /// <value>
        /// The <see cref="Type"/> of the extension settings data.
        /// </value>
        /// <remarks>The type returned by this property must implement the <see cref="IExtensionSettings"/> interface.</remarks>
        protected abstract Type ExtensionSettingsType { get; }

        /// <summary>
        /// Gets the <see cref="Type"/> of the settings user interface.
        /// </summary>
        /// <value>
        /// The <see cref="Type"/> of the settings user interface.
        /// </value>
        /// <remarks>The type returned by this property must be derived from <see cref="UserControl"/> type.</remarks>
        protected abstract Type SettingsControlType { get; }

        #endregion Protected Properties

        ///// <summary>
        ///// Binds the setting to the current settings <see cref="UserControl"/>.
        ///// </summary>
        //public void BindSetting(IExtensionSettings setting)
        //{
        //    this.BindSettingsToControl(setting);
        //}

        #region Public Methods

        /// <summary>
        /// Gets the strongly typed settings of current extension.
        /// </summary>
        /// <typeparam name="T">The type of settings.</typeparam>
        /// <returns>The strongly typed settings of current extension.</returns>
        public T GetExtensionSettings<T>()
            where T : IExtensionSettings
        {
            return (T)this.ExtensionSettings;
        }

        /// <summary>
        /// Gets the strongly typed settings user interface instance.
        /// </summary>
        /// <typeparam name="T">The type of settings user interface which should be derived from the <see cref="UserControl"/> class.</typeparam>
        /// <returns>The strongly typed settings user interface instance.</returns>
        public T GetSettingControl<T>()
            where T : UserControl
        {
            return (T)this.SettingControl;
        }

        /// <summary>
        /// Collects the settings from current user interface and then persist the settings data to the disk.
        /// </summary>
        public void PersistSettings()
        {
            WriteSettings(this.extension, this.CollectSettingsFromControl());
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Binds the extension settings represented by <paramref name="settings"/> parameter to
        /// the current settings user interface.
        /// </summary>
        /// <param name="settings">The settings data that needs to be bound to the current user interface.</param>
        protected abstract void BindSettingsToControl(IExtensionSettings settings);

        /// <summary>
        /// Collects the extension settings from the current settings user interface.
        /// </summary>
        /// <returns>The settings data collected from the current user interface.</returns>
        protected abstract IExtensionSettings CollectSettingsFromControl();

        /// <summary>
        /// Fixes the settings read from the persisted settings file.
        /// </summary>
        /// <param name="settings">The settings to be fixed.</param>
        /// <returns>The fixed settings.</returns>
        protected virtual IExtensionSettings FixSetting(IExtensionSettings settings) => settings;

        #endregion Protected Methods

        #region Private Methods

        private static string GetExtensionSettingsFileName(Extension extension) => Path.Combine(ExtensionSettingsPath, string.Format("{0}.setting.json", extension.Id.ToString().Replace('-', '_').ToUpper()));

        private static IExtensionSettings ReadSettings(Extension extension, Type settingsType)
        {
            if (!typeof(IExtensionSettings).IsAssignableFrom(settingsType))
            {
                throw new ExtensionException($"The specified type '{settingsType}' does not implement the IExtensionSettings interface.");
            }

            var settingsFile = GetExtensionSettingsFileName(extension);
            if (!File.Exists(settingsFile))
            {
                return null;
            }

            var settingsJson = File.ReadAllText(settingsFile);
            return (IExtensionSettings)JsonConvert.DeserializeObject(settingsJson, settingsType);
        }

        private static void WriteSettings(Extension extension, object settings)
        {
            var settingsFile = GetExtensionSettingsFileName(extension);
            var settingsJson = JsonConvert.SerializeObject(settings);
            File.WriteAllText(settingsFile, settingsJson);
        }

        #endregion Private Methods

    }
}
