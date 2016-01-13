using UnityEngine;
using System.Collections;

public class MiniMapPositioning : MonoBehaviour {

	public static MiniMapPositioning instance;
	public Sprite[] levels_image;
	int currLevel;
	public WorldManager wm;
	public int slice;
	private int index;
	public bool[] zones;

	// Use this for initialization
	void Start () {
		if(instance == null){
			instance = this;
		}else{
			Destroy(this.gameObject);
		}
		currLevel = 0;
		index = 0;
		zones = new bool[fogDisclosure.instance.getZonesSize()];
		for(int i = 0; i < zones.Length; i++){
			zones[i] = zones[i] = fogDisclosure.instance.getStatusOne(i, slice);;
		}
	}

	public Sprite getLevelImage(){
		if(wm.CurrentSliceIndex != slice){
			chagedSlice();
		}
		slice = wm.CurrentSliceIndex;
		Debug.Log(slice);
		index = currLevel * 2;
		if(slice == 1){
			index ++;
		}

		return(levels_image[index]);
	}
	public bool[] getFogStatus(){
		chagedSlice();
		return zones;
	}

	public void changedLevel(int lv){
		if(currLevel != lv){
			zones = new bool[fogDisclosure.instance.getZonesSize()];
			for(int i = 0; i < zones.Length; i++){
				zones[i] = fogDisclosure.instance.getStatusOne(i, slice);
			}
			currLevel = lv;
		}
		
	}
	public int getSlice(){
		return slice;
	}
	public int getLevel(){
		return currLevel;
	}
	public void zoneActive(int i){
		zones[i] = false;
	}
	void chagedSlice(){
		zones = new bool[fogDisclosure.instance.getZonesSize()];
		for(int i = 0; i < zones.Length; i++){
			zones[i] = zones[i] = fogDisclosure.instance.getStatusOne(i, slice);;
		}
	}
}
