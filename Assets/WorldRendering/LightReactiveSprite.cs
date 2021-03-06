﻿using UnityEngine;
using System.Collections;

public class LightReactiveSprite : MonoBehaviour {

	public Sprite Light;
	public  Material SpriteLightMaterial;

	// Use this for initialization
	void Start () {
		MaterialPropertyBlock bloc = new MaterialPropertyBlock(); 
		Renderer rend = gameObject.GetComponent<Renderer> ();
		rend.material = SpriteLightMaterial;
		rend.GetPropertyBlock (bloc);
		bloc.AddTexture ("_LightTex",Light.texture);
		rend.SetPropertyBlock (bloc);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
