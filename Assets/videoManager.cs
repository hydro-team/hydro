using UnityEngine;
using System.Collections;

public class videoManager : MonoBehaviour {
	public Renderer r ;
	MovieTexture movie;
	// Use this for initialization
	void Start () {
		r = GetComponent<Renderer>();
		movie = (MovieTexture)r.material.mainTexture;	
		movie.Play();
		StartCoroutine(wait(movie.duration));
	}
	
	IEnumerator wait(float duration) {
		yield return new WaitForSeconds(duration);
		movie.Stop();
		//Application.loadedLevel();
	}
}
