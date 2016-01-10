using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PausedMenu : MonoBehaviour {

	public GameObject pausePanel;
	public bool isPaused;

	// Use this for initialization
	void Start () {
		isPaused = false;
	}
	
	// Update is called once per frame
	/**void Update () {
		if(isPaused){
			PauseGame(true);
		}else{
			PauseGame(false);
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			switchPause();
		}
	}**/

	void PauseGame(bool state){
		if(state){
			Time.timeScale = 0; //paused game
		}else{
			Time.timeScale = 1; //unpausedGame
		}
		pausePanel.SetActive(state);
	}

	public void switchPause(){
		isPaused = !isPaused;
		PauseGame(isPaused);
	}
}
