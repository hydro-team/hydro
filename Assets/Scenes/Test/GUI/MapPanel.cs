using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapPanel : MonoBehaviour
{

	public WorldManager worldManager;
	public Text MapTitle;
	// Use this for initialization
	void Start (){
	
	}
	
	// Update is called once per frame
	void Update (){
	
	}

	void onEnable (){
		//TODO levels.get level
		if (enabled) {
			MapTitle.text = (worldManager.CurrentSliceIndex + 1).ToString ();
		}
	}
}
