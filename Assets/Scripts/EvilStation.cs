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
		yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
		
		FlipDialogue();
	}

}
