using UnityEngine;
using System.Collections;

public class MotionController : MonoBehaviour {

    public float maxPushForce;

    Vector2? targetPosition;
    float remainingmMoveTime;
    Rigidbody2D body;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    public void MoveTo(Vector2 position) {
        targetPosition = position;
        remainingmMoveTime = 5f;
    }

    void Update() {
        if (targetPosition == null) { return; }
        if (remainingmMoveTime <= 0) { return; }
        var direction = targetPosition.Value - (Vector2)transform.position;
        var force = direction.sqrMagnitude > maxPushForce * maxPushForce ? direction.normalized * maxPushForce : direction;
        body.AddForce(force);
        remainingmMoveTime -= Time.deltaTime;
    }
}
