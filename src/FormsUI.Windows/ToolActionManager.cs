using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsUI.Windows
{
    public sealed class ToolActionManager : ComponentManager<ToolAction>
    {
        public ToolActionManager() { }

        public ToolActionManager(IEnumerable<ToolAction> toolActions)
            : base(toolActions)
        { }

        public ToolAction this[string id] => Get(id);

        public ToolAction Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            var toolAction = components.FirstOrDefault(c => string.Equals(c.Id, id));
            if (toolAction == null)
            {
                throw new InvalidOperationException("The action component has not been defined.");
            }

            return toolAction;
        }
    }
}
