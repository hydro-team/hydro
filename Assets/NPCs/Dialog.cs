using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {

	//FIXME
	public GameObject Emotions;
	bool _feels;
	
	bool showFeelings{
		get{return _feels;}
		set{
			if(value == true){
//				Emotions.transform.localScale = Vector3.one;
			}
			else{
//				foreach (GameObject feel in Emotions){
//					feel.transform.localScale = Vector3.zero;
//				}
			}
			_feels = value;
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTrigger2DEnter(Collider2D entered){
		if (entered.gameObject.tag == "Player") {
			showFeelings = true;
		}
	}

	void OnTrigger2DExit(){
		showFeelings = false;
	}
}
