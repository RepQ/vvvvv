using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineDamage : Enemy
{
    public float maxDamage;
    public float minDamage;

    public override void DamagePlayer()
    {
        base.DamagePlayer();
        player.playerLife -= (Random.Range(minDamage, maxDamage));
    }
}
