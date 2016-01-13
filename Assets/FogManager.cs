using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FogManager : MonoBehaviour {

	public Image [] elements;
	// Use this for initialization
	void Start () {
	}

	public void setFog(bool [] status){
		for(int i = 0; i < status.Length; i++){
			elements[i].enabled = status[i];
		}
	}

	public void deactivateFog(){
		for(int i =0; i < elements.Length; i++){
			elements[i].enabled = false;
		}
	}
}
