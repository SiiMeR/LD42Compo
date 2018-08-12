using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveDarkness : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Camera.main.GetComponent<FakeLighting>().FadeOutDarkness();
			
			AudioManager.Instance.StopAllMusic();
			AudioManager.Instance.Play("02Departure", 1f, true);
		}
	}
}
