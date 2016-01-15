using UnityEngine;
using UnityEngine.UI;

public class ScrollOnTopWhenEnabled : MonoBehaviour {

    ScrollRect scroll;

    void Awake() { scroll = GetComponent<ScrollRect>(); }

    void OnEnable() { scroll.verticalNormalizedPosition = 1f; }
}
