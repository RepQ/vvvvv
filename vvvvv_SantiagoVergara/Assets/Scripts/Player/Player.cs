using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask deathLayer;

    [Header("References")]
    public Animator animatorPlayer;
    public SpriteRenderer playerSprite;
    public Rigidbody2D rg2d;
    private CapsuleCollider2D playerCollider;
    private CameraMove cameraFollower;
    private LifeBar lifeBar;

    [Header("Player Data")]
    public float playerLife = 100f;
    public float playerGravity;
    public Vector2 playerVelocity;
    public Vector2 playerPosition;
    public float horizontalDirection;
    public float verticalDirection;

    private RaycastHit2D[] hits = new RaycastHit2D[3];

    public bool isDeath = false;
    private bool isGrounded = false;
    private bool isGravityInverted = false;
    private bool canDash = true;
    public bool isDashing = false;

    [SerializeField] float raycastDistance;
    [SerializeField] float dashImpulse;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashDuration;
    private float dashTimeRemaining = 0f;
    private float lastTimeDash = 0f;


    private void Awake()
    {
        cameraFollower = GameManager.gameManager.mainCamara;
        lifeBar = GameManager.gameManager.playerLifeBar;
    }

    // Start is called before the first frame update
    void Start()
    {

        rg2d = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerPosition = GameManager.gameManager.playerPositionInit;
        playerVelocity = GameManager.gameManager.playerVelocityinit;
        playerGravity = GameManager.gameManager.playerGravity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGrounded();
    }

    private void Update()
    {
        PlayerMove();
        InvertGravityPlayer();
        if (playerLife <= 0f)
            isDeath = true;
        if (isDeath)
            GameManager.gameManager.DeathPlayerHandle();
    }

    private void OnEnable()
    {
        GameManager.gameManager.player = this;
        SetCamera();
        SetLifeBar();
    }

    public void SetCamera()
    {
        Vector3 cameraPosition = cameraFollower.transform.position;

        cameraFollower.playerToFollow = GetComponent<Player>();
        cameraFollower.CameraON = true;
        cameraFollower.transform.position = new Vector3(playerPosition.x, cameraPosition.y, cameraPosition.z);
        Debug.Log(cameraFollower.playerToFollow.name);
    }

    public void SetLifeBar()
    {
        lifeBar.player = this;
        lifeBar.currentLifePlayer = playerLife;
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
        rg2d.AddForce(new Vector2(horizontalDirection * dashImpulse, 0), ForceMode2D.Impulse);
        animatorPlayer.SetTrigger("dash");
        dashTimeRemaining = dashDuration;
        lastTimeDash = Time.time;
        canDash = false;
        isDashing = true;
    }

    private void ResetDash()
    {
        canDash = true;
    }
    private void InvertGravityPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGravityInverted = !isGravityInverted;
            rg2d.gravityScale *= -1;
        }
        animatorPlayer.SetBool("isGrounded", isGrounded);
    }
    private void PlayerMove()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");

        HandleDashPlayer();
        if (!isDashing)
        {
            rg2d.velocity = new Vector2(horizontalDirection * playerVelocity.x,
                Mathf.Clamp(rg2d.velocity.y, -playerGravity, playerGravity));
        }
        animatorPlayer.SetFloat("horizontal", rg2d.velocity.x);

        playerPosition = rg2d.position;
        CheckDirectionPlayer();
    }
    private void CheckGrounded()
    {
        isGrounded = false;
        Vector2 raycastDirection = isGravityInverted ? Vector2.up : Vector2.down;
        Vector2 raycastOrigin = playerPosition +
            (isGravityInverted ? Vector2.up : Vector2.down) *
            new Vector2(0, playerCollider.size.y / 2);
        Vector2 raycastOffset = new(playerCollider.size.x / 2, 0);

        hits[0] = Physics2D.Raycast(raycastOrigin, raycastDirection, raycastDistance, groundLayer);
        hits[1] = Physics2D.Raycast(raycastOrigin + raycastOffset,
            raycastDirection, raycastDistance, groundLayer);
        hits[2] = Physics2D.Raycast(raycastOrigin - raycastOffset,
            raycastDirection, raycastDistance, groundLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null) isGrounded = true;
        }
        Debug.Log(isGrounded);
    }

    private void CheckDeathLayer()
    {
        //isDeath = false;

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
        verticalDirection = isGravityInverted ? 1 : -1;
        playerSprite.flipY = isGravityInverted;
    }
    public void ResetPlayer()
    {
        isDeath = false;
        playerLife = 100f;
        rg2d.velocity = Vector2.zero;
        rg2d.gravityScale = Mathf.Abs(rg2d.gravityScale);
        isGravityInverted = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            GameManager.gameManager.DeathPlayerHandle();
        }
    }
}
