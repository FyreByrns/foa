using Otter.Colliders;
using Otter.Graphics.Drawables;
using Otter.Components;

namespace FOA.Components {
    class Rotater : Component {
        public float Rotation { get; set; }

        Graphic graphic;
        PolygonCollider collider;

        public override void UpdateFirst() {
            base.UpdateFirst();

            // attempt to attach a graphic
            if (graphic == null)
                graphic = Entity.Graphic;

            // attempt to attach a polygon collider
            if (collider == null)
                collider = Entity.Collider as PolygonCollider;
        }

        public override void Update() {
            base.Update();

            // apply rotation to graphic
            if (graphic != null)
                graphic.Transform.Rotation = Rotation;

            // apply rotation to collider
            if (collider != null)
                collider.Rotation = Rotation;
        }
    }
}
