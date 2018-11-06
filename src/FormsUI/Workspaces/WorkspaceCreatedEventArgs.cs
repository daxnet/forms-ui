using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    public sealed class WorkspaceCreatedEventArgs<TModel> : EventArgs
        where TModel : INotifyPropertyChanged
    {
        public WorkspaceCreatedEventArgs(TModel model)
        {
            this.Model = model;
        }

        public TModel Model { get; }
    }
}
