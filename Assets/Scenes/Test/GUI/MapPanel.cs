using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapPanel : MonoBehaviour
{

	public WorldManager worldManager;
	public Text MapTitle;
	public Image Map;

	public WaterTrigger[] levels;


	// Use this for initialization
	void Start (){

	}
	
	// Update is called once per frame
	void Update (){
	
	}

	public void activePanel (){
		if (enabled) {
			//MapTitle.text = (worldManager.CurrentSliceIndex + 1).ToString ();
			MapTitle.text = "Act " + (worldManager.CurrentSliceIndex + 1).ToString();
			Sprite s = MiniMapPositioning.instance.getLevelImage();
			Map.GetComponent<Image>().sprite = s;
		}
	}
}