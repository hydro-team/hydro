using UnityEngine;
using System.Collections.Generic;

public class Generator : MonoBehaviour {

    public GameObject prototype;
    public float period;

    float remainingTime;
    IList<GameObject> created = new List<GameObject>();

	void Update () {
	    if (remainingTime <= 0) {
            remainingTime += period;
            var clone = GameObject.Instantiate(prototype, transform.position, Quaternion.identity);
            created.Add(clone as GameObject);
            if (created.Count > 10) {
                GameObject.Destroy(created[0]);
                created.RemoveAt(0);
            }
        }
        remainingTime -= Time.deltaTime;
	}
}
