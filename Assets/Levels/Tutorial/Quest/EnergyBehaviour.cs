using UnityEngine;
using System.Collections;

public class EnergyBehaviour : MonoBehaviour {

	public Inventory inventory;


	public void OnTriggerEnter2D(Collider2D other){
		if (inventory.item == Items.EMPTY) {
			inventory.pickUp(Items.ENERGY, gameObject.GetComponent<SpriteRenderer> ().sprite, gameObject.GetComponent<SpriteRenderer> ().color);
			//inventory.item = Items.ENERGY;
			gameObject.GetComponent<Collider2D>().enabled = false;
			StartCoroutine("fade");
		}
	}
	public float fadeTime = 0.5f;
	IEnumerator fade(){
		SpriteRenderer renderp = gameObject.GetComponent<SpriteRenderer> ();
		float alpha = renderp.color.a;
		float count = 0f;
		while (count<fadeTime){
			count+= Time.deltaTime;
			renderp.color = new Color ( renderp.color.r,renderp.color.g,renderp.color.b, (1f-count/fadeTime)*alpha);
			yield return null;
		}
		this.enabled = false;
	}
}
