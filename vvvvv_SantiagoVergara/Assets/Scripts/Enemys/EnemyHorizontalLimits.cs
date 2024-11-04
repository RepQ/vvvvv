using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalLimits : MonoBehaviour
{
    private Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision.GetComponent<Enemy>();
        enemy.enemyVelocityX *= -1;
        enemy.transform.localScale = Vector3.Scale(enemy.transform.localScale, new Vector3(-1, 1, 1));
    }
}
