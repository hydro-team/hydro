using UnityEngine;
using System.Collections;

public class TestLight : MonoBehaviour {

	public Sprite LightMap;

	// Use this for initialization
	void Start () {
		MaterialPropertyBlock bloc = new MaterialPropertyBlock(); 
		Renderer rend = gameObject.GetComponent<Renderer> ();
		rend.GetPropertyBlock (bloc);
		bloc.AddTexture ("_LightTex", LightMap.texture);
		rend.SetPropertyBlock (bloc);
		Debug.Log (bloc.GetTexture("_MainTex").ToString());

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
