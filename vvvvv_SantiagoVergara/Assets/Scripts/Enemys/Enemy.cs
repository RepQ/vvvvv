 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D enemyRigidBody;
    public Player player;

    [Header("Enemy Stats")]
    public float enemyVelocityX;
    public float enemyVelocityY;
    public float enemyDamage;
    public float enemyLife;
    public float enemyPushPlayer;
    // Start is called before the first frame update
    public virtual void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    public virtual void EnemyMovement()
    {
        enemyRigidBody.velocity = new Vector2(enemyVelocityX, enemyVelocityY);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == gameObject.layer)
        {
            enemyVelocityX *= -1;
            enemyVelocityY *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, 
                transform.localScale.y, transform.localScale.z);
        }
        else
        {
            DamagePlayer();
        }
    }

    public virtual void DamagePlayer()
    {
        player.rg2d.AddForce(new Vector2(-player.horizontalDirection, -player.verticalDirection) * enemyPushPlayer, ForceMode2D.Impulse);
        player.animatorPlayer.SetTrigger("hitBy");
        Debug.Log("isHit");
    }

    public virtual void Update()
    {
        if (player == null)
        {
            player = GameManager.gameManager.player;
        }
    }
}
