using UnityEngine;

namespace Extensions {

    public static class ComponentExtensions {

        /// <summary>Places this component to the given position, preserving the z coordinate.</summary>
        public static void MoveTo(this Component component, Vector2 position) {
            component.transform.position = new Vector3(position.x, position.y, component.transform.position.z);
        }

        /// <summary>Places this component to the given local position, preserving the z coordinate.</summary>
        public static void MoveLocallyTo(this Component component, Vector2 position) {
            component.transform.localPosition = new Vector3(position.x, position.y, component.transform.localPosition.z);
        }

        /// <summary>Moves this component by the given direction, preserving the z coordinate.</summary>
        public static void MoveToward(this Component component, Vector2 direction) {
            component.transform.position += new Vector3(direction.x, direction.y);
        }
    }
}
