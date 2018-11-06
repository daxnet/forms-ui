using FormsUI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    public class MostRecentlyUsedItem
    {
        private MostRecentlyUsedItem() { }

        public MostRecentlyUsedItem(string name, string path)
        {
            this.Name = name;
            this.Path = path;
            this.DateCreated = DateTime.UtcNow;
        }


        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Path { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
