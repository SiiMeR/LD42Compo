using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

	public GameObject credits;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnStartPressed()
	{
		SceneManager.LoadScene("Main");
	}

	public void OnCreditsPressed()
	{
		credits.SetActive(true);
	}

	public void OnExitPressed()
	{
		Application.Quit();
	}

}
