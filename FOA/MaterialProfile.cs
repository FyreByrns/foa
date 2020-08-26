using System;
using System.Collections.Generic;
using System.Text;

namespace FOA {
    class MaterialProfile {
        public HashSet<SystemicProperty> Properties { get; }

        MaterialProfile() {
            Properties = new HashSet<SystemicProperty>();
        }
        MaterialProfile(params SystemicProperty[] properties) : this() {
            Properties = new HashSet<SystemicProperty>(properties);
        }

        #region     Profiles

        public static MaterialProfile Wood = new MaterialProfile("Aflame:Temperature above 10", "Waterlogged:Moisture above 20");

        #endregion  Profiles
    }
}
