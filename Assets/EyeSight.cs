using UnityEngine;
using System.Collections;

public class EyeSight : MonoBehaviour {

	public Transform [] hidingplace;
	HuntingFish fish;
	public float threshold;
	// Use this for initialization
	void Start () {
		fish = transform.parent.GetComponent<HuntingFish>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter2D(Collider2D other) {
		Debug.Log(other.name);
		if(other.tag == "Player"){
			for(int i = 0; i < hidingplace.Length; i++){
				if(Vector3.Distance(other.transform.position, hidingplace[i].position)<threshold){
					Debug.Log("hydro is hiding");
					return;
				}
			}
			fish.identified(other.transform.position);
		}
	}
}
