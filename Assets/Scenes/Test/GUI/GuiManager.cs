using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {

	public GameObject MapPop;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Map(){
		MapPop.SetActive (!MapPop.activeSelf);
	}

	public void Quest(){
		
	}

	public void Options(){

	}
}
