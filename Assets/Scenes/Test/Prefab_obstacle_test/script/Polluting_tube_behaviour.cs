using UnityEngine;
using System.Collections;

public class Polluting_tube_behaviour : MonoBehaviour {

	public GameObject particleSys;
	private ParticleSystem particle;
	public Transform player;
	public float distance;
	private bool active;
	// Use this for initialization
	void Start () {
		active = true;
		particle = particleSys.GetComponent<ParticleSystem>();
	}

	public void OnCollisionEnter2D(Collision2D collided){
		string s = collided.gameObject.tag;
		if(s == "indestructablerock"){
			active = false;
			particle.gameObject.SetActive(false);
		}
	}
	private void setVisible(bool visible){
		Debug.Log("SetVisible " + visible);
		if(active){
			if(visible){
				particle.enableEmission = true;

			}else{
				particle.enableEmission = false;
			}
		}
	}

	public void OnTriggerExit2D(Collider2D other) {
		setVisible(false);
	}

	public void OnTriggerEnter2D(Collider2D other) {
		setVisible(true);
	}
	
}
