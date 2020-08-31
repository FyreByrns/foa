using Otter.Components;
using Otter.Core;
using System;
using System.Collections.Generic;

namespace FOA.Components {
    /// <summary>
    /// Component attached to entities which have bodies.
    /// </summary>
    class Body : Component {
        /// <summary>
        /// Starting point of the body. No parents.
        /// </summary>
        public BodyPart Base { get; private set; }
        public List<BodyPart> AllParts { get; }

        /// <summary>
        /// Get part by name in the body.
        /// </summary>
        public BodyPart GetPart(string name) {
            return Base.GetPart(name);
        }

        /// <summary>
        /// Apply a material profile to all parts in the body.
        /// </summary>
        public void ApplyMaterialProfile(MaterialProfile profile) {
            foreach (BodyPart part in AllParts)
                part.ApplyMaterialProfile(profile);
        }

        #region     Body Makers

        /// <summary>
        /// Make a humanoid body.
        /// </summary>
        public static Body MakeHumanoid(int fingers = 5, int toes = 5, MaterialProfile profile = null) {
            Body body = new Body();

            var head = body.Base = BodyPart.MakeHead();
            var neck = head.AddChild(BodyPart.MakeNeck());
            var torso = neck.AddChild(BodyPart.MakeTorso());
            var armLeft = torso.AddChild(BodyPart.MakeArm("Left"));
            var armRight = torso.AddChild(BodyPart.MakeArm("Right"));
            var legLeft = torso.AddChild(BodyPart.MakeLeg("Left"));
            var legRight = torso.AddChild(BodyPart.MakeLeg("Right"));

            var handLeft = armLeft.AddChild(BodyPart.MakeAppendage("Left", "Hand", fingers));
            var handRight = armRight.AddChild(BodyPart.MakeAppendage("Right", "Hand", fingers));
            var footLeft = legLeft.AddChild(BodyPart.MakeAppendage("Left", "Foot", toes));
            var footRight = legRight.AddChild(BodyPart.MakeAppendage("Right", "Foot", toes));

            body.AllParts.AddRange(new[] {
                head,
                neck,
                torso,
                armLeft, handLeft,
                armRight,handRight,
                legLeft, footLeft,
                legRight, footRight,
            });

            if (profile != null)
                body.ApplyMaterialProfile(profile);

            return body;
        }

        #endregion  Body Makers

        public Body() {
            AllParts = new List<BodyPart>();
        }

        public class BodyPart : SystemicEntity {
            public BodyPart Parent { get; private set; }
            public List<BodyPart> Children { get; }

            public BodyPart AddChild(BodyPart child) {
                child.Parent = this;
                Children.Add(child);
                return child;
            }

            /// <summary>
            /// Get part by name in the part and its children.
            /// </summary>
            public BodyPart GetPart(string name) {
                if (name == Name)
                    return this;

                foreach (BodyPart part in Children) {
                    BodyPart result = part.GetPart(name);
                    if (result != null)
                        return result;
                }

                return null;
            }

            #region     Part Makers

            public static BodyPart MakeHead() =>
                new BodyPart(null, 1, "Head");

            public static BodyPart MakeNeck() =>
                new BodyPart(null, 1, "Neck");

            public static BodyPart MakeArm(string side) =>
                new BodyPart(null, 1, $"{side}Arm");

            public static BodyPart MakeAppendage(string side, string type, int fingers) =>
                new BodyPart(null, fingers, $"{side}{type}");

            public static BodyPart MakeFinger(int number) =>
                new BodyPart(null, 0, $"Finger{number}");

            public static BodyPart MakeTorso() =>
                new BodyPart(null, 4, "Torso");

            public static BodyPart MakeLeg(string side) =>
                new BodyPart(null, 1, $"{side}Leg");

            public static BodyPart MakeFoot(string side) =>
                new BodyPart(null, 0, $"{side}Foot");

            #endregion  Part Makers

            public BodyPart(BodyPart parent, int numberOfChildren, string name) {
                Parent = parent;
                Children = new List<BodyPart>(numberOfChildren);
                Name = name;
            }
        }
    }
}
