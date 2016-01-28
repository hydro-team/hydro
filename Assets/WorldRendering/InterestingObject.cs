using UnityEngine;
using System.Collections;

public class InterestingObject : MonoBehaviour {
	SpriteRenderer rend;
	public static Color glow= Color.red;
	public bool used = false;
	Color original;
	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		original = rend.material.color;
		StartCoroutine (glowAnimation ());
//		StartCoroutine("glowAnimation");
	}
	
	IEnumerator glowAnimation(){
//		Debug.Log ("Called glow");
		bool up = true;
		float count = 0f;
		float animationTime = 1.5f;
		while (!used) {
			if (count >= animationTime || count <= 0f)
				up = !up;
			if (up){
				count+= Time.deltaTime;

			}else{
				count-= Time.deltaTime;
			}
//			Debug.Log("glowing");
			rend.material.color = Color.Lerp(original,glow,Mathf.Clamp(count ,0f,animationTime)/animationTime);
			yield return null;
		}
		rend.material.color = original;
//		Debug.Log ("Finished animation");
		this.enabled = false;
		yield return null;
	}
}
