using UnityEngine;
using System.Collections;

public class endLevel : MonoBehaviour {
	

	void OnCollisionEnter2D(Collision2D other){
		ScreenFader.instance.halfShadetoBlack();
		if(ScreenFader.instance.fadeColor().r <= 5){
			Application.LoadLevel("end");
		}
		
	}
}
