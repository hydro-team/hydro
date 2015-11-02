using UnityEngine;
using System.Collections;

public class Flow : MonoBehaviour {

    public float strength;

    void OnTriggerStay(Collider other) {
        var body = other.gameObject.GetComponent<Rigidbody>();
        if (body != null) {
            var height = transform.localScale.y;
            var flowBase = transform.position - (transform.up * 0.5f);
            var distance = (other.transform.position - flowBase).magnitude;
            if (distance < height) {
                var proportion = (height - distance) / height;
                var force = transform.up * strength * proportion;
                body.AddForce(force);
                var flowEnd = transform.position + (transform.up * 0.5f);
                Debug.DrawLine(flowEnd, flowEnd + force);
            }
        }
    }
}
