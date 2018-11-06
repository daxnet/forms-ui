
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FormsUI.Extensions
{
    /// <summary>
    /// Represents the Extension Manager that registers and manages the extensions.
    /// </summary>
    public sealed class ExtensionManager : ExternalResourceManager<Extension>
    {
        private const string ExtensionsFolder = "extensions";

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionManager"/> class.
        /// </summary>
        /// <param name="path">The path which contains the extension assemblies.</param>
        public ExtensionManager(string path)
            : base(path, "*.dll")
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionManager"/> class.
        /// </summary>
        /// <param name="searchFromExtensionsFolder">The boolean value indicates whether the searching of the extensions should
        /// be based on the extensions sub folder.</param>
        public ExtensionManager(bool searchFromExtensionsFolder = true)
            : this(searchFromExtensionsFolder ? Path.Combine(Application.StartupPath, ExtensionsFolder) : Application.StartupPath)
        {

        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets all of the registered extensions.
        /// </summary>
        /// <value>
        /// All extensions.
        /// </value>
        public IEnumerable<KeyValuePair<Guid, Extension>> AllExtensions
        {
            get
            {
                return this.Resources;
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets all the registered extensions with the specified extension type.
        /// </summary>
        /// <typeparam name="TExtension">The type of the extension to be retrieved</typeparam>
        /// <returns>A list of registered extensions that have the same type.</returns>
        public IEnumerable<TExtension> GetExtensions<TExtension>()
            where TExtension : Extension => Resources.Values.Where(t => t.GetType().IsSubclassOf(typeof(TExtension))).Select(p => (TExtension)p);

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Loads the resources from the given file.
        /// </summary>
        /// <param name="fileName">The name of the file from which the resources are loaded.</param>
        /// <returns>A list of loaded extensions.</returns>
        protected override IEnumerable<Extension> LoadResources(string fileName)
        {
            var assembly = Assembly.LoadFile(fileName);
            var result = new List<Extension>();
            foreach (var type in assembly.GetExportedTypes())
            {
                if (type.IsDefined(typeof (ExtensionAttribute)) &&
                    type.IsSubclassOf(typeof (Extension)))
                {
                    try
                    {
                        var extensionLoaded = (Extension) Activator.CreateInstance(type);
                        this.OnResourceLoaded(extensionLoaded);
                        result.Add(extensionLoaded);
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }

        #endregion Protected Methods
    }
}
