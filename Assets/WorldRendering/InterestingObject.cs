using UnityEngine;
using System.Collections;

public class InterestingObject : MonoBehaviour {
	SpriteRenderer rend;
	static Color glow = Color.red;
	public bool used;
	Color original;
	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		original = rend.material.color;
	}
	
	IEnumerator glowAnimation(){
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
			rend.material.color = Color.Lerp(original,glow,Mathf.Clamp(count ,0f,animationTime)/animationTime);
			yield return null;
		}
		yield return null;
	}
}
