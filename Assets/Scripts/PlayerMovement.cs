﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxController2D))]
public class PlayerMovement : MonoBehaviour
{

	public float minJumpHeight = 1f;
	public float maxJumpHeight = 4f;
	public float timeToJumpApex = .4f;

	public float moveSpeed = 10;

	public float accelerationTimeAirborne = .2f;
	public float accelerationTimeGrounded = .1f;

	private float _maxJumpVelocity;
	private float _minJumpVelocity;
	private float _velocityXSmoothing;
	
	private Vector3 _velocity;
	private BoxController2D _controller;

	private bool _hasJumped;
	
	// Use this for initialization
	void Start ()
	{
		_controller = GetComponent<BoxController2D>();

		var gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		
		Physics2D.gravity = new Vector3(gravity, 0, 0);

		_maxJumpVelocity = Mathf.Abs(gravity * timeToJumpApex);
		_minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale > .01f)
		{
			UpdateMovement();
		}
	}

	private void UpdateMovement()
	{
		if (_controller.collisions.above || _controller.collisions.below)
		{
			_velocity.y = 0;
			_hasJumped = false;
		}
		
		var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		
		Debug.DrawRay(transform.position, input, Color.red);

		if (Input.GetButtonDown("Jump") && _controller.collisions.below)
		{
			_velocity.y = _maxJumpVelocity;
			_hasJumped = true;
		}

		if (Input.GetButtonUp("Jump") && !_hasJumped)
		{
			if (_velocity.y > _minJumpVelocity)
			{
				_velocity.y = _minJumpVelocity;
			}
			
		}
		
		var targetVelocityX = input.x * moveSpeed;

		_velocity.x = Mathf.SmoothDamp(_velocity.x, targetVelocityX, ref _velocityXSmoothing, (_controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		
		_velocity.y += Physics2D.gravity.x * Time.deltaTime;
		
		_controller.Move(_velocity * Time.deltaTime);
		
	}
}