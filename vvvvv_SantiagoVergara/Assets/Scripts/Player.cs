using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public event OnChangeZone OnChangeZone;
    public event OnDeath OnDeathPlayer;
    public LayerMask groundLayer;
    public Rigidbody2D rg2d;
    public Vector2 velocity;
    public Vector2 position;
    private bool isGrounded;
    private Vector2 playerDirection;
    private float horizontalDirection;
    private float verticalDirection;

    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        OnDeathPlayer -= GameManager.gameManager.DeathPlayerHandle;
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
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        verticalDirection = rg2d.velocity.normalized.y;
        playerDirection = new Vector2(horizontalDirection, verticalDirection);

        //RaycastHit2D hitWall = Physics2D.Raycast(rg2d.transform.position, Vector2.right * Mathf.Sign(horizontalDirection), 1.0f, wallLayer);
        RaycastHit2D hitGround = Physics2D.Raycast(rg2d.transform.position, Vector2.down * Mathf.Sign(rg2d.gravityScale), 3.0f, groundLayer);
        if (hitGround.collider != null)
        {
            isGrounded = true;
            //Debug.LogWarning("is Grounded!");
            //rg2d.AddForce(new Vector2(0, rg2d.gravityScale * 10f), ForceMode2D.Force);
        }
        else
        {
            isGrounded= false;
        }
        rg2d.velocity = new Vector2(horizontalDirection * velocity.x,
            Mathf.Clamp(rg2d.velocity.y, -velocity.y, velocity.y));
    }

    private void Update()
    {
        Debug.LogWarning(playerDirection.ToString());
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rg2d.gravityScale *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            OnDeathPlayer?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ZoneHorizontal"))
        {
            OnChangeZone?.Invoke(collision, Mathf.Sign(horizontalDirection));
        }
        else if (collision.gameObject.CompareTag("ZoneVertical"))
        {
            OnChangeZone?.Invoke(collision, Mathf.Sign(verticalDirection));
        }
    }


    public void ResetPlayer()
    {
        rg2d.velocity = Vector2.zero;
        rg2d.position = position;
        rg2d.gravityScale = Mathf.Abs(rg2d.gravityScale);
    }
}
