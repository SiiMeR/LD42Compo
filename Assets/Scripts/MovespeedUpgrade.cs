using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovespeedUpgrade : Upgrade
{

	public int moveSpeedBonus = 3;
	public override void RemoveEffectFromPlayer(Player player)
	{
		player.GetComponent<PlayerMovement>().moveSpeed -= moveSpeedBonus;
	}

	public override void AddEffectToPlayer(Player player)
	{
		player.GetComponent<PlayerMovement>().moveSpeed += moveSpeedBonus;
		
	}
}
