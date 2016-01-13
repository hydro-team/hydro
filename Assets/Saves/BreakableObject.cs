using System.Collections.Generic;

namespace Saves {

    public class BreakableObject : SaveElement {

        public bool broken = false;

        public override void Serialize(IDictionary<string, string> bindings) {
            bindings["broken"] = broken.ToString();
        }

        public override void Deserialize(IDictionary<string, string> bindings) {
            broken = bool.Parse(bindings["broken"]);
        }
    }
}
