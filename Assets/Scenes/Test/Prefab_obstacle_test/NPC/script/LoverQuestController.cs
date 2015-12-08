using UnityEngine;
using System.Collections;

public class LoverQuestController : MonoBehaviour {
	public Transform [] fishes;
	Vector3 meetpoint;
	float cumnulativeDistance;
	public bool activemission;
	Animator anim;

	// Use this for initialization
	void Start () {
		meetpoint = transform.position;
		activemission = true;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		cumnulativeDistance = (fishes[0].position +fishes[1].position - 2*meetpoint).sqrMagnitude;
		if (activemission && cumnulativeDistance < 1){
			activemission = false;
			anim.SetTrigger("Quest_ended");
		}
	}
}
