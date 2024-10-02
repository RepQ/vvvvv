using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public LayerMask groundLayer;
    public Rigidbody2D rg2d;
    public Vector2 velocity;
    public Vector2 position;
    private RaycastHit2D[] hits;
    private bool isGrounded;
    private float horizontalDirection;
    private float verticalDirection;


    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hits = new RaycastHit2D[3];
        position = GameManager.gameManager.playerPositionInit;
        velocity = GameManager.gameManager.playerVelocityinit;

        rg2d.position = position;
        rg2d.rotation = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int i = -1;
        isGrounded = false;
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        verticalDirection = - rg2d.transform.up.y;

        hits[0] = Physics2D.Raycast(rg2d.position, new Vector2(0, verticalDirection), 2.0f, groundLayer);
        hits[1] = Physics2D.Raycast(rg2d.position, new Vector2(-transform.localScale.x / 2, transform.localScale.y / 2).normalized, 2.0f, groundLayer);
        hits[2] = Physics2D.Raycast(rg2d.position, new Vector2(transform.localScale.x / 2, transform.localScale.y / 2).normalized, 2.0f, groundLayer);

        Debug.DrawLine(rg2d.transform.position, rg2d.transform.position - rg2d.transform.up * 2.0f, Color.yellow);
        Debug.DrawLine(rg2d.position, rg2d.transform.position - (new Vector3(-transform.localScale.x / 2, transform.localScale.y / 2).normalized) * 2.0f, Color.red);
        Debug.DrawLine(rg2d.position, rg2d.transform.position - (new Vector3(1, 1.5f, 0).normalized) * 2.0f, Color.red);
        while (++i < 3)
        {
            if (hits[i].collider != null) { isGrounded = true; }
        }
        Debug.Log(isGrounded);
        rg2d.velocity = new Vector2(horizontalDirection * velocity.x,
            Mathf.Clamp(rg2d.velocity.y, -velocity.y, velocity.y));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rg2d.gravityScale *= -1;
            rg2d.rotation += 180f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            GameManager.gameManager.DeathPlayerHandle();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ZoneHorizontal"))
        {
        }
        else if (collision.gameObject.CompareTag("ZoneVertical"))
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
