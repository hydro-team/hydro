using UnityEngine;
using System.Collections;

public class WaterColorManager : MonoBehaviour {
	public const byte tubeImportance = 3;
	public const byte spotImportance = 1;

	public static WaterColorManager instance;

	public BoxCollider[] triggers;
	public SpriteRenderer [] water;

	public Color startColor;
	public Color endColor;
	public Color curColor;

	public int [] spots;
	public int currLevel;
	public Vector3 increment;

	// Use this for initialization
	void Start () {
		if(instance == null){
			instance = this;
		}else{
			Destroy(this.gameObject);
		}

		//currLevel = 0;

		spots = new int[triggers.Length];
		GameObject[] spot, tube;
		spot = GameObject.FindGameObjectsWithTag("PollutionSpot");
		tube = GameObject.FindGameObjectsWithTag("Tube");
		for(int trig = 0; trig < triggers.Length; trig++){
			Bounds bounds = triggers[trig].bounds;
			for(int i = 0; i < spot.Length; i++){
				if(bounds.Contains(spot[i].transform.position)){
					spots[trig]+= spotImportance;
				}
			}
			for(int i = 0; i < tube.Length; i++){
				if(bounds.Contains(tube[i].transform.position)){
					spots[trig]+= tubeImportance;
				}
			}
		}
		changeLevelWaterColor();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log((Color32)water[0].color);
		water[0].color = Color.Lerp(water[0].color, curColor, Time.deltaTime*2);
		water[1].color = Color.Lerp(water[1].color, curColor, Time.deltaTime*2);
		//Debug.Log((Color32)water[0].color);
	}

	public void eatenSpot(){
		//Debug.Log ("EATEN");
		updateColor(spotImportance);
	}

	public void brokenTube(){
		updateColor(tubeImportance);
	}

	private void updateColor(int importance){
		curColor.r += increment.x * importance;
		curColor.g += increment.y * importance;
		curColor.b += increment.z * importance;
		//water[0].color = curColor;
		//water[1].color = curColor;
	}

	void changeLevelWaterColor(){
		int slice = spots[currLevel];
		if(slice == 0){
			curColor= endColor;
		}else{
			curColor = startColor;
			increment.x = (endColor.r - startColor.r)/slice;
			increment.y = (endColor.g - startColor.g)/slice;
			increment.z = (endColor.b - startColor.b)/slice;
		}
		water[0].color = curColor;
		water[1].color = curColor;
	}

	public void changedLevel(int lv){
		//Debug.Log ("CALLED " + lv);
		if(currLevel != lv){
			currLevel = lv;
			changeLevelWaterColor();
		}

	}
}
