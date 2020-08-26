using System;
using System.Collections.Generic;
using System.Text;

using Otter.Core;
using Otter.Colliders;
using Otter.Components;
using Otter.Utility;

namespace FOA {
    /// <summary>
    /// An area in the world which influences the Systemic Variables of Systemic Entities within it.
    /// </summary>
    class SystemicField : Entity {
        public string VariableAffected { get; }
        public float AmountPerFrame { get; }

        public override void Update() {
            base.Update();

            foreach (SystemicEntity entity in Collider.CollideEntities<SystemicEntity>(X, Y, CollisionTags.SystemicAreaOfEffect))
                entity.State.ModifyValue(VariableAffected, AmountPerFrame);
        }

        public override void Render() {
            base.Render();

            Collider.Render(Otter.Graphics.Color.Red);
        }

        SystemicField(string affects, float amountPerFrame, float lifetime) {
            VariableAffected = affects;
            AmountPerFrame = amountPerFrame;

            // 0 means unlimited lifetime.
            if (lifetime > 0)
                LifeSpan = lifetime;
        }

        /// <summary>
        /// A circular field.
        /// </summary>
        public SystemicField(string affects, float amountPerFrame, float lifetime, float radius) : this(affects, amountPerFrame, lifetime) {
            Collider circle = new CircleCollider((int)radius, (int)CollisionTags.SystemicAreaOfEffect);
            circle.CenterOrigin();
            AddCollider(circle);
        }

        /// <summary>
        /// A field with a specified collider.
        /// </summary>
        public SystemicField(string affects, float amountPerFrame, float lifetime, Collider collider) : this(affects, amountPerFrame, lifetime) {
            collider.CenterOrigin();
            AddCollider(collider);
        }

        [OtterCommand(group: "systemic", helpText: "Create a systemic area of effect at a position with a radius.")]
        static void CreateAreaOfEffect(float x, float y, string affects, float amountPerFrame, float lifetime, float radius) {
            SystemicField field = new SystemicField(affects, amountPerFrame, lifetime, radius) { X = x, Y = y };
            Scene.Instance.Add(field);
        }
    }
}
