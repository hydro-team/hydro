using UnityEngine;
using System.Collections;

public class GuiManager : MonoBehaviour {

	public GameObject MapPop;
	public GameObject QuestPop;
	public GameObject OptionPop;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	bool MapOpen = false;
	public void Map(){
		openPop (MapPop, ref MapOpen);
	}
	bool QuestOpen= false;
	public void Quest(){
		openPop (QuestPop,ref QuestOpen);
	}
	bool OptionsOpen = false;
	public void Options(){
		openPop (OptionPop,ref OptionsOpen);
	}

	void closeAll(){
		MapPop.SetActive (false);
		QuestPop.SetActive (false);
		OptionPop.SetActive (false);

	}

	void openPop( GameObject popup, ref bool PopBool){
		closeAll ();
		if (!PopBool) {
			popup.SetActive(true);
		}

		PopBool = !PopBool;
		Debug.Log (PopBool);
	}
}
