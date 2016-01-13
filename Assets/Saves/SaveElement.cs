using UnityEngine;
using System.Collections.Generic;

namespace Saves {

    public abstract class SaveElement : MonoBehaviour {

        public new string name;

        public abstract void Serialize(IDictionary<string, string> bindings);

        public abstract void Deserialize(IDictionary<string, string> bindings);
    }
}
