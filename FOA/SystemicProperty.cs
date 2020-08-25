using System;
using System.Collections.Generic;
using System.Text;

namespace FOA {
    class SystemicProperty {
        public string Name { get; }
        public float Value { get; set; }

        public SystemicProperty(string name, float value) {
            Name = name;
            Value = value;
        }
    }
}
