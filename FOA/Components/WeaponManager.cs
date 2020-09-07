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
        public float TargetX, TargetY;
        public Weapon Weapon;
        public PolygonCollider WeaponCollider;

        public void Attack(CombatStance stance = CombatStance.Concentrated) {
            if (Weapon == null) return;
            if (Weapon.AttackState == 0)
                Weapon.AttackState = 1;
        }

        public override void Update() {
            base.Update();

            // If attacking, update the weapon
            if (Weapon != null && Weapon.AttackState > 0) {
                Weapon.timer += 1;
                if (Weapon.timer > Weapon.TimeToChangeAttackState) {
                    Weapon.timer = 0;
                    Weapon.AttackState++;
                    if (Weapon.AttackState >= Weapon.Hitboxes.Length)
                        Weapon.AttackState = 0;
                }
                Entity.RemoveCollider(WeaponCollider);
                WeaponCollider = Weapon.CurrentHitbox;
                Entity.AddCollider(WeaponCollider);
                WeaponCollider.Rotation = Util.Angle(Entity.X, Entity.Y, Scene.MouseX, Scene.MouseY);
                WeaponCollider.OriginY = -40;
            }
        }

        public override void Render() {
            base.Render();

            WeaponCollider?.Render(Color.Gold);
        }

        public override void Added() {
            base.Added();
            WeaponCollider = new PolygonCollider(new float[] { 0, 0, 1, 0, 0, 1, });
            //Entity.RemoveCollider(WeaponCollider);
            Entity.AddCollider(WeaponCollider);

            Weapon = new Weapon(new PolygonCollider[] {
                new PolygonCollider(new Polygon(new float[]{ 0,0, 0,0, 0,0,      }), CollisionTags.Combat),
                new PolygonCollider(new Polygon(new float[]{ 10,20, 55,55, 60,0, }), CollisionTags.Combat),
                new PolygonCollider(new Polygon(new float[]{ 10,10, 65,65, 80,0, }), CollisionTags.Combat),
                new PolygonCollider(new Polygon(new float[]{ 10,20, 55,55, 60,0, }), CollisionTags.Combat),
            });
        }
    }

    /// <summary>
    /// Contains hitboxes, timings.
    /// </summary>
    class Weapon {
        /// <summary>
        /// What stage of attack the weapon is in.
        /// </summary>
        public int AttackState = 0;
        /// <summary>
        /// How long it takes when attacking to move through attack states.
        /// </summary>
        public float TimeToChangeAttackState = 10f;
        public float timer;

        /// <summary>
        /// Hitboxes for each attack state.
        /// </summary>
        public PolygonCollider[] Hitboxes;

        public PolygonCollider CurrentHitbox => Hitboxes[AttackState];

        public Weapon(params PolygonCollider[] hitboxes) {
            Hitboxes = hitboxes;
        }
    }
}
