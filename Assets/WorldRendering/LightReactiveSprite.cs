using UnityEngine;
using System.Collections;

public class LightReactiveSprite : MonoBehaviour {

	public Sprite LightMap;
	public  Material SpriteLightMaterial;

	// Use this for initialization
	void Start () {
		MaterialPropertyBlock bloc = new MaterialPropertyBlock(); 
		Renderer rend = gameObject.GetComponent<Renderer> ();
		rend.material = SpriteLightMaterial;
		rend.GetPropertyBlock (bloc);
		bloc.AddTexture ("_LightTex", LightMap.texture);
		rend.SetPropertyBlock (bloc);
		Debug.Log (bloc.GetTexture("_MainTex").ToString());

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
