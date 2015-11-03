using UnityEngine;
using System.Collections;

public class DecayingFlow : Flow {

    public float duration;
    float remaining;

    void Start() {
        remaining = duration;
    }

	void Update () {
        if (remaining > 0) { remaining -= Time.deltaTime; }
	}

    public override Vector3 CalculateForceFor(Rigidbody body) {
        var proportion = remaining / duration;
        return base.CalculateForceFor(body) * proportion;
    }
}
