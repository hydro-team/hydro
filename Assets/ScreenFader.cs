using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	Image fade;
	bool state;
	bool direction;
	bool blockhalf;
	public static ScreenFader instance;
	// Use this for initialization
	void Start () {
		fade = GetComponent<Image>();
		fade.color = Color.clear;
		state = false;
		direction = true;
		blockhalf = false;
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
				fade.color = Color.Lerp(fade.color, Color.black, Time.deltaTime*3.5f);
			}else{
				fade.color = Color.Lerp(fade.color, Color.clear, Time.deltaTime*3.5f);
			}
			if(fade.color == Color.black && !blockhalf){
				direction = false;
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
	public void halfShadetoBlack(){
		state = true;
		blockhalf = true;
	}
}
