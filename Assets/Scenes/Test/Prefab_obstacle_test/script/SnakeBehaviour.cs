using UnityEngine;
using System.Collections;

public class SnakeBehaviour : MonoBehaviour {

	public float speed;
	public Vector3 center;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(Vector3.forward * Time.deltaTime * speed);
		transform.RotateAround(center, Vector3.forward, speed * Time.deltaTime);
	}
}
