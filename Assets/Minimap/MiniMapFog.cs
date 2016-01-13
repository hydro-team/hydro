using UnityEngine;
using System.Collections;

public class MiniMapFog : MonoBehaviour {

	public bool[] fogStatus;
	public GameObject[] fogs;

	// Use this for initialization
	void Start () {
	}

	public void onActive(){

		int level = MiniMapPositioning.instance.getLevel();
		int slice = MiniMapPositioning.instance.getSlice();
		fogStatus = MiniMapPositioning.instance.getFogStatus();
		fogs[level*2 + slice].GetComponent<FogManager>().setFog(fogStatus);
		for(int i = 0; i < fogs.Length; i++){
			if(i != (level*2 + slice)){
				fogs[i].GetComponent<FogManager>().deactivateFog();
			}
		}
	}
}
