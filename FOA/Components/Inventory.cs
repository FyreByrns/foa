using Otter.Components;
using System.Collections.Generic;

namespace FOA.Components {
    /// <summary>
    /// Component attached to entities which have inventories.
    /// </summary>
    class Inventory : Component {
        public HashSet<Item> Items { get; }

        /// <summary>
        /// Add an item by name with a count.
        /// </summary>
        public void AddItem(string name, int count = 1) {
            Items.Add(new Item(name, count));
        }
    }
}
