using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Examples.DockableWindows.Models
{
    public class Note : PropertyChangedNotifier
    {
        private string title;
        private string content;

        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string Content
        {
            get => content;
            set
            {
                if (content != value)
                {
                    content = value;
                    OnPropertyChanged(nameof(Content));
                }
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Note note &&
                   title == note.title &&
                   content == note.content;
        }

        public override int GetHashCode()
        {
            var hashCode = -1490960589;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(content);
            return hashCode;
        }
    }
}
