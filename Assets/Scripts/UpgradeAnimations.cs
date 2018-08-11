using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAnimations : MonoBehaviour {

	public Transform sunshine;
	public float animationAmplitude = 1f;
	public float animationPeriod = 1f;
	public float animationRotateSpeed = 30f;
	
	private Vector3 startPos;
	
	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update()
	{
		sunshine.Rotate(Vector3.back, animationRotateSpeed * Time.deltaTime);

		var theta = Time.timeSinceLevelLoad / animationPeriod;
		var distance = animationAmplitude * Mathf.Sin(theta);
		transform.position = startPos + Vector3.up * distance;
	}
}
