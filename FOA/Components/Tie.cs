using Otter.Components;
using Otter.Core;

namespace FOA.Components {
    /// <summary>
    /// Ties one entity to another, with an offset.
    /// </summary>
    class Tie : Component {
        public Entity TiedTo { get; }
        public float OffsetX { get; set; }
        public float OffsetY { get; set; }

        public override void Update() {
            base.Update();
            Entity.X = TiedTo.X + OffsetX;
            Entity.Y = TiedTo.Y + OffsetY;
        }

        public Tie(Entity tiedTo, float offsetX = 0, float offsetY = 0) {
            TiedTo = tiedTo;
            OffsetX = offsetX;
            OffsetY = offsetY;
        }
    }
}
