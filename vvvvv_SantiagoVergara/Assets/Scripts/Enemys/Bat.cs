using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        EnemyMovement();
    }
}
