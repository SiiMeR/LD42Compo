using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeList : MonoBehaviour
{
	public List<GameObject> items;

	public GameObject itemPrefab;

	public GameObject listParent;
	
	private Player _player;
	
	void OnEnable ()
	{
		_player = FindObjectOfType<Player>();

		items = _player.upgrades
			.Where(upgrade => !upgrade.isEquipped)
			.Select(mapUpgradeToMenuItem).ToList();
		
		print(items.Count);
		if (items.Count > 0)
		{
			EventSystem.current.SetSelectedGameObject(items[0].gameObject);
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
	void Update () {
		
	}
}
