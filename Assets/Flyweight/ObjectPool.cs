using UnityEngine;
using System;
using System.Collections.Generic;

namespace Flyweight {

    /// <summary>
    /// Generates a fixed amount of copies of a given prototype object.
    /// Copies can be requested already initialized by passing the initialization action to the Request method.
    /// When an object is not used anymore, the client code must release it to the original pool.
    /// When all the pool objects are requested, requesting one more causes an exception to be thrown.
    /// The shared objects are added a SharedObject component, which contains a method ReleaseThis.
    /// </summary>
    public class ObjectPool : MonoBehaviour {

        public GameObject prototype;
        public int copies;

        Stack<GameObject> pool;

        void Awake() {
            if (copies <= 0) { throw new IndexOutOfRangeException("Number of copies in an object pool must be positive"); }
            pool = new Stack<GameObject>(copies);
            for (int i = 0; i < copies; i++) { pool.Push(GenerateCopy()); }
        }

        GameObject GenerateCopy() {
            var copy = GameObject.Instantiate(prototype);
            copy.AddComponent<SharedObject>();
            copy.GetComponent<SharedObject>().owner = this;
            return copy;
        }

        /// <summary>
        /// Returns a shared object, or throws an exception if no object is available.
        /// The object is initialized by the given action.
        /// </summary>
        public GameObject Request(Action<GameObject> initializer) {
            if (pool.Count == 0) { throw new InvalidOperationException("Empty object pool"); }
            var obj = pool.Pop();
            initializer.Invoke(obj);
            return obj;
        }

        /// <summary>Returns a shared object, or null if no object is available.</summary>
        public GameObject TryRequest(Action<GameObject> initializer) {
            if (pool.Count == 0) { return null; }
            return Request(initializer);
        }

        /// <summary>Returns the given object to the shared pool.</summary>
        public void Release(GameObject obj) {
            if (obj.GetComponent<SharedObject>() == null) { throw new ArgumentException("Cannot release an object not belonging to any pool"); }
            if (obj.GetComponent<SharedObject>().owner != this) { throw new ArgumentException("Cannot release an object not owned by this pool"); }
            if (pool.Contains(obj)) { throw new InvalidOperationException("Cannot release an already free shared object"); }
            pool.Push(obj);
        }
    }
}
