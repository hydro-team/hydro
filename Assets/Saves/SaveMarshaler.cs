using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Saves {

    public class SaveMarshaler : MonoBehaviour {

        public string filename;

        public string SavePath { get { return Application.persistentDataPath + "/" + filename; } }

        public void Marshal(IDictionary<string, IList<Entity>> context) {
            using (var stream = new StreamWriter(SavePath, false)) {
                foreach (var level in context) {
                    MarshalLevel(stream, level.Key, level.Value);
                }
            }
        }

        void MarshalLevel(StreamWriter stream, string levelName, IList<Entity> levelContext) {
            stream.WriteLine(levelName);
            stream.WriteLine(levelContext.Count);
            foreach (var entity in levelContext) {
                MarshalEntity(stream, entity);
            }
        }

        void MarshalEntity(StreamWriter stream, Entity entity) {
            stream.WriteLine(entity.type);
            stream.WriteLine(entity.name);
            stream.WriteLine(entity.bindings.Count);
            foreach (var binding in entity.bindings) {
                stream.WriteLine(binding.Key + ":" + binding.Value);
            }
        }

        public IDictionary<string, IList<Entity>> Unmarshal() {
            if (!File.Exists(SavePath)) { return new Dictionary<string, IList<Entity>>(); }
            var levels = new Dictionary<string, IList<Entity>>();
            string level;
            using (var stream = new StreamReader(SavePath)) {
                while (IsLevel(level = stream.ReadLine())) {
                    var count = int.Parse(stream.ReadLine());
                    levels[level] = UnmarshalEntities(stream, count);
                }
            }
            return levels;
        }

        bool IsLevel(string level) {
            return level != null && level.Length > 0;
        }

        IList<Entity> UnmarshalEntities(StreamReader stream, int count) {
            var entities = new List<Entity>(count);
            for (int i = 0; i < count; i++) {
                entities.Add(UnmarshalEntity(stream));
            }
            return entities;
        }

        Entity UnmarshalEntity(StreamReader stream) {
            var type = stream.ReadLine();
            var name = stream.ReadLine();
            var entity = Entity.Of(type, name);
            var bindings = int.Parse(stream.ReadLine());
            for (int i = 0; i < bindings; i++) {
                var binding = stream.ReadLine().Split(new char[] { ':' }, 2);
                entity.bindings[binding[0]] = binding[1];
            }
            return entity;
        }

        public struct Entity {

            public string type;
            public string name;
            public IDictionary<string, string> bindings;

            public static Entity Of(string type, string name) {
                var entity = new Entity();
                entity.type = type;
                entity.name = name;
                entity.bindings = new Dictionary<string, string>();
                return entity;
            }
        }
    }
}
