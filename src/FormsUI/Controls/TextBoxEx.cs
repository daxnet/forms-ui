using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsUI.Controls
{
    public class TextBoxEx : TextBox
    {

        public event EventHandler<EventArgs> ShowCueChanged;

        private bool showCue;

        [Category("Appearance")]
        [Description("Specifies whether the cue text (watermark) should be shown on the text box.")]
        public bool ShowCue
        {
            get
            {
                return showCue;
            }
            set
            {
                if (showCue != value)
                {
                    showCue = value;
                    OnShowCueChanged(EventArgs.Empty);
                }
            }
        }

        private string cue;


        protected virtual void OnShowCueChanged(EventArgs e)
        {
            ShowCueChanged?.Invoke(this, e);
        }
    }
}
