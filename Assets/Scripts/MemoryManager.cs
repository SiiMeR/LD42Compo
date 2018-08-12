using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MemoryManager : Singleton<MemoryManager> {
	
	public static readonly Color MEMORY_FREE = new Color32(246,240,248,255);
	public static readonly Color MEMORY_ALLOCATED = new Color32(82,124,108,255);
	public static readonly Color MEMORY_CORRUPTED = new Color32(153,117,119,255);
	public static readonly Color MEMORY_LEAKED = new Color32(8,20,30,255);
	public static readonly Color MEMORY_PREALLOCATED = Color.yellow;
	
	public static readonly int   MEMORY_BLOCK_SIZE = 32;
	
	public List<GameObject> _memoryRows;

	public  List<Image> _images;
	private Player _player;
	
	public Image legendFree;
	public Image legendAllocated;
	public Image legendCorrupted;
	public Image legendLeaked;
	// Use this for initialization
	public void Start ()
	{
		_player = FindObjectOfType<Player>();
		_images = new List<Image>();
		
		legendFree.color = MEMORY_FREE;
		legendAllocated.color = MEMORY_ALLOCATED;
		legendCorrupted.color = MEMORY_CORRUPTED;
		legendLeaked.color = MEMORY_LEAKED;

		_memoryRows.ForEach(memoryRow => _images.AddRange(memoryRow.GetComponentsInChildren<Image>().ToList()));

		_images.ForEach(image => image.color = MEMORY_FREE);
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateFreeMemory();


		//_player.FreeMemory -= 1;
	}

	private void UpdateFreeMemory()
	{
		FindObjectOfType<Player>().FreeMemory = _images.Count(image => image.color == MEMORY_FREE || image.color == MEMORY_PREALLOCATED) * MEMORY_BLOCK_SIZE ;
	}

	public void CheckForFaultyBlocks()
	{
		_images
			.Where(image => !image.color.Equals(MEMORY_FREE) && !image.color.Equals(MEMORY_ALLOCATED) 
			                                           && !image.color.Equals(MEMORY_CORRUPTED) && !image.color.Equals(MEMORY_LEAKED))
			.ToList()
			.ForEach(image => image.color = MEMORY_FREE);
	}

	// one block
	public void CorruptMemory()
	{
		var freeBlocks = _images.Where(image => image.color.Equals(MEMORY_FREE)).ToList();
		if (freeBlocks.Count <= 1)
		{
			return;
		}

		var idx = Random.Range(0, freeBlocks.Count);
		
		freeBlocks.ElementAt(idx).color = MEMORY_CORRUPTED;
	}

	public bool FreeMemory(int amount)
	{
		if (amount < 32)
		{
			amount = 32;
		}
		
		var blocksToFree = (amount / MEMORY_BLOCK_SIZE);
		
		var allocBlocks = _images.Where(image => image.color.Equals(MEMORY_ALLOCATED)).ToList();

		if (allocBlocks.Count < blocksToFree)
		{
			Debug.LogWarning("Not enough blocks");
		}
		
		allocBlocks.Take(blocksToFree).ToList().ForEach(freeBlock => freeBlock.color = MEMORY_FREE);

		return true;
	}

	// removes as many as it can upto max limit
	public void RemoveCorruptMemory(int blocksToRemove)
	{
		var corruptBlocks = _images.Where(image => image.color.Equals(MEMORY_CORRUPTED)).ToList();

		if (corruptBlocks.Count >= blocksToRemove)
		{
			corruptBlocks.Take(blocksToRemove).ToList().ForEach(block => block.color = MEMORY_FREE);
			
			return;
		}
		
		corruptBlocks.ForEach(block => block.color = MEMORY_FREE);
		
		

	}
	
	// promise to not use them :)
	public List<Image> SelectForAllocation(int amountOfBlocks)
	{
		CheckForFaultyBlocks();
			
		var freeBlocks = _images.Where(image => image.color.Equals(MEMORY_FREE)).ToList();
		
		if (freeBlocks.Count() < amountOfBlocks) return null;
		
		var imgs = freeBlocks.Take(amountOfBlocks).ToList();
		
		imgs.ForEach(block => block.color = Color.white);
		
		return imgs;
	}

	public bool TakePreAllocatedSlots(List<Image> slots)
	{
		CheckForFaultyBlocks();
		//FindObjectOfType<Player>().FreeMemory -= slots.Count * MEMORY_BLOCK_SIZE;
		slots.ForEach(slot => slot.color = MEMORY_ALLOCATED);
		
		return true;
	}
	
	
	
	public bool AllocateMemory(int amount)
	{
		if (amount < 32)
		{
			amount = 32;
		}
		
		var blocksToAllocate = (amount / MEMORY_BLOCK_SIZE);
		
		var freeBlocks = _images.Where(image => image.color.Equals(MEMORY_FREE)).ToList();
		
		if (freeBlocks.Count < blocksToAllocate)
		{
			Debug.LogWarning("Not enough blocks");
		}

	//	FindObjectOfType<Player>().FreeMemory -= amount;
		freeBlocks.Take(blocksToAllocate).ToList().ForEach(freeBlock => freeBlock.color = MEMORY_ALLOCATED);

		return true;
	}

	public void LeakOneBlockOfMemory()
	{
		for (var i = _images.Count -1 ; i >= 0; --i)
		{
			var img = _images[i].color == MEMORY_FREE;

			if (!img) continue;
			
			_images[i].color = MEMORY_LEAKED;
			break;
		}
	}
	
	/*public void UpdateLeakedMemory()
	{
		
		var currentMemory = _player.FreeMemory;
		var totalMemory = _player.totalMemory;
		
		currentMemory >>= 5;
		currentMemory <<= 5;

		var blocksLeaked = (totalMemory - currentMemory) / MEMORY_BLOCK_SIZE;
		
		for (var i = _images.Count - 1; i > _images.Count - blocksLeaked; i--)
		{
			_images[i].color = MEMORY_LEAKED;
		}

	}*/
}
