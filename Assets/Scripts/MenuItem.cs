using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
	public Upgrade upgrade;

	public TextMeshProUGUI upgradeNameText;
	
	// Use this for initialization
	void OnEnable()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnButtonPressed()
	{
		if (!upgrade) return;
		
		upgrade.OnAcquire.Invoke(FindObjectOfType<Player>());

		MemoryManager.Instance.AllocateMemory(upgrade.memoryCost);
		
		UpgradeList.Instance.selectNextUpgrade();
		
		Destroy(gameObject);
	}
}
