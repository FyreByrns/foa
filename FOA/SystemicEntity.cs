using System;
using System.Collections.Generic;
using System.Text;

using Otter.Core;
using Otter.Utility;
using Otter.Colliders;
using Otter.Graphics.Drawables;
using Otter.Graphics.Text;
using Otter.Graphics;

namespace FOA {
    class SystemicEntity : Entity {
        public SystemicEntityState State { get; private set; }
        public Dictionary<string, SystemicProperty> Properties;

        /// <summary>
        /// Whether the entity has a property with a certain name.
        /// </summary>
        public bool HasProperty(string propertyName)
            => Properties.ContainsKey(propertyName);

        /// <summary>
        /// Whether a property is active on the entity.
        /// </summary>
        public bool PropertyActive(string propertyName) {
            if (!HasProperty(propertyName))
                return false;
            return Properties[propertyName].Active;
        }

        /// <summary>
        /// Apply a material profile to the object.
        /// </summary>
        public void ApplyMaterialProfile(MaterialProfile profile) {
            foreach (SystemicProperty property in profile.Properties)
                Properties[property.Name] = property;
        }

        /// <summary>
        /// Set the graphic of the Entity.
        /// </summary>
        public new void SetGraphic(Graphic graphic) {
            Graphic = graphic;
            Graphic.CenterOrigin();
        }

        /// <summary>
        /// Set the collider of the Entity.
        /// </summary>
        public void SetCollider(PolygonCollider collider) {
            Collider = collider;
            Collider.CenterOrigin();
        }

        List<Text> debug = new List<Text>();
        public bool Debug;

        public override void Update() {
            base.Update();

            if (Debug) {
                foreach (Text t in debug)
                    RemoveGraphic(t);
                debug.Clear();

                foreach (string s in Properties.Keys) {
                    Text prop = new Text($"{s}:{Properties[s].Active}", 16) { Color = Color.Black, OutlineColor = Color.White, OutlineThickness = 3 };
                    prop.Y += debug.Count * 18;
                    debug.Add(prop);

                    AddGraphic(prop);
                }

                foreach (SystemicVariable variable in State.Variables) {
                    Text var = new Text($"{variable.Name}:{variable.Value}", 16) { Color = Color.Black, OutlineColor = Color.White, OutlineThickness = 3 };
                    var.Y += debug.Count * 18;
                    debug.Add(var);

                    AddGraphic(var);
                }
            }
        }

        public SystemicEntity() {
            State = new SystemicEntityState();
            Properties = new Dictionary<string, SystemicProperty>();

            SetGraphic(Image.CreateCircle(10));
            SetCollider(new PolygonCollider(Polygon.CreateCircle(10), CollisionTags.SystemicAreaOfEffect));
        }
    }
}
