using System;
using System.Collections.Generic;
using System.Text;

namespace FOA {
    class MaterialProfile {
        public HashSet<SystemicProperty> Properties { get; }

        /// <summary>
        /// MaterialProfile with its own declared properties and all the properties of parent profiles.
        /// </summary>
        public MaterialProfile InheritingFrom(params MaterialProfile[] parents) {
            foreach (MaterialProfile parent in parents)
                foreach (SystemicProperty inheritingProperty in parent.Properties)
                    Properties.Add(inheritingProperty);

            return this;
        }

        MaterialProfile() {
            Properties = new HashSet<SystemicProperty>();
        }
        MaterialProfile(params SystemicProperty[] properties) : this() {
            Properties = new HashSet<SystemicProperty>(properties);
        }

        #region     Profiles

        public static MaterialProfile Wood = new MaterialProfile(
            "Aflame:Temperature above Flashpoint and Dry",
            "Wet:Moisture above Soakpoint",
            "Dry:Moisture below Drypoint"
            );

        public static MaterialProfile Flesh = new MaterialProfile(
            "Burnt:HeatDamage above BurnThreshold",
            "Cooked:HeatDamage above CookingThreshold"
            );

        public static MaterialProfile Bone = new MaterialProfile(
            "Broken:BluntDamage above BoneStrength"
            );

        public static MaterialProfile FleshWithBones = new MaterialProfile(
            ).InheritingFrom(Flesh, Bone);

        public static MaterialProfile Fat = new MaterialProfile(
            "Aflame:Temperature above Flashpoint",
            "Boiling:Temperature above Boilingpoint",
            "Liquid:Temperature above Freezingpoint"
            ).InheritingFrom(Flesh);

        #endregion  Profiles
    }
}
