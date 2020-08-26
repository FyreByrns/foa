using System;
using System.Collections.Generic;
using System.Text;

using Otter.Components;

namespace FOA {
    class SystemicProperty : Component {
        public string Name { get; private set; }
        public List<Condition> Conditions { get; }
        public List<string> RequiredProperties { get; }
        public bool Active { get; private set; }

        public override void Added() {
            base.Added();
            // Can only be added to a SystemicEntity
            if (Entity is SystemicEntity entity) {
                entity.Properties.Add(Name, this);
            }
            else RemoveSelf();
        }

        public override void Update() {
            base.Update();
            if (!(Entity is SystemicEntity owner))
                return;

            bool allConditionsMatch = true;
            foreach (Condition condition in Conditions) {
                float relatedValue = owner.State.GetVariable(condition.VariableName).Value;
                float threshold = owner.State.GetConstant(condition.ThresholdConstantName).Value;

                bool success = false;
                if (condition.ActiveAboveThreshold)
                    success = relatedValue > threshold;
                else if (condition.ActiveBelowThreshold)
                    success = relatedValue < threshold;

                if (!success) {
                    allConditionsMatch = false;
                    break;
                }
            }

            bool allPropertiesMatch = true;
            foreach(string property in RequiredProperties) {
                if (!owner.PropertyActive(property)) {
                    allPropertiesMatch = false;
                    break;
                }
            }

            Active = allConditionsMatch && allPropertiesMatch;
        }

        /// <summary>
        /// Parse a SystemicProperty from a string.
        /// </summary>
        public static SystemicProperty Parse(string input) {
            // create property to return
            SystemicProperty property = new SystemicProperty();

            // split the string on ':' to contents[]
            string[] contents = input.Split(':');
            // Name of property is contents[0]
            property.Name = contents[0];
            // split contents[1] on ' ' to conditions[]
            string[] conditions = contents[1].Split(' ');
            // element = 0
            int element = 0;
            while (element < conditions.Length) {
                // the name of the next condition is conditions[element]
                string variable = conditions[element];
                // if conditions[element + 1] is "above" or "below" then the current condition is a variable
                string next = conditions[element + 1];
                if (next == "above" || next == "below") {
                    //   the systemic constant the variable is above is conditions[element + 2]
                    string constant = conditions[element + 2];
                    property.Conditions.Add(new Condition(variable, constant, next == "above"));
                    //   if "and" follows the systemic constant then more conditions follow
                    if (element + 3 < contents.Length && conditions[element + 3] == "and")
                        //     set element to element +4
                        element += 4;
                    //   otherwise, stop parsing
                    else
                        break;
                }
                // otherwise the current condition is a property
                else {
                    //   add the conditions[element] to the list of required properties
                    property.RequiredProperties.Add(conditions[element]);
                    //   if "and" follows, more conditions follow
                    if (element + 1 < contents.Length && conditions[element + 1] == "and")
                        //     set element to element + 2
                        element += 2;
                    //   otherwise, stop parsing
                    else
                        break;
                }
            }

            return property;
        }

        SystemicProperty() {
            Conditions = new List<Condition>();
            RequiredProperties = new List<string>();
        }

        public static implicit operator SystemicProperty(string input)
            => Parse(input);

        public class Condition {
            public string VariableName;
            public string ThresholdConstantName;

            public bool ActiveAboveThreshold { get; }
            public bool ActiveBelowThreshold => !ActiveAboveThreshold;

            public Condition(string variableName, string thresholdConstantName, bool activeAboveThreshold) {
                VariableName = variableName;
                ThresholdConstantName = thresholdConstantName;
                ActiveAboveThreshold = activeAboveThreshold;
            }
        }
    }
}
