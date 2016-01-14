using System.Collections.Generic;

namespace Saves {

    public class HydroState : SaveElement {

        public float x;
        public float y;

        public override void Serialize(IDictionary<string, string> bindings) {
            bindings["x"] = x.ToString();
            bindings["y"] = y.ToString();
        }

        public override void Deserialize(IDictionary<string, string> bindings) {
            x = int.Parse(bindings["x"]);
            y = int.Parse(bindings["y"]);
        }
    }
}
