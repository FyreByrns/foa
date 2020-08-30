using Otter.Components;
using Otter.Core;

namespace FOA.Components {
    /// <summary>
    /// Component attached to entities which are body parts.
    /// </summary>
    class BodyPart : Component {
        public string Name { get; }
        public Entity Part { get; }

        public override void Added() {
            base.Added();

            // Can't be added to an entity which does not own body parts.
            if (Entity.GetComponent<BodyPartOwner>() == null)
                RemoveSelf();
        }

        public BodyPart(Entity part, string name) {
            Part = part;
            Name = name;
        }
    }
}
