using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    public class WorkspaceEventArgs<TModel> : EventArgs
        where TModel : INotifyPropertyChanged
    {
        public WorkspaceEventArgs(string fileName, TModel model)
        {
            this.FileName = fileName;
            this.Model = model;
        }

        public string FileName { get; }

        public TModel Model { get; }
    }
}
