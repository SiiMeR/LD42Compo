using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
	[SerializeField] private GameObject textPanel;
	
	public Transform sunshine;
	public float animationAmplitude = 10f;
	public float animationPeriod = 5f;
	public float animationRotateSpeed = 0.5f;
	

	
	private Vector3 startPos;
	
	
	// Use this for initialization
	void Start ()
	{
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		sunshine.Rotate(Vector3.back, animationRotateSpeed);
		
		var theta = Time.timeSinceLevelLoad / animationPeriod;
		var distance = animationAmplitude * Mathf.Sin(theta);
		transform.position = startPos + Vector3.up * distance;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			AcquireItem(other.gameObject.GetComponent<Player>());
		}
	}

	private void AcquireItem(Player player)
	{
		textPanel.SetActive(true);
	}
}
