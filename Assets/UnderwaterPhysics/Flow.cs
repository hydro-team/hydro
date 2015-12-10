using UnityEngine;
using System;

namespace UnderwaterPhysics {

    public class Flow : MonoBehaviour {

        public float strength;
        public float duration;

        ParticleSystem bubbles;
        Action onExausted;
        float remainingTime = 0;
        bool active = false;

        void Awake() {
            if (GetComponent<Collider2D>() == null) {
                throw new OperationCanceledException("Flow requires a Collider2D component");
            }
            if (strength <= 0) { Debug.LogWarning("Flow: strength should be a positive value, but it is set to " + strength); }
            if (duration <= 0) { Debug.LogWarning("Flow: duration should be a positive value, but it is set to " + duration); }
            bubbles = GetComponentInChildren<ParticleSystem>();
            bubbles.Stop();
        }

        public void Enable(Vector2 from, Vector2 to, float z = 0, Action onExausted = null) {
            var direction = to - from;
            var length = direction.magnitude;
            transform.position = (Vector3)((from + to) / 2f) + (Vector3.forward * z);
            transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
            transform.localScale = new Vector3(transform.localScale.x, length, 1);
            remainingTime = duration;
            this.onExausted = onExausted;
            EnableBubbles(length);
            active = true;
        }

        void EnableBubbles(float length) {
            bubbles.transform.localPosition = new Vector3(0, -0.5f);
            bubbles.startSpeed = strength;
            bubbles.startLifetime = length / bubbles.startSpeed;
            bubbles.Play();
        }

        void Update() {
            if (!active) { return; }
            if (remainingTime <= 0) {
                active = false;
                bubbles.Stop();
                if (onExausted != null) { onExausted.Invoke(); }
            } else {
                remainingTime -= Time.deltaTime;
            }
        }

        void OnTriggerStay2D(Collider2D other) {
            if (!active) { return; }
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
