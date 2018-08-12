using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EvilStation : MonoBehaviour
{
	

	public GameObject evilModal;
	
	public string title;
	
	[TextArea(5, 10)] 
	public string description;

	public TextMeshProUGUI titleText;
	public TextMeshProUGUI descText;

	public GameObject evilsign;
	public MemoryManager memoryManager;
	// Use this for initialization
	void Start ()
	{

		titleText.text = title;
		descText.text = description;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (Input.GetKeyDown(KeyCode.X))
			{
				FlipDialogue();
			}
		}
		
		if (other.gameObject.CompareTag("Player"))
		{
			evilsign.SetActive(true);
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (Input.GetKeyDown(KeyCode.X))
			{
				FlipDialogue();
			}
		}
	}

	private void FlipDialogue()
	{
		evilModal.SetActive(!evilModal.activeInHierarchy);
		Time.timeScale = evilModal.activeInHierarchy ? 0.0f : 1.0f;

		if (evilModal.activeInHierarchy)
		{
			memoryManager.CorruptMemory();
			StartCoroutine(WaitForKeyPress());
		}
		
		
	}

	public IEnumerator WaitForKeyPress()
	{
		yield return new WaitForEndOfFrame();
		
		yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.X));
		
		FlipDialogue();
	}



	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			evilsign.SetActive(false);
		}
	}
}
