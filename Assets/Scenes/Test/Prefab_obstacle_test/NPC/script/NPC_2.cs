using UnityEngine;
using System.Collections;

public class NPC_2 : MonoBehaviour {

	public GameObject target_to_be_destroyed;
	public Transform [] navigationPoints;
	public bool activemission;
	public int index;
	Vector3 direction;
	//public Animator anim;
	
	// Use this for initialization
	void Start () {
		index = 0;
		activemission = true;
		//anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(activemission && target_to_be_destroyed.GetComponent<Collider2D>().enabled == false){
			activemission = false;
			Debug.Log ("Mi sono sveglaito " + gameObject.name);
			//anim.SetTrigger("Quest_ended");
		}
		if(!activemission && index < navigationPoints.Length){
			direction = (navigationPoints[index].position - transform.position);
			//Debug.Log (navigationPoints[index].position + " " + transform.position);
			transform.Translate(direction.normalized * Time.deltaTime*3);
			Debug.Log(direction.sqrMagnitude);
			if(direction.sqrMagnitude < 0.05f){
				index ++;
			}
		}

	}
}
