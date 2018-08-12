using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToiletControl : MonoBehaviour
{


	public TextMeshProUGUI text;

	public Animator animator;
	
	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			text.gameObject.SetActive(true);

			if (Input.GetKeyDown(KeyCode.X))
			{
				StartCoroutine(Ignition());
			}
		}
	}

	public IEnumerator Ignition()
	{
		text.text = "";
		
		animator.SetTrigger("Dooropen");


		yield return null;



	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			text.gameObject.SetActive(false);
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			text.gameObject.SetActive(true);
			
			if (Input.GetKeyDown(KeyCode.X))
			{
				StartCoroutine(Ignition());
			}
		}

	}
}
