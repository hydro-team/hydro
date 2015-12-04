using UnityEngine;
using System.Collections;

//FIXME
[RequireComponent (typeof(PolygonCollider2D))]
public class GeyserBehviour : MonoBehaviour {
	/// <summary>
	/// This variable contains the time in second that the geyser will be producing the flow.
	/// </summary>
	public float activitytime;
	/// <summary>
	/// This variable contains the time in second that the geyser will stop producing the flow.
	/// </summary>
	public float restTime;
	/// <summary>
	/// This variable contains the strenght of the flow.
	/// </summary>
	public float intensity;
	/// <summary>
	/// This variable contains the time in second untill the next change of state.
	/// </summary>
	private float curtime;
	/// <summary>
	/// This variable contains the state of the geyser: true if active, false otherwise.
	/// </summary>
	private bool active;

	public ParticleSystem GeyserParticle;
	PolygonCollider2D polycol;


	void Start () {
//		active = true;
		polycol = GetComponent<PolygonCollider2D> ();
		active = false;
		geyserEnable ();
//		curtime = activitytime;
	}
	
	/// <summary>
	/// This method perform the task of activatig and disactiate the geyser once the timer make the state change.
	/// </summary>
	void Update () {
//		if(curtime >= 0){
//			curtime -= Time.deltaTime;
//		}else{
//			if(active){
//				active = false;
//				GeyserParticle.emissionRate = 0;
//				curtime = restTime;
//				gameObject.GetComponent<PolygonCollider2D>().enabled = false;
//			}else{
//				active = true;
//				GeyserParticle.emissionRate =20;
//				curtime = activitytime;
//				gameObject.GetComponent<PolygonCollider2D>().enabled = true;
//			}
//		}
	
	}

	void geyserEnable(){
		active = !active;
		GeyserParticle.emissionRate = active ? 20 : 0;
		polycol.enabled = active;
		Invoke ("geyserEnable", active ? activitytime : restTime);
	}

	/// <summary>
	/// This method perform the task of computing the strenght of the fow and produce the effect on the approaching objects.
	/// </summary>
	/// <param name="other"> 
	/// the collider2D of the objet that entered in the trigger area
	/// </param>
	void OnTriggerStay2D(Collider2D other) {
		//Debug.Log (other.name);
		Rigidbody2D rb = other.attachedRigidbody;
		if(rb != null){
			Vector2 dir = other.gameObject.transform.position - gameObject.transform.position;
			float dist = Vector2.Distance(other.gameObject.transform.position,gameObject.transform.position);
			Debug.DrawRay(this.transform.position, dir);
			rb.AddForce(dir*(intensity/dist));
		}
	}
}
