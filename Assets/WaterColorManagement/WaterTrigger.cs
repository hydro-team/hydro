using UnityEngine;
using System.Collections;

public class WaterTrigger : MonoBehaviour {

	public int level;

	public GameObject hydro;

	public Bounds bound;
	void Start () {
		bound = GetComponent<BoxCollider>().bounds;
		hydro = GameObject.FindGameObjectWithTag("Player");
	}
	void Update () {

		if(bound.Contains(hydro.transform.position)){
			WaterColorManager.instance.changedLevel(level);
		}
	}
}
