using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCorruptionUpgrade : Upgrade
{

    public int corruptionRemovedOnUse = 1;

    public override void RemoveEffectFromPlayer(Player player)
    {
        base.RemoveEffectFromPlayer(player);
    }

    public override void AddEffectToPlayer(Player player)
    {
        MemoryManager.Instance.RemoveCorruptMemory(corruptionRemovedOnUse);
        base.AddEffectToPlayer(player);
        isEquipped = true;
        
    }
}
