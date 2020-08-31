using Otter.Components;
using System.Collections.Generic;

namespace FOA.Components {
    /// <summary>
    /// Component attached to entities which have inventories.
    /// </summary>
    class Inventory : Component {
        /// <summary>
        /// Get the items in this inventory.
        /// </summary>
        public IEnumerable<Item> GetItems() {
            foreach (Item item in Entity.GetComponents<Item>())
                yield return item;
        }
    }
}
