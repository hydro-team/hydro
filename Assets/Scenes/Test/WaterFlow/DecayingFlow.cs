using UnityEngine;
using System.Collections;

public class DecayingFlow : Flow {

    public float duration;
    float remainingTime;

    void Start() {
        remainingTime = duration;
        StartCoroutine(Decay());
    }

    IEnumerator Decay() {
        while (remainingTime > 0) {
            remainingTime -= Time.deltaTime;
            yield return null;
        }
        GameObject.Destroy(this.gameObject);
    }

    public override Vector3 CalculateForceFor(Rigidbody body) {
        var proportion = remainingTime / duration;
        return base.CalculateForceFor(body) * proportion;
    }
}
