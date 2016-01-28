using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	Image fade;
	bool state;
	bool direction;
	bool blockhalf;
	public static ScreenFader instance;
	string nextlevel;
	Color colorToFade;
	// Use this for initialization
	void Start () {
		fade = GetComponent<Image>();
		fade.color = Color.clear;
		state = false;
		direction = true;
		blockhalf = false;

		colorToFade = Color.black;

		if(instance == null){
			instance = this;
		}else{
			Destroy (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(state){
			if (direction){
				fade.color = Color.Lerp(fade.color, colorToFade, Time.deltaTime*3.5f);
			}else{
				fade.color = Color.Lerp(fade.color, Color.clear, Time.deltaTime*3.5f);
			}
			if(fade.color == colorToFade && blockhalf){
				Debug.Log("STOP AND GO TO NEXT LEVEL");
				direction = false;
				Application.LoadLevel(nextlevel);
			}
			if(fade.color == Color.clear){
				state = false;
			}
		}
	}
	public void activeFade(){
		state = true;
	}
	public Color fadeColor(){
		return fade.color;
	}
	public void halfShadetoBlack(string nextLevelname){
		colorToFade = Color.black;
		state = true;
		blockhalf = true;
		nextlevel = nextLevelname;
	}
	public void halfShadetoWhite(string nextLevelname){
		colorToFade = Color.white;
		state = true;
		blockhalf = true;
		nextlevel = nextLevelname;
	}
}
