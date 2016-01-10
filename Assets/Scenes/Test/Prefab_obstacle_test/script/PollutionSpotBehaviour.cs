using UnityEngine;
using System.Collections;

public class PollutionSpotBehaviour : MonoBehaviour {

	/*private float maxTime;
	private float curtime;
	public float radius;

	private Vector3 origin;*/
	Animator anim;

	// Use this for initialization
	void Start () {
		/*maxTime = Random.Range (3,20);
		curtime = maxTime;
		origin = transform.position;*/
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		/*if(curtime <= 0){
			float f= Random.Range(0,360);
			transform.RotateAround(transform.position, Vector3.back, f);
			curtime = maxTime;
		}else{
			curtime -= Time.deltaTime;
		}
		float distance = (origin - transform.position).sqrMagnitude;
		//Debug.Log(transform.localPosition.sqrMagnitude);
		if(distance >= radius){
			transform.RotateAround(transform.position, Vector3.back, Random.Range(90,180));
		}
		transform.Translate(Vector3.right* Time.deltaTime * 2);
*/
	}

	public void OnCollisionEnter2D(Collision2D collided){
		if(collided.gameObject.tag == "Player"){
			WaterColorManager.instance.eatenSpot();
			anim.SetTrigger("hit");
		}
	}

	public void eaten(){
		gameObject.SetActive(false);
	}
}
