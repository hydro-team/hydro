using UnityEngine;
using System.Collections;

public class RockManager : MonoBehaviour {

	public static RockManager Instance;


	public int currentslice;
	public GameObject[] rockPool;
	public bool[,] attached;
	public Vector3 pit;
	//public GameObject[] slices;
	public Vector3[,] positions;
	private int elementcount;
	private int numslice;

	void Awake(){
		Instance = this;
	}

	public RockManager getRockManager(){
		return Instance;
	}

	// Use this for initialization
	void Start () {
		/*slices = new GameObject[transform.childCount];
		for(int i = 0; i < transform.childCount; i++){
			slices[i] =transform.GetChild(i).gameObject;
		}*/
		numslice = transform.childCount;

		rockPool = GameObject.FindGameObjectsWithTag("indestructablerock");
		attached = new bool[numslice, rockPool.Length];
		Debug.Log (rockPool.Length);

		elementcount = 0;
		for(int i = 0; i < numslice; i++){
			int j = transform.GetChild(i).GetChild(0).childCount;
			if(j > elementcount){
				elementcount = j;
			}
		}
		positions= new Vector3[numslice,elementcount];
		for(int i = 0; i < numslice; i++){
			for(int j = 0; j < elementcount; j++){
				if(j < transform.GetChild(i).GetChild(0).childCount){
					positions[i,j] = transform.GetChild(i).GetChild(0).GetChild(j).transform.position;
				}else{
					positions[i,j] = pit;
				}
			}
		}
		//Debug.Log (numslice);
		//Debug.Log (elementcount);
		//Debug.Log(positions[0,0] + "|" + positions[0,1]);
		//Debug.Log(positions[1,0] + "|" + positions[1,1]);

		relocate(currentslice);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.W) && currentslice < (numslice - 1)){
			currentslice ++;
			relocate(currentslice);
		}
		if(Input.GetKeyUp(KeyCode.S) && currentslice > 0){
			currentslice --;
			relocate(currentslice);
		}
	}

	void relocate(int slice){
		for(int i = 0; i < elementcount; i++){
			if(i < rockPool.Length ){
				rockPool[i].SetActive(true);
				if(rockPool[i].GetComponent<HingeJoint2D>() != null){
					if(attached[slice,i]){
						rockPool[i].GetComponent<HingeJoint2D>().enabled = true;
					}else{
						rockPool[i].GetComponent<HingeJoint2D>().enabled = false;
					}
				}
				rockPool[i].transform.position = positions[slice, i];
			}
		}
		for(int i = elementcount; i < rockPool.Length; i++){
			if(rockPool[i].GetComponent<HingeJoint2D>() != null){
				rockPool[i].GetComponent<HingeJoint2D>().enabled = false;
			}
			rockPool[i].SetActive(false);
			rockPool[i].transform.position = pit;
		}
	}

	public void setAttached(GameObject rock){
		Debug.Log ("Set Attached called " + currentslice);
		for(int i = 0; i < rockPool.Length; i++){
			if(rockPool[i] == rock){
				attached[currentslice, i] = !attached[currentslice, i];
			}
			Debug.Log(attached[currentslice, i]);
		}
	}
}
