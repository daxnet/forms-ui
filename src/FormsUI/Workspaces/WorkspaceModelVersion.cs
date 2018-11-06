using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    /// <summary>
    /// Represents the version of the workspace model.
    /// </summary>
    public sealed class WorkspaceModelVersion
    {
        public static readonly WorkspaceModelVersion Zero = new WorkspaceModelVersion(0, 0);
        public static readonly WorkspaceModelVersion One = new WorkspaceModelVersion(1, 0);

        private WorkspaceModelVersion() { }

        public WorkspaceModelVersion(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }

        public WorkspaceModelVersion(string versionString)
        {
            var parts = versionString.Split('.');
            if (!int.TryParse(parts[0].Trim(), out var major))
            {
                throw new ArgumentException("Given version string is not in a correct format.", nameof(versionString));
            }

            if (!int.TryParse(parts[1].Trim(), out var minor))
            {
                throw new ArgumentException("Given version string is not in a correct format.", nameof(versionString));
            }

            Major = major;
            Minor = minor;
        }

        public int Major { get; set; }

        public int Minor { get; set; }

        public override string ToString() => $"{Major}.{Minor}";

        public override int GetHashCode() => Major.GetHashCode() ^ Minor.GetHashCode();

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

            if (obj is WorkspaceModelVersion wmv)
            {
                return wmv.Major.Equals(Major) &&
                    wmv.Minor.Equals(Minor);
            }

            return false;
        }

        public static bool operator ==(WorkspaceModelVersion a, WorkspaceModelVersion b) => Equals(a, b);

        public static bool operator !=(WorkspaceModelVersion a, WorkspaceModelVersion b) => !(a == b);

        public static bool operator >(WorkspaceModelVersion a, WorkspaceModelVersion b)
        {
            if (a.Major > b.Major)
            {
                return true;
            }
            else if (a.Major == b.Major)
            {
                return a.Minor > b.Minor;
            }
            else
            {
                return false;
            }
        }

        public static bool operator <(WorkspaceModelVersion a, WorkspaceModelVersion b)
        {
            return a != b && !(a > b);
        }
    }
}
