using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {

	//FIXME
	public Sprite[] Emotions;
	public int index;
	public bool _feels;
	public bool change;
	public float duration;
	public SpriteRenderer sprite;
	
	bool showFeelings{
		get{return _feels;}
		set{
			if(value == true){
//				Emotions.transform.localScale = Vector3.one;
			}
			else{
//				foreach (GameObject feel in Emotions){
//					feel.transform.localScale = Vector3.zero;
//				}
			}
			_feels = value;
		}
	}


	// Use this for initialization
	void Start () {
		index = 0;
		sprite = GetComponent<SpriteRenderer>();
		sprite.sprite = Emotions[index];
		showFeelings = false;
		change = true;
		sprite.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(showFeelings && change){
				StartCoroutine(changeSprite());
		}
	}

	void OnTriggerEnter2D(Collider2D entered){
		if (entered.gameObject.tag == "Player") {
			sprite.enabled = true;
			showFeelings = true;
		}
	}

	void OnTriggerExit2D(Collider2D exited){
		if (exited.gameObject.tag == "Player") {
			showFeelings = false;
			
			sprite.enabled = false;
		}	
	}

	IEnumerator changeSprite() {
		change = false;
		yield return new WaitForSeconds(duration);
		index = (index + 1) % Emotions.Length;
		sprite.sprite = Emotions[index];
		change = true;
	}
}
