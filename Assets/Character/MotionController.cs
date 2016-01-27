using UnityEngine;

public class MotionController : MonoBehaviour {

    public float maxSpeed;
    public float stopRadius;

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
        if (direction.sqrMagnitude < stopRadius * stopRadius) {
            targetPosition = null;
        } else {
            body.velocity = direction.normalized * maxSpeed;
            remainingmMoveTime -= Time.deltaTime;
        }
    }
}
