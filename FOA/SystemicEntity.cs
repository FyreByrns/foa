using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Otter.Core;

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

        public SystemicEntity() {
            State = new SystemicEntityState();
            Properties = new Dictionary<string, SystemicProperty>();
        }
    }
}
