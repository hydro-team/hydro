using UnityEngine;
using System.Collections;

public class WaterPhysics : MonoBehaviour {

    public float dragCoefficient;

    Rigidbody body;

	void Start () {
        body = GetComponent<Rigidbody>();
	}

	void Update () {
        var direction = -body.velocity.normalized;
        var speed = body.velocity.magnitude;
        var force = speed * speed * dragCoefficient;
        body.AddForce(direction * force);
	}
}
