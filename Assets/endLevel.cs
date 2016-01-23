using UnityEngine;
using System.Collections;

public class endLevel : MonoBehaviour {
	

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			ScreenFader.instance.halfShadetoBlack();
			if(ScreenFader.instance.fadeColor().a >= 250){
				Application.LoadLevel("end");
			}
		}
	}
}
