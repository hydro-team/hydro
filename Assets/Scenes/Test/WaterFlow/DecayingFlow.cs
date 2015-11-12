using UnityEngine;
using System;
using System.Collections;

public class DecayingFlow : Flow {

    public float duration;

    Action onExausted;
    float remainingTime;
    bool decaying = false;

    IEnumerator Decay() {
        decaying = true;
        while (remainingTime > 0) {
            remainingTime -= Time.deltaTime;
            yield return null;
        }
        decaying = false;
        onExausted.Invoke();
        gameObject.SetActive(false);
    }

    public void Initialize(Vector2 start, Vector2 end, Action onExausted) {
        gameObject.SetActive(true);
        var direction = end - start;
        transform.position = (start + end) / 2f;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        transform.localScale = new Vector3(1f, direction.magnitude / 2f, 1f);
        remainingTime = duration;
        this.onExausted = onExausted;
        if (!decaying) { StartCoroutine(Decay()); }
    }

    public override Vector3 CalculateForceFor(Rigidbody body) {
        var proportion = remainingTime / duration;
        return base.CalculateForceFor(body) * proportion;
    }
}
