using UnityEngine;
using System.Collections;

public class endLevel : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			ScreenFader.instance.halfShadetoBlack("end");
			/*Debug.Log(ScreenFader.instance.fadeColor().a);
			if(ScreenFader.instance.fadeColor().a >= 2)
				Application.LoadLevel("end");
			}*/
		}
	}
}
