using UnityEngine;
using System.Collections;

public class NPC_1 : MonoBehaviour {

	public GameObject target_to_be_destroyed;
	public bool activemission;
	public Animator anim;
	public float limitvisible;

	// Use this for initialization
	void Start () {
		activemission = true;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(activemission && target_to_be_destroyed.GetComponent<Collider2D>().enabled == false){
			activemission = false;
			anim.SetTrigger("Quest_ended");

		}
		if(!activemission){
			//Debug.Log("Position = " + transform.position + " shift = " + Vector3.left * Time.deltaTime * 5);
			transform.Translate(Vector3.left * Time.deltaTime * 5);
			//Debug.Log("Result " + transform.position);
			if(transform.position.x < limitvisible){
				gameObject.SetActive(false);
			}
		}
	}
}
