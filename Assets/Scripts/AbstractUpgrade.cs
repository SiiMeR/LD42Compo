using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbstractUpgrade : MonoBehaviour
{

	private OnAcquireEvent ApplyUpgrade;
	private OnAcquireEvent DeApplyUpgrade;
	
	public string title;

	[TextArea(5, 10)] public string description;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public class OnAcquireEvent : UnityEvent<Player>
	{
        
	}
}
