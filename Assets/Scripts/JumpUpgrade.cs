using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUpgrade : Upgrade
{

	public float bonusJumpHeight = 1f;


	public override void RemoveEffectFromPlayer(Player player)
	{
		player.gameObject.GetComponent<PlayerMovement>().MaxJumpHeight -= bonusJumpHeight;
		base.RemoveEffectFromPlayer(player);
	}

	public override void AddEffectToPlayer(Player player)
	{
		isEquipped = true;
		player.gameObject.GetComponent<PlayerMovement>().MaxJumpHeight += bonusJumpHeight;
		base.AddEffectToPlayer(player);
	}
}
