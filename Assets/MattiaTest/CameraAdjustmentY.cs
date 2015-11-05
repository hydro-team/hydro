using UnityEngine;
using System.Collections;

public class CameraAdjustmentY : MonoBehaviour {

	//Variabili del mondo
	public const int N_camera= 3; // numero di cameree usate
	public const int N_slice = 3; //numero di slice nel livello

	public float[] level_heights = new float[N_slice]; //array contenente le altezze delle origini dei diversi slice
	public int livello; // slice in cui si trova hydro

	public GameObject[] cameras = new GameObject[N_camera]; //array contenete le varie camere
	// Use this for initialization
	void Start () {
		livello = 0;
	}

	//metodo chiamto per correggere l'altezza delle camere quando hydro si sposta da un livello ad un altro
	public void adjust(int level){
		int j = -1;
		if(level > 0 && level < N_slice - 1){
			Debug.Log ("livelli intermedi");
			for(int i = 0; i < N_camera; i++){
				cameras[i].transform.position = new Vector3(transform.position.x, level_heights[level + j], transform.position.z);
				j++;
			}
		}else{
			if(level == 0){
				Debug.Log("livello 0");
				cameras[0].transform.position = new Vector3(transform.position.x, level_heights[0] + 100, transform.position.z);
				j++;
				for(int i = 1; i < N_camera; i++){
					cameras[i].transform.position = new Vector3(transform.position.x, level_heights[level + j], transform.position.z);
					j++;
				}
			}else{
				Debug.Log("livello finale");
				cameras[N_camera -1].transform.position = new Vector3(transform.position.x, level_heights[N_slice-1] + 100, transform.position.z);
				for(int i = 0; i < N_camera-1; i++){
					cameras[i].transform.position = new Vector3(transform.position.x, level_heights[level + j], transform.position.z);
					j++;
				}
			}
		}
	}
}
