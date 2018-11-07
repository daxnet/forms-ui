using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI
{
    /// <summary>
    /// Provides the utility methods for internal use.
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// Determines whether the user has the write permission to the specified directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns>
        ///   <c>true</c> if the user has the write permission to the specified directory; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasWritePermission(string directoryName)
        {
            var permissionSet = new PermissionSet(PermissionState.None);
            var writePermission = new FileIOPermission(FileIOPermissionAccess.Write, directoryName);
            permissionSet.AddPermission(writePermission);
            return permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet);
        }
    }
}
