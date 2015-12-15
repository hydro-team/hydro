using UnityEngine;
using System.Collections;



public class BreakableRockEffects : MonoBehaviour {
	
	public float mass;
	public float drag;
	SpriteRenderer rend;
	public float FadeTime;
	float curTime;

	void Update(){
		rend.color = new Color (rend.color.r, rend.color.g, rend.color.b, 1f - (Time.time - curTime) / FadeTime);
	}

	public void BreakRock(Vector2 impact){
		Rigidbody2D r2d = gameObject.AddComponent<Rigidbody2D> ();
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		r2d.mass = mass;
		r2d.drag = drag;
		r2d.AddForce (impact, ForceMode2D.Impulse);
		curTime = Time.time;
		enabled = true;
		rend = GetComponent <SpriteRenderer> ();
		Invoke ("KillPiece", FadeTime);
	}

	void KillPiece(){
		//TODO chiamare il padre e dirgli UCCIDIMIIIIIIII
		gameObject.SetActive (false);
	}

}
