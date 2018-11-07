using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsUI
{
    /// <summary>
    /// Provides a scoped context that updates the UI cursor, in which the long-running operation will be executed.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public sealed class LengthyOperation : IDisposable
    {
        #region Private Fields

        private readonly Control parent;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LengthyOperation"/> class.
        /// </summary>
        /// <param name="parent">The parent control on which the long-running operation is going to execute.</param>
        public LengthyOperation(Control parent)
        {
            this.parent = parent;
            parent.Cursor = Cursors.WaitCursor;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            parent.Cursor = Cursors.Default;
        }

        #endregion Public Methods
    }
}
