using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    public override void Update()
    {
        base.Update();
        EnemyMovement();
    }
}
