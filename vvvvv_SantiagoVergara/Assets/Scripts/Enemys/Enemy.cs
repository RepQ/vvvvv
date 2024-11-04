using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D enemyRigidBody;
    [Header("Enemy Stats")]
    public float enemyVelocityX;
    public float enemyVelocityY;
    public float enemyDamage;
    public float enemyLife;
    public float enemyPushPlayer;
    public float maxDamage;
    public float minDamage;
    public String limitLayer;
    // Start is called before the first frame update
    public virtual void Start()
    {
        if (GetComponent<Rigidbody2D>() != null)
            enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    public virtual void EnemyMovement()
    {
        enemyRigidBody.velocity = new Vector2(enemyVelocityX, enemyVelocityY);
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Player player;
            player = collider.gameObject.GetComponent<Player>();
            DamagePlayer(player);
        }
    }
    public virtual void DamagePlayer(Player player)
    {
        player.playerLife -= (UnityEngine.Random.Range(minDamage, maxDamage));
        player.animatorPlayer.SetBool("isHit", true);
        player.isHit = true;
    }

    public void Update()
    {
        if (enemyRigidBody != null)
            EnemyMovement();
    }
}
