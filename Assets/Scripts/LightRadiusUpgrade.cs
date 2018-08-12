using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRadiusUpgrade : Upgrade
{
    public float lightRadiusBonus = 0.05f;
    
    public override void RemoveEffectFromPlayer(Player player)
    {
        Camera.main.GetComponent<FakeLighting>().MaxLightRadius -= lightRadiusBonus;
        Camera.main.GetComponent<FakeLighting>().SmallLightRadius -= lightRadiusBonus;
    }

    public override void AddEffectToPlayer(Player player)
    {
        Camera.main.GetComponent<FakeLighting>().MaxLightRadius += lightRadiusBonus;
        Camera.main.GetComponent<FakeLighting>().SmallLightRadius += lightRadiusBonus;
    }
}
