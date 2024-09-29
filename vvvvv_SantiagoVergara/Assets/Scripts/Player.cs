using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDeath();
public class Player : MonoBehaviour
{
    public Rigidbody2D rg2d;
    private Vector2 velocity;
    private Vector2 position;

    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        velocity = GameManager.gameManager.playerVelocityinit;
        position = GameManager.gameManager.playerPositionInit;

        rg2d.transform.position = position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float strength = Input.GetAxis("Horizontal");

        rg2d.velocity = new Vector2(strength * velocity.x, rg2d.velocity.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rg2d.gravityScale *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("death"))
        {
            if (GameManager.gameManager.DeathPlayer != null)
            {
                GameManager.gameManager.DeathPlayer();
            }
        }
    }
}
