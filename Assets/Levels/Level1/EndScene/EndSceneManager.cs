using UnityEngine;
using System.Collections;

public class EndSceneManager : MonoBehaviour {

	public void goBacktoMainMenu(){
		Application.LoadLevel("MainMenu");
	}
}
