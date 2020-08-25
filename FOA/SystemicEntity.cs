using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

using Otter.Core;

namespace FOA {
    class SystemicEntity : Entity {
        public HashSet<Property> Properties { get; private set; }

        /// <summary>
        /// Whether the entity has a property with a certain name.
        /// </summary>
        public bool HasProperty(string propertyName) {
            foreach (Property property in Properties)
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
            Properties = new HashSet<Property>();
        }
    }
}
