using Otter.Components;
using Otter.Core;

namespace FOA.Components {
    /// <summary>
    /// Component attached to entities which are items.
    /// </summary>
    class Item : Component {
        public string Name { get; }
        public Entity Item { get; }

        public override void Added() {
            base.Added();

            // Can't be added to an entity which does not have an inventory.
            if (Entity.GetComponent<Inventory>() == null)
                RemoveSelf();
        }

        public Item(Entity item, string name) {
            Item = item;
            Name = name;
        }
    }
}
