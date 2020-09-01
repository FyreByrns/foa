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
        public void AddItem(string name, int count = 1, MaterialProfile profile = null) {
            if (count == 0) return;

            Item item = new Item(name, count);
            Items.Add(item);

            if (profile != null)
                item.ApplyMaterialProfile(profile);
        }
    }
}
