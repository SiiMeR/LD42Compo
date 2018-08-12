using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToiletControl : MonoBehaviour
{

	public TextMeshProUGUI title;
	public TextMeshProUGUI author;
	public TextMeshProUGUI exit;
	
	
	public TextMeshProUGUI text;

	public Animator animator;

	public ParticleSystem ps;
	// Use this for initialization
	void Start ()
	{
		ps = GetComponentInChildren<ParticleSystem>();
		ps.Stop();
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

		//AudioManager.Instance.FadeToNextMusic("02Departure", 8f);

		
		var pos = new Vector2(7f, 83.75f);

		var player = FindObjectOfType<Player>();

		player.GetComponent<PlayerMovement>().enabled = false;
		player.GetComponent<PlayerMovement>()._animatorController.SetBool("Walking", true);
		var startPos = player.transform.position;

		var timer = 0f;

		while ((timer += Time.unscaledDeltaTime) < 1.5f)
		{
			player.transform.position = Vector3.Lerp(startPos, pos, timer / 1.5f);
			yield return null;
		}
		
		player.GetComponent<PlayerMovement>()._animatorController.SetBool("Walking", false);


		player.GetComponent<SpriteRenderer>().flipX = false;
		
		
		animator.SetTrigger("DoorOpen");

		var rdpc = animator.GetCurrentAnimatorStateInfo(0).length +
		           animator.GetCurrentAnimatorStateInfo(0).normalizedTime - 0.5f;
		
		
		yield return new WaitForSeconds(rdpc);
		
		var nextPos = new Vector2(7.3f, 84.75f);
		
		player.GetComponent<PlayerMovement>()._animatorController.SetBool("Walking", true);
		
		startPos = player.transform.position;

		timer = 0f;

		while ((timer += Time.unscaledDeltaTime) < 1.5f)
		{
			player.transform.position = Vector3.Lerp(startPos, nextPos, timer / 1.5f);
			yield return null;
		}
		
		player.GetComponent<PlayerMovement>()._animatorController.SetBool("Walking", false);


		player.GetComponent<SpriteRenderer>().sortingOrder = -1000;
		
		animator.SetTrigger("DoorClosed");

		 rdpc = animator.GetCurrentAnimatorStateInfo(0).length +
		           animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
		
		
		yield return new WaitForSeconds(rdpc);
		
		var psStart = 30;
		var psEnd = 50;
		
		ps.Play();

		var startVel = 0;
		var endVel = 10;

		var sPos  = transform.position;
		
		timer = 0;

		player.transform.parent = transform;

		Camera.main.GetComponent<CameraShake>().enabled = true;
		while ((timer += Time.unscaledDeltaTime) < 30f)
		{
			var main = ps.main;
			main.simulationSpeed = Mathf.Lerp(psStart, psEnd, timer / 30f);

			var t = timer / 30f;

			t = t * t * t * t;

			transform.position = Vector3.Lerp(sPos, new Vector3(sPos.x,sPos.y + 100, sPos.z), t);

			yield return null;
		}

		timer = 0;

		while ((timer += Time.unscaledDeltaTime) < 5f)
		{
			var c = title.color;
			c.a =  Mathf.Lerp(0f, 1f, timer / 5f);
			title.color = c;
			yield return null;
		}

		timer = 0;
		while ((timer += Time.unscaledDeltaTime) < 5f)
		{
			var c = author.color;
			c.a =  Mathf.Lerp(0f, 1f, timer / 5f);
			author.color = c;
			yield return null;
		}
		
		yield return new WaitForSeconds(2.0f);
		
		timer = 0;
		while ((timer += Time.unscaledDeltaTime) < 5f)
		{
			var c = author.color;
			c.a =  Mathf.Lerp(1f, 0f, timer / 5f);
			author.color = c;
			yield return null;
		}
			
		
		timer = 0;
		while ((timer += Time.unscaledDeltaTime) < 5f)
		{
			var c = exit.color;
			c.a =  Mathf.Lerp(0f,1f, timer / 5f);
			exit.color = c;
			yield return null;
		}


		yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape));
		
		Application.Quit();

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
