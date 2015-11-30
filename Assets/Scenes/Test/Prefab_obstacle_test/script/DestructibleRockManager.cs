using UnityEngine;
using System.Collections;

public class DestructibleRockManager : MonoBehaviour {

	public static DestructibleRockManager Instance;


	public int currentslice;
	public GameObject[] rockPool;
	private bool[,] attached;
	private float[,] lives;
	private bool[,] active;
	public float delay;
	public Vector3 pit;
	//public GameObject[] slices;
	public Vector3[,] positions;
	private int elementcount;
	private int numslice;

	void Awake(){
		Instance = this;
	}

	public DestructibleRockManager getDestructibleRockManager(){
		return Instance;
	}

	// Use this for initialization
	void Start () {
		/*slices = new GameObject[transform.childCount];
		for(int i = 0; i < transform.childCount; i++){
			slices[i] =transform.GetChild(i).gameObject;
		}*/
		numslice = transform.childCount;

		rockPool = GameObject.FindGameObjectsWithTag("destructiblerock");
		attached = new bool[numslice, rockPool.Length];
		lives = new float[numslice, rockPool.Length];
		for(int i = 0; i < numslice; i++){
			for(int j = 0; j < rockPool.Length; j++){
				lives[i,j] = 5f;
			}
		}
		active = new bool[numslice, rockPool.Length];
		Debug.Log (rockPool.Length);

		elementcount = 0;
		for(int i = 0; i < numslice; i++){
			int j = transform.GetChild(i).GetChild(1).childCount;
			if(j > elementcount){
				elementcount = j;
			}
		}
		positions= new Vector3[numslice,elementcount];
		for(int i = 0; i < numslice; i++){
			for(int j = 0; j < elementcount; j++){
				if(j < transform.GetChild(i).GetChild(1).childCount){
					positions[i,j] = transform.GetChild(i).GetChild(1).GetChild(j).transform.position;
				}else{
					positions[i,j] = pit;
				}
			}
		}
		Debug.Log (numslice);
		Debug.Log (elementcount);
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
//			rockPool[i].GetComponent<SmallRock>().setLife(lives[currentslice, i]);
		}
		for(int i = elementcount; i < rockPool.Length; i++){
			if(rockPool[i].GetComponent<HingeJoint2D>() != null){
				rockPool[i].GetComponent<HingeJoint2D>().enabled = false;
			}
			rockPool[i].SetActive(false);
			rockPool[i].transform.position = pit;
		//	rockPool[i].GetComponent<SmallRock>().setLife(lives[currentslice, i]);
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

	public void setIncative(GameObject rock){
		Debug.Log ("Set Inactive called " + currentslice);
		for(int i = 0; i < rockPool.Length; i++){
			if(rockPool[i] == rock){
				active[currentslice, i] = false;
				Debug.Log("coroutinestart");
				StartCoroutine(respawn(i, currentslice, delay));
				if(rockPool[i].GetComponent<HingeJoint2D>() != null){
					Destroy(rockPool[i].GetComponent<HingeJoint2D>());
					setAttached(rockPool[i]);
				}
			}
		}
		rock.SetActive(false);
	}

	IEnumerator respawn(int i, int slice, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		Debug.Log ("respawned " + i + " " + slice + "life " + lives[slice,i]);
		lives[slice, i] = 5f;
		active[slice, i] = true;
		//rockPool[i].GetComponent<SmallRock>().setLife(5);
		rockPool[i].SetActive(true);

	}
}
