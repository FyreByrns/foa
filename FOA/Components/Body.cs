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
        public static Body MakeHumanoid(
            int fingers = 5, int toes = 5,
            int teeth = 32, int ribs = 24,
            int meatTorso = 6, int meatArms = 2, int meatLegs = 3, float meatFactor = 1f,
            int fatTorso = 2, int fatArms = 0, int fatLegs = 1, float fatFactor = 1f,
            MaterialProfile partProfile = null, MaterialProfile boneProfile = null, MaterialProfile fleshProfile = null, MaterialProfile fatProfile = null) {
            Body body = new Body();

            BodyPart head = body.Base = BodyPart.MakeHead();
            BodyPart neck = head.AddChild(BodyPart.MakeNeck());
            BodyPart torso = neck.AddChild(BodyPart.MakeTorso());
            BodyPart armLeft = torso.AddChild(BodyPart.MakeArm("Left"));
            BodyPart armRight = torso.AddChild(BodyPart.MakeArm("Right"));
            BodyPart legLeft = torso.AddChild(BodyPart.MakeLeg("Left"));
            BodyPart legRight = torso.AddChild(BodyPart.MakeLeg("Right"));

            BodyPart handLeft = armLeft.AddChild(BodyPart.MakeAppendage("Left", "Hand", fingers));
            BodyPart handRight = armRight.AddChild(BodyPart.MakeAppendage("Right", "Hand", fingers));
            BodyPart footLeft = legLeft.AddChild(BodyPart.MakeAppendage("Left", "Foot", toes));
            BodyPart footRight = legRight.AddChild(BodyPart.MakeAppendage("Right", "Foot", toes));

            body.AllParts.AddRange(new[] {
                head,
                neck,
                torso,
                armLeft, handLeft,
                armRight,handRight,
                legLeft, footLeft,
                legRight, footRight,
            });

            partProfile ??= MaterialProfile.FleshWithBones;
            boneProfile ??= MaterialProfile.Bone;
            fleshProfile ??= MaterialProfile.Flesh;
            fatProfile ??= MaterialProfile.Fat;

            if (partProfile != null)
                body.ApplyMaterialProfile(partProfile);

            meatTorso = (int)(meatTorso * meatFactor);
            meatArms = (int)(meatArms * meatFactor);
            meatLegs = (int)(meatLegs * meatFactor);

            fatTorso = (int)(fatTorso * fatFactor);
            fatArms = (int)(fatArms * fatFactor);
            fatLegs = (int)(fatLegs * fatFactor);

            Inventory headInventory = head.AddComponent<Inventory>();
            headInventory.AddItem("Skull", 1, boneProfile);
            headInventory.AddItem("Jawbone", 1, boneProfile);
            headInventory.AddItem("Tooth", teeth, boneProfile);

            Inventory torsoInventory = torso.AddComponent<Inventory>();
            torsoInventory.AddItem("Rib", ribs, boneProfile);
            torsoInventory.AddItem("Meat", meatTorso, fleshProfile);
            torsoInventory.AddItem("Fat", fatTorso, fatProfile);

            Inventory armLeftInventory = armLeft.AddComponent<Inventory>();
            armLeftInventory.AddItem("Humerus", 1, boneProfile);
            armLeftInventory.AddItem("Ulna", 1, boneProfile);
            armLeftInventory.AddItem("Radius", 1, boneProfile);
            armLeftInventory.AddItem("Meat", meatArms, fleshProfile);
            armLeftInventory.AddItem("Fat", fatArms, fatProfile);

            Inventory armRightInventory = armRight.AddComponent<Inventory>();
            armRightInventory.AddItem("Humerus", 1, boneProfile);
            armRightInventory.AddItem("Ulna", 1, boneProfile);
            armRightInventory.AddItem("Radius", 1, boneProfile);
            armRightInventory.AddItem("Meat", meatArms, fleshProfile);
            armRightInventory.AddItem("Fat", fatArms, fatProfile);

            Inventory legLeftInventory = legLeft.AddComponent<Inventory>();
            legLeftInventory.AddItem("Femur", 1, boneProfile);
            legLeftInventory.AddItem("Fibula", 1, boneProfile);
            legLeftInventory.AddItem("Tibula", 1, boneProfile);
            legLeftInventory.AddItem("Meat", meatLegs, fleshProfile);
            legLeftInventory.AddItem("Fat", fatLegs, fatProfile);

            Inventory legRightInventory = legRight.AddComponent<Inventory>();
            legRightInventory.AddItem("Femur", 1, boneProfile);
            legRightInventory.AddItem("Fibula", 1, boneProfile);
            legRightInventory.AddItem("Tibula", 1, boneProfile);
            legRightInventory.AddItem("Meat", meatLegs, fleshProfile);
            legRightInventory.AddItem("Fat", fatLegs, fatProfile);

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
