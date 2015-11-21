using UnityEngine;
using System.Collections;

public class GeyserBehviour : MonoBehaviour {

	public float activitytime;
	public float restTime;
	public float intensity;

	private float curtime;
	private bool active;
	// Use this for initialization
	void Start () {
		active = true;
		curtime = activitytime;
	}
	
	// Update is called once per frame
	void Update () {
		if(curtime >= 0){
			curtime -= Time.deltaTime;
		}else{
			if(active){
				active = false;
				curtime = restTime;
				gameObject.GetComponent<PolygonCollider2D>().enabled = false;
			}else{
				active = true;
				curtime = activitytime;
				gameObject.GetComponent<PolygonCollider2D>().enabled = true;
			}
		}
	
	}

	void OnTriggerStay2D(Collider2D other) {
		Debug.Log (other.name);
		Rigidbody2D rb = other.attachedRigidbody;
		if(rb != null){
			Vector2 dir = other.gameObject.transform.position - gameObject.transform.position;
			float dist = Vector2.Distance(other.gameObject.transform.position,gameObject.transform.position);
			Debug.DrawRay(this.transform.position, dir);
			rb.AddForce(dir*(intensity/dist));
		}
	}
}
