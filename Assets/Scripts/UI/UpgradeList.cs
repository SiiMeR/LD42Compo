using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeList : MonoBehaviour
{
	private List<Button> items;
	// Use this for initialization
	void Start ()
	{
		items = GetComponentsInChildren<Button>().ToList();

		if (items.Count > 0)
		{
			EventSystem.current.SetSelectedGameObject(items[0].gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
