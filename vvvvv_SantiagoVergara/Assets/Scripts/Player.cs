using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public delegate void onDeath();
    public event onDeath deathPlayer;

    public delegate void onChangeZone(Collider2D collision);
    public event onChangeZone changeZone;

    public Rigidbody2D rg2d;
    public Vector2 velocity;
    public Vector2 position;

    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        deathPlayer -= GameManager.gameManager.DeathPlayerHandle;
        changeZone -= GameManager.gameManager.ChangeZoneHandle;
    }
    // Start is called before the first frame update
    void Start()
    {
        position = GameManager.gameManager.playerPositionInit;
        velocity = GameManager.gameManager.playerVelocityinit;

        rg2d.transform.position = position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float strength = Input.GetAxis("Horizontal");

        rg2d.velocity = new Vector2(strength * velocity.x, 
            Mathf.Clamp(rg2d.velocity.y, -velocity.y, velocity.y));
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
        if (collision.gameObject.CompareTag("Death"))
        {
            deathPlayer?.Invoke();
        }

        if (collision.gameObject.CompareTag("ZoneRight"))
        {
            changeZone?.Invoke(collision);
        }
        else if (collision.gameObject.CompareTag("ZoneLeft"))
        {

        }
    }


    public void ResetPlayer()
    {
        rg2d.velocity = Vector2.zero;
        rg2d.position = position;
        rg2d.gravityScale = Mathf.Abs(rg2d.gravityScale);
    }
}
