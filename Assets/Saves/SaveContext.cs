using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Saves {

    public class SaveContext : MonoBehaviour {

        public string levelName;
        public SaveMarshaler marshaler;

        void Awake() {
            Deserialize();
        }

        public T Add<T>(string name) where T : SaveElement {
            T element = gameObject.AddComponent<T>();
            element.name = name;
            return element;
        }

        public T Get<T>(string name) where T : SaveElement {
            var elements = gameObject.GetComponents<T>();
            if (elements == null) { return null; }
            return elements.SingleOrDefault(element => element.name == name) as T;
        }

        public T GetOrCreate<T>(string name) where T : SaveElement {
            T element = Get<T>(name);
            if (element != null) { return element; }
            return Add<T>(name);
        }

        public void Serialize() {
            var elements = gameObject.GetComponents<SaveElement>();
            if (elements == null) { return; }
            var entities = elements.Select(element => {
                var entity = SaveMarshaler.Entity.Of(element.GetType().FullName, element.name);
                element.Serialize(entity.bindings);
                return entity;
            }).ToList();
            var context = marshaler.Unmarshal();
            context[levelName] = entities;
            marshaler.Marshal(context);
        }

        public void Deserialize() {
            var context = marshaler.Unmarshal();
            if (!context.ContainsKey(levelName)) { return; }
            foreach (var entity in context[levelName]) {
                var element = GetOrCreate(Type.GetType(entity.type), entity.name);
                element.Deserialize(entity.bindings);
            }
        }

        SaveElement GetOrCreate(Type type, string name) {
            var elements = gameObject.GetComponents(type);
            if (elements == null) { elements = new SaveElement[0]; }
            var element = elements.SingleOrDefault(e => ((SaveElement)e).name == name) as SaveElement;
            if (element == null) {
                element = gameObject.AddComponent(type) as SaveElement;
                element.name = name;
            }
            return element;
        }
    }
}
