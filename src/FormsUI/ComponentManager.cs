using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsUI
{
    /// <summary>
    /// Represents the component managers that manage component lifetime.
    /// </summary>
    /// <typeparam name="TComponent">The type of the component.</typeparam>
    public abstract class ComponentManager<TComponent> : Component, ICollection<TComponent>
        where TComponent : class, IComponent
    {
        #region Protected Fields

        protected readonly List<TComponent> components = new List<TComponent>();

        #endregion Protected Fields

        #region Private Fields

        private bool disposed = false;

        #endregion Private Fields

        #region Protected Constructors

        protected ComponentManager() { }

        protected ComponentManager(IEnumerable<TComponent> components) => this.components.AddRange(components);

        #endregion Protected Constructors

        #region Public Properties

        public int Count => components.Count;

        public bool IsReadOnly => false;

        #endregion Public Properties

        #region Public Methods

        public void Add(TComponent item) => components.Add(item);

        public void Clear() => components.Clear();

        public bool Contains(TComponent item) => components.Contains(item);

        public void CopyTo(TComponent[] array, int arrayIndex) => components.CopyTo(array, arrayIndex);

        public IEnumerator<TComponent> GetEnumerator() => components.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => components.GetEnumerator();

        public bool Remove(TComponent item) => components.Remove(item);

        #endregion Public Methods

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    foreach (var item in components)
                    {
                        item.Dispose();
                    }

                    components.Clear();
                }

                disposed = true;
            }
        }

        #endregion Protected Methods

    }
}
