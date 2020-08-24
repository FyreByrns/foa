using System;
using System.Collections.Generic;
using System.Text;

using static FOA.MaterialProperties;

namespace FOA {
    /// <summary>
    /// A preset for a material.
    /// </summary>
    class MaterialProfile {
        public Dictionary<MaterialProperties, float> DefaultValues { get; }

        MaterialProfile(params (MaterialProperties property, float amount)[] values) {
            DefaultValues = new Dictionary<MaterialProperties, float>();

            foreach (var prop in values)
                DefaultValues[prop.property] = prop.amount;
        }

        #region Profiles

        public static MaterialProfile Wood = new MaterialProfile(
            (Flammable, 10),
            (Porous, 2));

        #endregion
    }
}
