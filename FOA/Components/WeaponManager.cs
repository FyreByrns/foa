using Otter.Colliders;
using Otter.Components;
using Otter.Graphics;
using Otter.Graphics.Drawables;
using Otter.Utility;
using Otter.Utility.MonoGame;
using System;
using System.Collections.Generic;
using System.Text;

namespace FOA.Components {
    class WeaponManager : Component {
        Image Crosshair;
        public Weapon RightHand;
        public float TargetX, TargetY;
        public bool Attacking;

        float attackTimer;

        public void Attack() {
            if(!Attacking)
            Attacking = true;
        }

        public override void Added() {
            base.Added();

            RightHand = new Weapon() {
                Reach = 100,
                Shape = new PolygonCollider(new Polygon(new float[]
            {
                0, -10,
                100, -10,
                110, 0,
                100, 10,
                0, 10
            })),
                RestingAngleModifier = -20,
                HitMovementR = 10,
                HitTime = 10,
            };

            RightHand.Shape.OriginY = GetComponent<BodyHitbox>().RightSide.OriginY*2;

            Entity.AddCollider(RightHand.Shape);

            Crosshair = Image.CreateCircle(5, Color.Gold);
            Entity.AddGraphic(Crosshair);
        }

        public override void Render() {
            base.Render();

            RightHand.Shape.Render(Color.White);
        }

        public override void Update() {
            base.Update();

            Crosshair.X = 100;
            Crosshair.Y = 0;

            float angle = Util.Angle(Entity.X, Entity.Y, TargetX, TargetY);

            Vector2 loc = Util.Rotate(Crosshair.X, Crosshair.Y, angle);
            Crosshair.X = loc.X;
            Crosshair.Y = loc.Y;


            if (Attacking) {
                attackTimer += Scene.Game.DeltaTime;

                if (attackTimer > RightHand.HitTime) {
                    Attacking = false;
                    attackTimer = 0;
                }
                else {
                    RightHand.Shape.Rotation = angle + attackTimer * RightHand.HitMovementR + RightHand.RestingAngleModifier;
                }
            }
            else {
                RightHand.Shape.Rotation = angle + RightHand.RestingAngleModifier;
            }
        }
    }

    class Weapon {
        public float Reach;
        public float RestingAngleModifier;
        public float HitTime;
        public float HitMovementX, HitMovementY, HitMovementR;
        public PolygonCollider Shape;
    }
}
