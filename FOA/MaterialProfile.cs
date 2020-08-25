using System;
using System.Collections.Generic;
using System.Text;

namespace FOA {
    class MaterialProfile {
        public HashSet<Property> Properties { get; }

        MaterialProfile() {
            Properties = new HashSet<Property>();
        }
        /// <summary>
        /// Construct a profile from comma-separated name / value pairs.
        /// </summary>
        MaterialProfile(string input) : this() {
            foreach (string property in input.Split(',')) {
                string[] values = property.Split(' ');
                Properties.Add(new Property(values[0], float.Parse(values[1])));
            }
        }

        public static implicit operator MaterialProfile(string input)
            => new MaterialProfile(input);

        #region     Profiles

        public static MaterialProfile Wood = "Flammability 10,Porousness 2";

        #endregion  Profiles
    }
}
