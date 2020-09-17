using Otter.Colliders;
using Otter.Components;
using Otter.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FOA.Components {
    class MovingEntityCollider : Component {
        public new Collider Collider { get; set; }
        float lastX, lastY;


        public override void Update() {
            base.Update();

            float x = Entity.X;
            float y = Entity.Y;

            List<Collider> c = Collider.CollideList(x, y, CollisionTags.Wall);
            while (c.Count != 1) {
                float diffX = x - lastX;
                float diffY = y - lastY;

                int dirX = Math.Sign(diffX);
                int dirY = Math.Sign(diffY);

                foreach (Collider collider in c) {
                    if (collider == Collider) continue;

                    while (Collider.Collide(x + dirX, y + dirY, collider) != null) {
                        x -= dirX;
                        y -= dirY;
                    }
                }
                c = Collider.CollideList(x, y, CollisionTags.Wall);
            }

            Entity.X = x;
            Entity.Y = y;
            lastX = x;
            lastY = y;
        }

        public override void Render() {
            base.Render();

            // (W)hite for (W)all
            Collider.Render(Color.White);
        }

        public override void Added() {
            base.Added();
            Entity.AddCollider(Collider);
        }

        public MovingEntityCollider(Collider collider) {
            Collider = collider;
            Collider.CenterOrigin();
        }
    }
}
