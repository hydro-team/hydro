using UnityEngine;
using System.Collections;
using System;

public class GuiManager : MonoBehaviour {

	public GameObject[] Buttons;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
	bool MapOpen = false;
	public void Map(){
		openPop (UIEnum.MAP);
	}
	bool QuestOpen= false;
	public void Quest(){
		openPop (UIEnum.QUEST);
	}
	bool OptionsOpen = false;
	public void Options(){
		openPop (UIEnum.OPTIONS);
	}

	void openPop( UIEnum pushed){
		GameObject go;
		foreach (UIEnum en in Enum.GetValues(typeof(UIEnum))){
			go = Buttons [(int)en];
			if(!go.activeSelf && pushed == en){
				go.SetActive(true);
				}else{
					go.SetActive(false);
				}
			}
		}

		private enum UIEnum{
			MAP,
			QUEST,
			OPTIONS
		}
	}
