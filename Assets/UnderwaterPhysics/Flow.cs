using UnityEngine;
using System;

namespace UnderwaterPhysics {

    public class Flow : MonoBehaviour {

        public float strength;
        public float duration;

        float remainingTime;
        Action onExausted;

        void Awake() {
            if (GetComponent<Collider2D>() == null) {
                throw new OperationCanceledException("Flow requires a Collider2D component");
            }
            if (strength <= 0) { Debug.LogWarning("Flow: strength should be a positive value, but it is set to " + strength); }
            if (duration <= 0) { Debug.LogWarning("Flow: duration should be a positive value, but it is set to " + duration); }
        }

        public void Enable(Vector2 from, Vector2 to, float z = 0, Action onExausted = null) {
            gameObject.SetActive(true);
            var direction = to - from;
            transform.position = (Vector3)((from + to) / 2f) + (Vector3.forward * z);
            transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
            transform.localScale = new Vector2(transform.localScale.x, direction.magnitude);
            remainingTime = duration;
            this.onExausted = onExausted;
        }

        void Update() {
            if (remainingTime <= 0) {
                if (onExausted != null) { onExausted.Invoke(); }
                gameObject.SetActive(false);
            } else {
                remainingTime -= Time.deltaTime;
            }
        }

        void OnTriggerStay2D(Collider2D other) {
            var body = other.gameObject.GetComponent<Rigidbody2D>();
            if (body != null) {
                var force = CalculateForceFor(body);
                body.AddForce(force);
            }
        }

        Vector2 CalculateForceFor(Rigidbody2D body) {
            var height = transform.localScale.y;
            var flowBase = transform.position - (transform.up * 0.5f);
            var distance = (body.transform.position - flowBase).magnitude;
            if (distance >= height) { return Vector3.zero; }
            var distanceProportion = (height - distance) / height;
            var timeProportion = remainingTime / duration;
            return transform.up * strength * distanceProportion * timeProportion;
        }
    }
}
