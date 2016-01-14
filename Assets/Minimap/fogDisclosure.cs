using UnityEngine;
using System.Collections;

public class fogDisclosure : MonoBehaviour {

	public static fogDisclosure instance;
	public Collider [] zones;
	public Collider [] zones_slice1;
	
	GameObject hydro;
	// Use this for initialization
	void Awake () {
		Debug.Log("Awake fogDis");
		if(instance == null){
			Debug.Log("instance = null");
			instance = this;
		}else{
			Debug.Log(instance);
			Destroy(this.gameObject);
		}
		hydro = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < zones.Length; i++){
			if(zones[i].bounds.Contains(hydro.transform.position)){
				MiniMapPositioning.instance.zoneActive(i);
				zones[i].enabled = false;
				return;
			}
		}
		for(int i = 0; i < zones_slice1.Length; i++){
			if(zones_slice1[i].bounds.Contains(hydro.transform.position)){
				MiniMapPositioning.instance.zoneActive(i);
				zones_slice1[i].enabled = false;
				return;
			}
		}
	}

	public int getZonesSize(){
		int slice = MiniMapPositioning.instance.getSlice();
		int level = MiniMapPositioning.instance.getLevel();
		zones = transform.GetChild(level*2).GetComponents<BoxCollider>();
		zones_slice1 = transform.GetChild(level*2 + 1).GetComponents<BoxCollider>();
		if(slice == 0){
			return zones.Length;
		}else{
			return zones_slice1.Length;
		}
	}

	public bool getStatusOne(int i, int slice){
		if(slice == 0){
			return zones[i].enabled;
		}else{
			return zones_slice1[i].enabled;
		}
	}
}
