using UnityEngine;
using System;

namespace Physics {

    public class Flow : MonoBehaviour {

        public float strength;
        public float duration;

        float remainingTime;
        Action onExausted;

        void Awake() {
            if (GetComponent<Collider2D>() == null) {
                throw new OperationCanceledException("Flow requires a Collider2D component");
            }
        }

        public void Enable(Vector2 from, Vector2 to, Action onExausted) {
            gameObject.SetActive(true);
            var direction = to - from;
            transform.position = (from + to) / 2f;
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
