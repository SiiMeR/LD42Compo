using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FakeLighting : MonoBehaviour
{

	public Material shaderMaterial;
	public Material higherResShaderMaterial;

	public float MaxLightRadius = 0.31f;
	public float SmallLightRadius = 0.27f;

	public float maxalpha = 0.4f;

	public int doTransition = 0;
	// Use this for initialization
	void Start ()
	{
		//StartCoroutine(FadeOutDarknessC());
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
			
		shaderMaterial.SetFloat("MaxLightRadius", MaxLightRadius);
		shaderMaterial.SetFloat("SmallLightRadius", SmallLightRadius);

		Graphics.Blit(src, rt,shaderMaterial);
		
		higherResShaderMaterial.SetFloat("maxalpha",maxalpha);
		higherResShaderMaterial.SetTexture("highResGamePlay", highResGamePlay);
		higherResShaderMaterial.SetTexture("LowResDarkness", rt);
		higherResShaderMaterial.SetInt("doTransition", doTransition);
		
		Graphics.Blit(src, dest, higherResShaderMaterial);
		
		RenderTexture.ReleaseTemporary(rt);
		RenderTexture.ReleaseTemporary(highResGamePlay);
	}

	public void FadeOutDarkness()
	{
		StartCoroutine(FadeOutDarknessC());
	}

	public IEnumerator FadeOutDarknessC()
	{

		yield return new WaitForSeconds(2.0f);
		

		doTransition = 1;
		var timer = 0f;


		var startrad = MaxLightRadius;
		var startsmall = SmallLightRadius;
		
		while ((timer += Time.fixedUnscaledDeltaTime) < 5f)
		{
			MaxLightRadius = Mathf.Lerp(startrad, 1.0f, timer / 5f);
			SmallLightRadius= Mathf.Lerp(startsmall, 1.0f, timer / 5f);

			yield return null;
		}
	}
}
