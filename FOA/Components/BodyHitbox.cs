using System;
using System.Collections.Generic;
using System.Text;

using Otter.Graphics;
using Otter.Colliders;
using Otter.Components;
using Otter.Utility;

namespace FOA.Components {
    /// <summary>
    /// Simple hitbox for a humanoid body.
    /// </summary>
    class BodyHitbox : Component{
        public PolygonCollider
            LeftSide,
            RightSide,
            Middle;

        public override void Added() {
            base.Added();

            LeftSide = new PolygonCollider(Polygon.CreateCircle(20), CollisionTags.Combat);
            RightSide = new PolygonCollider(Polygon.CreateCircle(20), CollisionTags.Combat);
            Middle = new PolygonCollider(Polygon.CreateCircle(30), CollisionTags.Combat);

            LeftSide.CenterOrigin();
            RightSide.CenterOrigin();
            Middle.CenterOrigin();

            LeftSide.OriginY += 40;
            RightSide.OriginY -= 40;

            Entity.AddCollider(LeftSide);
            Entity.AddCollider(RightSide);
            Entity.AddCollider(Middle);
        }

        /// <summary>
        /// Debug so I can tell which way things are facing.
        /// </summary>
        Color GetColour(Collider c) {
            if (c == LeftSide)
                return Color.Green;
            if (c == RightSide)
                return Color.Orange;
            if (c == Middle)
                return Color.Magenta;

            return Color.Red;
        }

        public override void Render() {
            base.Render();

            foreach (Collider collider in Colliders)
                collider.Render(GetColour(collider));
        }

        public BodyHitbox() {

        }
    }
}
