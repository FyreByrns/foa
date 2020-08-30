using System;
using System.Collections.Generic;
using System.Text;

using Otter.Components;
using Otter.Core;

namespace FOA.Components {
    /// <summary>
    /// Component attached to entities which have child body parts.
    /// </summary>
    class BodyPartOwner : Component {
        /// <summary>
        /// Get a body part by name.
        /// </summary>
        public Entity GetPartByName(string name) {
            foreach (BodyPart part in Entity.GetComponents<BodyPart>())
                if (part.Name == name)
                    return part.Entity;
            return null;
        }
    }
}
