using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MemoryManager : Singleton<MemoryManager> {
	
	public static readonly Color MEMORY_FREE = new Color32(246,240,248,255);
	public static readonly Color MEMORY_ALLOCATED = new Color32(82,124,108,255);
	public static readonly Color MEMORY_CORRUPTED = new Color32(153,117,119,255);
	public static readonly Color MEMORY_LEAKED = new Color32(8,20,30,255);
	public static readonly int   MEMORY_BLOCK_SIZE = 32;
	
	public List<GameObject> _memoryRows;

	private List<Image> _images;
	private Player _player;
	
	public Image legendFree;
	public Image legendAllocated;
	public Image legendCorrupted;
	public Image legendLeaked;
	// Use this for initialization
	void Start ()
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
		UpdateLeakedMemory();

		//_player.FreeMemory -= 1;
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
			return false;
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
			return false;
		}

		freeBlocks.Take(blocksToAllocate).ToList().ForEach(freeBlock => freeBlock.color = MEMORY_ALLOCATED);

		return true;
	}
	
	public void UpdateLeakedMemory()
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

	}
}
