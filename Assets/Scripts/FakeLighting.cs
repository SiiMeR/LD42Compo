﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

[ExecuteInEditMode]
public class FakeLighting : MonoBehaviour
{

	public Material shaderMaterial;
	public Material higherResShaderMaterial;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		
		RenderTexture rt = RenderTexture.GetTemporary(src.width / 16, src.height / 16, 0,RenderTextureFormat.ARGB32);
		rt.filterMode = FilterMode.Point;

		RenderTexture highResGamePlay = RenderTexture.GetTemporary(src.width, src.height, 0 , RenderTextureFormat.ARGB32);
		
		Graphics.Blit(src, highResGamePlay);


		Graphics.Blit(src, rt,shaderMaterial);
		
		higherResShaderMaterial.SetTexture("highResGamePlay", highResGamePlay);
		higherResShaderMaterial.SetTexture("LowResDarkness", rt);
		
		Graphics.Blit(src, dest, higherResShaderMaterial);
		RenderTexture.ReleaseTemporary(rt);
	}
}
