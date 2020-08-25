using System.Collections.Generic;

namespace FOA {
    /// <summary>
    /// State of a Systemic Entity. Contains values of systemic variables.
    /// </summary>
    public class SystemicEntityState {
        public HashSet<SystemicVariable> Variables { get; private set; }

        /// <summary>
        /// Returns whether the state has a variable by name.
        /// </summary>
        public bool HasVariable(string name) {
            foreach (SystemicVariable variable in Variables)
                if (variable.Name == name)
                    return true;
            return false;
        }

        /// <summary>
        /// Sets the value of a variable by name.
        /// </summary>
        public void SetValue(string name, float value) {
            if (!HasVariable(name))
                Variables.Add(new SystemicVariable(name, value));
            else
                foreach (SystemicVariable variable in Variables) {
                    if (variable.Name == name)
                        variable.Value = value;
                }
        }

        /// <summary>
        /// Modify the value of a variable by name.
        /// </summary>
        public void ModifyValue(string name, float amount) {
            GetVariable(name)?.Modify(amount);
        }

        /// <summary>
        /// Returns the value of a variable by name.
        /// </summary>
        public float GetValue(string name) {
            foreach (SystemicVariable value in Variables)
                if (value.Name == name)
                    return value.Value;
            return 0;
        }

        /// <summary>
        /// Returns a variable by name.
        /// </summary>
        public SystemicVariable GetVariable(string name) {
            // If the variable doesn't exist, create it.
            if (!HasVariable(name))
                SetValue(name, 0);

            // Loop through variables to find matching name.
            foreach (SystemicVariable variable in Variables)
                if (variable.Name == name)
                    return variable;

            // Should never be reached.
            return null;
        }

        public SystemicEntityState() {
            Variables = new HashSet<SystemicVariable>();
        }
    }
}
