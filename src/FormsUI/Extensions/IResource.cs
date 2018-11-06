
using System;

namespace FormsUI.Extensions
{
    /// <summary>
    /// Represents that the implemented classes are the resources.
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// Gets or sets the identifier of the resource.
        /// </summary>
        /// <value>
        /// The identifier of the resource.
        /// </value>
        Guid Id { get; set; }
    }
}
