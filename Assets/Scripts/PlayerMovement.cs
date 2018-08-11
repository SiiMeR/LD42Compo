using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public float minJumpHeight = 1f;
	public float maxJumpHeight = 4f;
	public float timeToJumpApex = .4f;

	public float moveSpeed = 10;

	public float accelerationTimeAirborne = .2f;
	public float accelerationTimeGrounded = .1f;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
