using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVerticalLimits : MonoBehaviour
{
    private Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.GetComponent<Enemy>();
        enemy.enemyVelocityY *= -1;
    }
}
