using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI.Workspaces
{
    public sealed class WorkspaceOpenedEventArgs<TModel> : WorkspaceEventArgs<TModel>
        where TModel : INotifyPropertyChanged
    {
        public WorkspaceOpenedEventArgs(string fileName, TModel model)
            : base(fileName, model)
        {
        }
    }
}
