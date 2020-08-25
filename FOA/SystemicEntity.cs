using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Otter.Core;

namespace FOA {
    class SystemicEntity : Entity {
        public SystemicEntityState State { get; private set; }
        public HashSet<SystemicProperty> Properties { get; private set; }

        /// <summary>
        /// Whether the entity has a property with a certain name.
        /// </summary>
        public bool HasProperty(string propertyName) {
            foreach (SystemicProperty property in Properties)
                if (propertyName == property.Name)
                    return true;
            return false;
        }

        /// <summary>
        /// Apply a material profile to the object.
        /// </summary>
        public void ApplyMaterialProfile(MaterialProfile profile) {
            Properties = profile.Properties;
        }

        public SystemicEntity() {
            State = new SystemicEntityState();
            Properties = new HashSet<SystemicProperty>();
        }
    }
}
