using UnityEngine;
using System.Collections;

public class MotionController : MonoBehaviour {

    public float maxPushForce;

    Vector2? targetPosition;
    bool colliding = false;
    Vector2 lastPosition;
    Rigidbody2D body;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    public void MoveTo(Vector2 position) {
        targetPosition = position;
    }

    void Update() {
        if (targetPosition == null) { return; }
        if (colliding) { StartCoroutine(StopWhenCollidingAfter(0.5f)); }
        var direction = targetPosition.Value - (Vector2)transform.position;
        var force = direction.sqrMagnitude > maxPushForce * maxPushForce ? direction.normalized * maxPushForce : direction;
        body.AddForce(force);
    }

    IEnumerator StopWhenCollidingAfter(float seconds) {
        while (colliding && seconds > 0) {
            seconds -= Time.deltaTime;
            yield return null;
        }
        if (colliding) { targetPosition = null; }
        yield return null;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        colliding = true;
    }

    void OnCollisionExit2D(Collision2D collision) {
        colliding = false;
    }
}
