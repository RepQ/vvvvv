using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public LayerMask groundLayer;
    private Animator animatorPlayer;
    public Rigidbody2D rg2d;
    public Vector2 velocity;
    public Vector2 position;
    private RaycastHit2D[] hits;
    private bool isGrounded = false;
    private bool isGravityInverted = false;
    private float horizontalDirection;

    private readonly float raycastDistance = 0.5f;


    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
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
        CheckGrounded();
        PlayerMove();
    }

    private void Update()
    {
        InvertGravityPlayer();
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

    private void InvertGravityPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGravityInverted = !isGravityInverted;
            rg2d.gravityScale *= -1;
            //rg2d.rotation += 180f;
        }
    }
    private void PlayerMove()
    {
        rg2d.velocity = new Vector2(horizontalDirection * velocity.x,
            Mathf.Clamp(rg2d.velocity.y, -velocity.y, velocity.y));
        animatorPlayer.SetFloat("horizontal", rg2d.velocity.x);
    }
    private void CheckGrounded()
    {
        int i = -1;
        isGrounded = false;
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        Vector2 raycastDirection = isGravityInverted ? Vector2.up : Vector2.down;
        Vector2 raycastOrigin = rg2d.position +
            (isGravityInverted ? Vector2.up : Vector2.down) *
            new Vector2(0, transform.localScale.y / 2);
        Vector2 raycastOffset = new(transform.localScale.x / 2, 0);

        hits[0] = Physics2D.Raycast(raycastOrigin, raycastDirection, raycastDistance, groundLayer);
        hits[1] = Physics2D.Raycast(raycastOrigin + raycastOffset,
            raycastDirection, raycastDistance, groundLayer);
        hits[2] = Physics2D.Raycast(raycastOrigin - raycastOffset,
            raycastDirection, raycastDistance, groundLayer);

        Debug.DrawLine(raycastOrigin, raycastOrigin - raycastDirection * raycastDistance, Color.red);
        Debug.DrawLine(raycastOrigin + raycastOffset, raycastOrigin + raycastOffset - raycastDirection * raycastDistance, Color.red);
        Debug.DrawLine(raycastOrigin - raycastOffset, raycastOrigin - raycastOffset - raycastDirection * raycastDistance, Color.red);

        while (++i < 3)
        {
            if (hits[i].collider != null) { isGrounded = true; }
        }
        Debug.Log(isGrounded);
    }

    public void ResetPlayer()
    {
        rg2d.velocity = Vector2.zero;
        rg2d.position = position;
        rg2d.gravityScale = Mathf.Abs(rg2d.gravityScale);
        isGravityInverted = false;
    }
}
