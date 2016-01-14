using UnityEngine;
using System.Collections;

public class HuntingFish : MonoBehaviour {

	public GameObject hydro;
	public Transform[] scoutinglimit;
	int index;
	public Vector3 target;
	public float radius;
	bool status;
	// Use this for initialization
	void Start () {
		status = false;
		index = 0;
		hydro = GameObject.FindGameObjectWithTag("Player");
		target = scoutinglimit[index].position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime*0.5f);
		Debug.Log(target);
		if(Vector3.Distance(transform.position, target)< 1 && !status){
			invertDirection();
		}
		if(status && ScreenFader.instance.fadeColor() == Color.black){
			hydro.transform.position = scoutinglimit[0].transform.position;
			target = scoutinglimit[index].position;
			status = false;
		}
	}
	void invertDirection(){
		Vector3 scale = transform.localScale;
		transform.localScale = new Vector3(scale.x*-1, 1, 1);
		index = (index + 1)%scoutinglimit.Length;
		target = scoutinglimit[index].position;
	}

	public void identified(Vector3 victim){
		target = victim;
		status = true;
	}

	public void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Player"){
			//TODO fade the screen
			ScreenFader.instance.activeFade();
		}
	}
}
