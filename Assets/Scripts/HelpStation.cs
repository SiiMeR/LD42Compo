using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpStation : MonoBehaviour
{

	public RuntimeAnimatorController sadController;
	public RuntimeAnimatorController happyController;

	public GameObject helpSign;

	public GameObject helpModal;
	
	public string title;
	
	[TextArea(5, 10)] 
	public string description;

	public TextMeshProUGUI titleText;
	public TextMeshProUGUI descText;
	
	
	private Animator _animator;
	// Use this for initialization
	void Start ()
	{
		_animator = GetComponent<Animator>();

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
		helpModal.SetActive(!helpModal.activeInHierarchy);
		Time.timeScale = helpModal.activeInHierarchy ? 0.0f : 1.0f;

		if (helpModal.activeInHierarchy)
		{
			StartCoroutine(WaitForKeyPress());
		}
		
		
	}

	public IEnumerator WaitForKeyPress()
	{
		yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
		
		FlipDialogue();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			_animator.runtimeAnimatorController = happyController;
			helpSign.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			_animator.runtimeAnimatorController = sadController;
			helpSign.SetActive(false);
		}
	}
}
