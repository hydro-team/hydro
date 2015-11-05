using UnityEngine;
using System.Collections;

public class SliceMovement : MonoBehaviour {

	//Variabili di riferimento del mondo
	public const int N_slice= 3;// costante del livello che indica il numero di slice
	public float[] spostamenti = new float[N_slice]; //array contenente le altezze delle origini dei diversi slice
	public GameObject hydro; //reference al modello di hydro

	Rigidbody2D rb; // this rigidboody
	public GameObject cameracenter; //reference del centro di movimento delle camere
	public GameObject collider_upper; //reference al controllore di via libera per lice livello superiore
	public GameObject collider_down; //reference al controllore di via libera per slice livello inferiore
	Checkmovement up;  //controllore via libera superiore
	Checkmovement down; // controllore via libera inferiore



	//variabili controllo stato
	int rotated = 2; //stao della rotazione: 0 in rotazione, 1 in traslazione, 2 stallo
	bool collided = false; //stato della collisione con la fine dello slice
	int rotdirection = 0; // direzione della rotazione
	public int level; // livello a cui si trova hydro
	bool changedlevel; // stato se sono in fase di cambiamento del livello
	//bool direction;
	//RaycastHit2D hit;

	// Use this for initialization
	void Start () {
		level = 1;
		changedlevel = false;
		rb = GetComponent<Rigidbody2D>();
		up = collider_upper.GetComponent<Checkmovement>();
		down = collider_down.GetComponent<Checkmovement>();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(new Vector2(transform.position.x, transform.position.y));
		/*RaycastHit2D hit = Physics2D.Raycast( new Vector2(transform.position.x, transform.position.y), Vector2.zero, cameralayer);
		if(hit.collider != null){
			//Debug.Log(hit.collider.name);
			Debug.DrawRay(cameracenter.transform.position, Vector3.forward * 10, Color.magenta);
		}*/

		//Rotazione verso la direzione di movimento del modello di hydro
		if(rotated == 0){
			//TODO inveertire rotazione quando si scende
			Quaternion targetRotation = Quaternion.LookRotation(new Vector3(-100, hydro.transform.position.y, hydro.transform.position.z) - hydro.transform.position);
			// Smoothly rotate towards the target point.
			hydro.transform.rotation = Quaternion.Slerp(hydro.transform.rotation, targetRotation, 2 * Time.deltaTime);
			//Debug.Log(hydro.transform.forward);
			if(equalvec(hydro.transform.forward, Vector3.left)){
				Debug.Log ("finita rotazione");
				rotated = 1;
			}
		}
		//traslazione verso la fine dello slice verso la direzione di movimento
		if(rotated == 1){
			if(hydro.transform.position.z < 0){
				//TODO direzione da cambiare quando si scende
				hydro.transform.Translate(Vector3.right * 5 * Time.deltaTime);
			}else{
				Debug.Log ("collided");
				collided = true;
			}
		}
		//modifica dello slice
		if(collided){
			//Debug.Log("collided");
			//transform.position = new Vector3(transform.position.x, spostamenti[level] + (transform.position.y), -2.5f);
			//TODO sisemare perche deve resapwn a z = 5 e tornare fino a punto. con movimento continuo
			transform.position = new Vector3(transform.position.x, spostamenti[level], transform.position.z);
			Debug.Log(level);
			//transform.position = transform.position + spostamenti[level];
			cameracenter.GetComponent<CameraAdjustmentY>().adjust(level);
			changedlevel = false;
			collided = false;
			rotated = 2;
		}
		//TODO rotazione contrarea quando si arriva.

		/*if(changedlevel && (level >= 0 && level <= N_slice)){
			transform.position = new Vector3(transform.position.x, spostamenti[level], transform.position.z);
			//transform.position = transform.position + spostamenti[level];
			cameracenter.GetComponent<CameraAdjustmentY>().adjust(level);
			changedlevel = false;
		}*/

	}

	//metodo invocato dal controllore per attivarela procedua di spostamento sui layer
	public void movementOnZ(bool up_down){
		//direction = up_down;
		//if(level > 0&& level < N_slice){

		if(up_down && level > 0 && up.freePassage()){
			level --;
			rotdirection = 1;
			rotated = 0;
		}
		if(!up_down && level < N_slice-1 && down.freePassage()){
			level ++;
			rotdirection = -1;
			rotated = 0;
		}
		changedlevel = true;
		Debug.Log(level);
	}

	//metodo per la verifica di equivalenza di due vettori
	bool equalvec(Vector3 v1, Vector3 v2){
		//Debug.Log (v1 +" " + v2);
		//Debug.Log (v1.x + " " + v2.x);
		if(v1.x == v2.x){
			return true;
		}
		return false;
	}	
}
