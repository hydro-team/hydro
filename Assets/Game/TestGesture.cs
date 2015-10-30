using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestGesture : MonoBehaviour,GestureEndObserver {
	public Text text;
	// Use this for initialization
	void Start () {
		GestureRecogniser.Recogniser.subscribe (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void notify(Gesture gesture){
		text.text = gesture.Type.ToString();
	}

	public void notifyProgress(Gesture gesture){

	}
}
