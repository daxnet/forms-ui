using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    public sealed class MostRecentlyUsedManager
    {
        private const string FileName = "sldimport.mru.json";

        private static readonly string FullName = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Signals Lead Discovery"), FileName);

        private List<MostRecentlyUsedItem> items = new List<MostRecentlyUsedItem>();

        public MostRecentlyUsedManager()
        {
            // Reload();
        }

        public void Reload()
        {
            try
            {
                if (File.Exists(FullName))
                {
                    items = JsonConvert.DeserializeObject<List<MostRecentlyUsedItem>>(File.ReadAllText(FullName));
                }
            }
            catch
            {
            }
        }

        public void Register(MostRecentlyUsedItem item)
        {
            if (items != null)
            {
                var foundItem = items.FirstOrDefault(x => x.Path.Replace('/', '\\').ToUpper().Equals(item.Path.Replace('/', '\\').ToUpper()));
                if (foundItem == null)
                {
                    this.items.Add(item);
                }
                else
                {
                    foundItem.DateCreated = DateTime.UtcNow;
                }
            }
        }

        public IEnumerable<MostRecentlyUsedItem> Items => items;

        public void Save()
        {
            var directoryName = Path.GetDirectoryName(FullName);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            var json = JsonConvert.SerializeObject(items);
            File.WriteAllText(FullName, json);
        }
    }
}
