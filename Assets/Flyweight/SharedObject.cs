using UnityEngine;
using System;

namespace Flyweight {

    /// <summary>Object owned by an ObjectPool.</summary>
    public class SharedObject : MonoBehaviour {

        public ObjectPool owner;

        /// <summary>Releases this shared object, returning to its belonging pool.</summary>
        public void ReleaseThis() {
            owner.Release(this.gameObject);
        }
    }
}
