using System;
using System.Collections.Generic;
using System.Text;

using Otter.Components;
using Otter.Core;

namespace FOA.Components {
    class Body : Component {
        public BodyPart MainPart { get; set; }

        public override void Added() {
            base.Added();
            if (Entity is BodyPart)
                MainPart = (BodyPart)Entity;
        }

        public Body() {

        }

        public class BodyPart : SystemicEntity {
            public BodyPart Parent { get; set; }
            public HashSet<BodyPart> Children { get; }

            public BodyPart(BodyPart parent) {
                Parent = parent;
                Children = new HashSet<BodyPart>();
            }
        }
    }
}
