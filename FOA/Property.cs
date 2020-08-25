using System;
using System.Collections.Generic;
using System.Text;

namespace FOA {
    class Property {
        public string Name { get; }
        public float Value { get; set; }

        public Property(string name, float value) {
            Name = name;
            Value = value;
        }
    }
}
