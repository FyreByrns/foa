using System;
using System.Collections.Generic;
using System.Text;

using Otter.Components;

namespace FOA {
    class SystemicProperty : Component{
        public string Name { get; }
        public string RelatedVariable { get; }
        public float Threshold { get; set; }
        public bool OnEdge { get; private set; }
        public bool Active { get; private set; }

        public bool ActiveAboveThreshold { get; }
        public bool ActiveBelowThreshold => !ActiveAboveThreshold;

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

            float relatedValue = owner.State.GetVariable(RelatedVariable).Value;

            if (ActiveAboveThreshold)
                Active = relatedValue > Threshold;
            else if (ActiveBelowThreshold)
                Active = relatedValue < Threshold;
            else if (relatedValue == Threshold)
                OnEdge = true;
        }

        public SystemicProperty(string name, string relatedVariable, float threshold, bool activeAboveThreshold) {
            Name = name;
            RelatedVariable = relatedVariable;
            Threshold = threshold;
            ActiveAboveThreshold = activeAboveThreshold;
        }

        public static implicit operator SystemicProperty(string input) {
            string[] contents = input.Split(' ',':');
            string name = contents[0];
            string related = contents[1];
            bool activeAbove = contents[2] == "above";
            float threshold = float.Parse(contents[3]);

            SystemicProperty property = new SystemicProperty(name, related, threshold, activeAbove);
            return property;
        }
    }
}
