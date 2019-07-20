using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsUI.Windows
{
    public class ToolAction : Component
    {

        #region Protected Fields

        protected readonly List<ToolStripItem> toolStrips = new List<ToolStripItem>();

        #endregion Protected Fields

        #region Private Fields

        private readonly Control parent;
        private bool @checked;
        private Image image;
        private bool disposed;
        private bool enabled;
        private Keys? shortcutKeys;
        private object tag;
        private string text;
        private string tooltipText;
        private bool visible;

        #endregion Private Fields

        #region Public Constructors

        protected internal ToolAction(string id,
            Control parent,
            string text,
            IEnumerable<ToolStripItem> associatedToolStrips,
            Action<ToolAction> clickAction = null,
            bool enabled = true,
            bool visible = true,
            string tooltipText = null,
            Image image = null,
            object tag = null,
            Keys? shortcutKeys = null)
        {
            Id = id;
            this.parent = parent;
            this.ClickAction = clickAction;
            toolStrips.AddRange(associatedToolStrips);
            toolStrips.ForEach(ts => ts.Click += ExecuteClickHandler);

            Text = text;
            Enabled = enabled;
            Visible = visible;
            if (image != null)
            {
                Image = image;
            }

            if (!string.IsNullOrEmpty(tooltipText))
            {
                TooltipText = tooltipText;
            }

            if (tag != null)
            {
                Tag = tag;
            }

            if (shortcutKeys != null)
            {
                ShortcutKeys = shortcutKeys;
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public bool Checked
        {
            get => @checked;
            set
            {
                @checked = value;
                toolStrips.ForEach(ts =>
                {
                    switch (ts)
                    {
                        case ToolStripMenuItem tsmi:
                            tsmi.Checked = value;
                            break;
                        case ToolStripButton tsb:
                            tsb.Checked = value;
                            break;
                    }
                });
            }
        }

        public Action<ToolAction> ClickAction { get; set; }

        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                toolStrips.ForEach(ts => ts.Enabled = value);
            }
        }

        public string Id { get; }

        public Keys? ShortcutKeys
        {
            get => shortcutKeys;
            set
            {
                if (shortcutKeys != value && value.HasValue)
                {
                    this.toolStrips.Where(t => t is ToolStripMenuItem)
                        .Select(t => t as ToolStripMenuItem)
                        .ToList()
                        .ForEach(t => t.ShortcutKeys = value ?? Keys.None);

                    shortcutKeys = value;
                }
                else
                {
                    this.toolStrips.Where(t => t is ToolStripMenuItem)
                        .Select(t => t as ToolStripMenuItem)
                        .ToList()
                        .ForEach(t => t.ShortcutKeys = Keys.None);
                }
            }
        }

        public object Tag
        {
            get => tag;
            set
            {
                tag = value;
                toolStrips.ForEach(ts => ts.Tag = value);
            }
        }

        public string Text
        {
            get => text;
            set
            {
                text = value;
                toolStrips.ForEach(ts => ts.Text = value);
            }
        }

        public string TooltipText
        {
            get => tooltipText;
            set
            {
                tooltipText = value;
                toolStrips.ForEach(ts => ts.ToolTipText = value);
            }
        }

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                toolStrips.ForEach(ts => ts.Visible = value);
            }
        }

        public Image Image
        {
            get => image;
            set
            {
                image = value;
                toolStrips.ForEach(ts => ts.Image = value);
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override bool Equals(object obj)
        {
            return obj is ToolAction action &&
                   Id == action.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<string>.Default.GetHashCode(Id);
        }

        public void Invoke() => ExecuteClickHandler(this, EventArgs.Empty);

        public override string ToString() => Id;

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.toolStrips.ForEach(ts => ts.Click -= ExecuteClickHandler);
                }

                disposed = true;
            }

            base.Dispose(disposing);
        }

        #endregion Protected Methods

        #region Private Methods

        private void ExecuteClickHandler(object sender, EventArgs e)
        {
            using (new LengthyOperation(parent))
            {
                this.ClickAction?.Invoke(this);
            }
        }

        #endregion Private Methods

    }
}
