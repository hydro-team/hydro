using UnityEngine;

public class ToroidalWandering : MonoBehaviour {

    public Rect toroidArea;
    public float speed;
    public float steering;
    public float lookaheadDistance;
    public float lookaheadRadius;

    float steeringAngle = Mathf.PI / 2f;
    Vector2 direction = Vector2.up;

    void Update() {
        UpdateSteeringAngle();
        UpdateDirection();
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        KeepInsideTheToroid();
    }

    void UpdateSteeringAngle() {
        var variation = Random.value - Random.value;
        steeringAngle += variation * steering * Time.deltaTime;
    }

    void UpdateDirection() {
        var newDirection = (direction * lookaheadDistance) + (VectorFromAngle(steeringAngle) * lookaheadRadius);
        direction = newDirection.normalized;
    }

    static Vector2 VectorFromAngle(float radians) {
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }

    void KeepInsideTheToroid() {
        var x = transform.position.x;
        var y = transform.position.y;
        if (x > toroidArea.xMax) {
            transform.position -= new Vector3(toroidArea.width, 0f);
        } else if (x < toroidArea.xMin) {
            transform.position += new Vector3(toroidArea.width, 0f);
        }
        if (y > toroidArea.yMax) {
            transform.position -= new Vector3(0f, toroidArea.height);
        } else if (y < toroidArea.yMin) {
            transform.position += new Vector3(0f, toroidArea.height);
        }
    }
}
