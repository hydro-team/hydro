using UnityEngine;
using System.Collections;

public class Flow : MonoBehaviour {

    public float strength;

    public virtual Vector3 CalculateForceFor(Rigidbody body) {
        var height = transform.localScale.y;
        var flowBase = transform.position - (transform.up * 0.5f);
        var distance = (body.transform.position - flowBase).magnitude;
        if (distance >= height) { return Vector3.zero; }
        var proportion = (height - distance) / height;
        return transform.up * strength * proportion;
    }

    void OnTriggerStay(Collider other) {
        var body = other.gameObject.GetComponent<Rigidbody>();
        if (body != null) {
            var force = CalculateForceFor(body);
            body.AddForce(force);
            var flowEnd = transform.position + (transform.up * 0.5f);
            Debug.DrawLine(flowEnd, flowEnd + force);
        }
    }
}
