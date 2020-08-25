namespace FOA {
    public class SystemicVariable {
        public string Name { get; }
        public float Value { get; set; }

        public void Modify(float amount) {
            Value += amount;
        }

        public SystemicVariable(string name, float value) {
            Name = name;
            Value = value;
        }
    }
}
