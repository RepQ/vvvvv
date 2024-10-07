using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    public LayerMask groundLayer;
    public Animator animatorPlayer;
    public SpriteRenderer playerSprite;
    private CapsuleCollider2D playerCollider;
    public Rigidbody2D rg2d;
    public Vector2 velocity;
    public Vector2 position;
    private float horizontalDirection;
    private RaycastHit2D[] hits;

    private bool isGrounded = false;
    private bool isGravityInverted = false;
    private bool canDash = true;
    public bool isDashing;

    [SerializeField] float raycastDistance = 1.5f;
    [SerializeField] float dashImpulse = 1200f;
    [SerializeField] float dashCooldown = 2f;
    [SerializeField] float dashDuration = 0.2f;
    private float dashTimeRemaining = 0f;

    private float lastTimeDash = 0f;


    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hits = new RaycastHit2D[3];
        position = GameManager.gameManager.playerPositionInit;
        velocity = GameManager.gameManager.playerVelocityinit;

        ResetPlayer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGrounded();
        CheckDirectionPlayer();
        PlayerMove();
    }

    private void Update()
    {
        InvertGravityPlayer();
        HandleDashPlayer();
        Debug.Log(rg2d.velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            GameManager.gameManager.DeathPlayerHandle();
        }
    }

    private void HandleDashPlayer()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && canDash && Time.time > lastTimeDash + dashCooldown)
        {
            Dash();
        }
        if (isDashing)
        {
            dashTimeRemaining -= Time.deltaTime;
            {
                if (dashTimeRemaining <= 0)
                {
                    isDashing = false;
                    animatorPlayer.SetTrigger("noDash");
                    Invoke(nameof(ResetDash), dashCooldown - dashDuration);
                }
            }
        }
    }

    private void Dash()
    {
        rg2d.velocity = new Vector2(horizontalDirection * (velocity.x + dashImpulse),
            Mathf.Clamp(rg2d.velocity.y, -velocity.y, velocity.y));
        //rg2d.AddForce(new Vector2(horizontalDirection * dashImpulse, 0), ForceMode2D.Impulse);
        animatorPlayer.SetTrigger("dash");
        canDash = false;
        dashTimeRemaining = dashDuration;
        isDashing = true;
        lastTimeDash = Time.time;
    }

    private void ResetDash()
    {
        canDash = true;
    }
    private void InvertGravityPlayer()
    {
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            isGravityInverted = !isGravityInverted;
            rg2d.gravityScale *= -1;
        }
        animatorPlayer.SetBool("isGrounded", isGrounded);
    }
    private void PlayerMove()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        if (!isDashing)
        {
            rg2d.velocity = new Vector2(horizontalDirection * velocity.x,
                Mathf.Clamp(rg2d.velocity.y, -velocity.y, velocity.y));
        }
        animatorPlayer.SetFloat("horizontal", rg2d.velocity.x);
    }
    private void CheckGrounded()
    {
        int i = -1;
        isGrounded = false;
        Vector2 raycastDirection = isGravityInverted ? Vector2.up : Vector2.down;
        Vector2 raycastOrigin = rg2d.position +
            (isGravityInverted ? Vector2.up : Vector2.down) *
            new Vector2(0, playerCollider.size.y / 2);
        Vector2 raycastOffset = new(playerCollider.size.x / 2, 0);

        hits[0] = Physics2D.Raycast(raycastOrigin, raycastDirection, raycastDistance, groundLayer);
        hits[1] = Physics2D.Raycast(raycastOrigin + raycastOffset,
            raycastDirection, raycastDistance, groundLayer);
        hits[2] = Physics2D.Raycast(raycastOrigin - raycastOffset,
            raycastDirection, raycastDistance, groundLayer);

        //Debug.DrawLine(raycastOrigin, raycastOrigin - raycastDirection * raycastDistance, Color.red);
        //Debug.DrawLine(raycastOrigin + raycastOffset, raycastOrigin + raycastOffset - raycastDirection * raycastDistance, Color.red);
        //Debug.DrawLine(raycastOrigin - raycastOffset, raycastOrigin - raycastOffset - raycastDirection * raycastDistance, Color.red);

        while (++i < 3)
        {
            if (hits[i].collider != null) { isGrounded = true;}
        }
        Debug.Log(isGrounded);
    }

    private void CheckDirectionPlayer()
    {
        if (horizontalDirection == 1)
        {
            playerSprite.flipX = false;
        }
        else if (horizontalDirection == -1)
        {
            playerSprite.flipX = true;
        }
        playerSprite.flipY = isGravityInverted;
    }
    public void ResetPlayer()
    {
        rg2d.velocity = Vector2.zero;
        rg2d.position = position;
        rg2d.gravityScale = Mathf.Abs(rg2d.gravityScale);
        isGravityInverted = false;
    }
}
