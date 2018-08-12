using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeList : Singleton<UpgradeList>
{
	public List<GameObject> items;

	public GameObject itemPrefab;

	public GameObject listParent;

	public TextMeshProUGUI noItemsText;
	private Player _player;

	private IEnumerator flashSlots;
	
	void OnEnable ()
	{

		
		
		_player = FindObjectOfType<Player>();

		items = _player.upgrades
			.Where(upgrade => !upgrade.isEquipped)
			.Select(mapUpgradeToMenuItem).ToList();
		
		if (items.Count > 0)
		{
			EventSystem.current.SetSelectedGameObject(items[0].gameObject);
			CheckMenuItem();
			noItemsText.gameObject.SetActive(false);
		}
		else
		{
			noItemsText.gameObject.SetActive(true);
		}
		
		

	}

	public void selectNextUpgrade()
	{
		if (items.Count > 0)
		{
			EventSystem.current.SetSelectedGameObject(items[0].gameObject);
			CheckMenuItem();
		}
	}
	void OnDisable()
	{
		items.ForEach(Destroy);
	}
	

	GameObject mapUpgradeToMenuItem(Upgrade upgrade)
	{
		var item = Instantiate(itemPrefab);

		item.GetComponent<MenuItem>().upgrade = upgrade;
		
		item.GetComponent<MenuItem>().upgradeNameText.text = upgrade.upgradeName;
		
		item.transform.SetParent(listParent.transform);
		
		return item;
	}
	
	// Update is called once per frame
	void Update ()
	{
		

	}

	private void CheckMenuItem()
	{
		
		if (EventSystem.current.currentSelectedGameObject.GetComponent<MenuItem>() != null)
		{
			var currentMenuItem = EventSystem.current.currentSelectedGameObject.GetComponent<MenuItem>();

			var cost = currentMenuItem.upgrade.memoryCost;
			
			var slotsNeeded = (cost / MemoryManager.MEMORY_BLOCK_SIZE);

			var freeSlots = MemoryManager.Instance.SelectForAllocation(slotsNeeded);

			if (freeSlots != null)
			{
				if (flashSlots != null)
				{
					
					StopCoroutine(flashSlots);
				}
			
				flashSlots = FlashSlots(freeSlots);

				StartCoroutine(flashSlots);
			}
		}
	}

	private IEnumerator FlashSlots(List<Image> freeSlots)
	{
		var timer = 0f;

		var startColor = freeSlots[0].color;

		while ((timer += Time.unscaledDeltaTime) < 5f)
		{
			freeSlots.ForEach(slot =>
			{
				slot.color = Color.Lerp(startColor, Color.yellow, Mathf.PingPong(Time.time, 1));
			});

			yield return null;
		}
		
		freeSlots.ForEach(slot =>
		{
			slot.color = startColor;
		});
	}
}
