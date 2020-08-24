using System;
using System.Collections.Generic;
using System.Text;

namespace FOA {
    class SystemicObject {
        /// <summary>
        /// Amounts of properties.
        /// </summary>
        public Dictionary<MaterialProperties, float> Properties { get; }

        /// <summary>
        /// Whether the object has a certain property.
        /// </summary>
        public bool HasProperty(MaterialProperties property)
            => Properties.ContainsKey(property);

        /// <summary>
        /// Apply a material profile to the object.
        /// </summary>
        public void ApplyMaterialProfile(MaterialProfile profile) {
            foreach (var pair in profile.DefaultValues)
                Properties[pair.Key] = pair.Value;
        }

        /// <summary>
        /// Default constructor. Initializes variables and does nothing else.
        /// </summary>
        public SystemicObject() {
            Properties = new Dictionary<MaterialProperties, float>();
        }
    }
}
