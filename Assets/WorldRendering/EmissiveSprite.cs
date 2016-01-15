using UnityEngine;
using System.Collections;

public class EmissiveSprite : MonoBehaviour {

	public Sprite Emission;
	public  Material SpriteLightMaterial;

	// Use this for initialization
	void Start () {
		MaterialPropertyBlock bloc = new MaterialPropertyBlock(); 
		Renderer rend = gameObject.GetComponent<Renderer> ();
		rend.material = SpriteLightMaterial;
		rend.GetPropertyBlock (bloc);
		bloc.AddTexture ("_EmissionTex",Emission.texture);
		rend.SetPropertyBlock (bloc);

	}

	// Update is called once per frame
	void Update () {

	}
}
