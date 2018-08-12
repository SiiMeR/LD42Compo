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

	public List<Image> preAllocatedMemory;

	// Use this for initialization
	void OnEnable()
	{
		preAllocatedMemory?.ForEach(slot => { slot.color = MemoryManager.MEMORY_PREALLOCATED; });
	}
	
	// Update is called once per frame
	void Update ()
	{
		preAllocatedMemory?.ForEach(slot => { slot.color = MemoryManager.MEMORY_PREALLOCATED; });

	}

	public void OnButtonPressed()
	{
		if (!upgrade) return;
		
		upgrade.OnAcquire.Invoke(FindObjectOfType<Player>());

		if (preAllocatedMemory.Count == upgrade.memoryCost / MemoryManager.MEMORY_BLOCK_SIZE )
		{
			MemoryManager.Instance.TakePreAllocatedSlots(preAllocatedMemory);
		}
		else
		{
			Debug.LogWarning($"Preallocated count {preAllocatedMemory.Count} didnt match real cost {upgrade.memoryCost / MemoryManager.MEMORY_BLOCK_SIZE}");
			MemoryManager.Instance.CheckForFaultyBlocks();
			
			MemoryManager.Instance.AllocateMemory(upgrade.memoryCost);
		}
		
		UpgradeList.Instance.selectNextUpgrade();
		
		Destroy(gameObject);
	}
}
