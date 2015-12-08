using UnityEngine;
using System.Collections;

public class Hiding_fish_behaviour : MonoBehaviour {

	public GameObject[] seaweeds;
	public int mode; //0 = hide, 1 = moveto an other hiding; 2 = attack;
	public Collider2D attackarea;
	public float max_wait_time;
	public float timer;
	public  int position;
	private Rigidbody2D rb;
	public float speedmoveemnt;
	public float speedattack;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		mode = 0;
		int random = (int)Random.Range(0,seaweeds.Length);
		transform.position = seaweeds[random].transform.position;
		position = random;
		timer = Random.Range(3, max_wait_time);
	}
	
	// Update is called once per frame
	void Update () {
		if(mode == 0){
			//the fish is hiding
			if(timer > 0){
				timer = timer-Time.deltaTime;
			}else{
				mode = 1;
				changeposition();
				timer = Random.Range(3, max_wait_time);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			mode = 2;
			attackarea.enabled= false;
			Vector3 target = other.transform.position;
			Vector2 direction = (Vector2)(target -transform.position);
			rb.AddForce(direction.normalized * speedattack);
		}
	}
	void changeposition(){
		//seaweeds[position].GetComponent<BoxCollider2D>().enabled = false;
		attackarea.enabled= false;
		int next = AINextHop();
		bool ver =(next-position) < 0 ? true: false;
		seaweeds[next].GetComponent<Hidingplace>().active(ver);
		//seaweeds[position].GetComponent<Hidingplace>().deActive();
		Vector3 target = seaweeds[next].transform.position;;
		Vector2 direction = (Vector2)(target -transform.position);
		Debug.Log (direction);
		rb.AddForce(direction.normalized*speedmoveemnt);
		position = next;
	}

	void OnCollisionEnter2D(Collision2D coll){
		Debug.Log(coll.gameObject.tag);
		if(coll.gameObject.tag == "Hiding"){
			mode = 0;
			transform.position = seaweeds[position].transform.position;
			transform.rotation = Quaternion.Euler(Vector3.zero);
			seaweeds[position].GetComponent<Hidingplace>().deActive();
			rb.velocity = Vector2.zero;
			rb.angularVelocity = 0f;
			attackarea.enabled= true;

		}
		if(coll.gameObject.tag == "Player"){
			rb.velocity = rb.velocity * -1;
			seaweeds[position].GetComponent<Hidingplace>().active(true);
		}
	}

	private int AINextHop(){
		int rand;
		/*if(position == seaweeds.Length-1){
			return position -1;
		}
		if(position == 0){
			return 1;
		}
		rand = Random.value < 0.5 ? -1 : 1;
		Debug.Log ("rand = " + rand);*/
		rand = Random.value < 0.5 ? -1 : 1;
		return (Mathf.Abs(rand + position) % seaweeds.Length);

	}
}
