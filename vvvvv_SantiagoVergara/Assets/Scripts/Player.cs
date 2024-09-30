using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public delegate void onDeath();
    public event onDeath deathPlayer;

    public delegate void onChangeZone(Collider2D collision, float direction);
    public event onChangeZone changeZone;

    public LayerMask wallLayer;
    public Rigidbody2D rg2d;
    public Vector2 velocity;
    public Vector2 position;
    private bool isGrounded;
    private float strength;

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
        strength = Input.GetAxis("Horizontal");

        RaycastHit2D hitWall = Physics2D.Raycast(rg2d.transform.position, Vector2.right * Mathf.Sign(strength), 1.0f, wallLayer);
        if (hitWall.collider != null)
        {
            rg2d.velocity = new Vector2(0, 
                Mathf.Clamp(rg2d.velocity.y, -velocity.y, velocity.y));
            Debug.LogWarning("Raycast collider");
            //rg2d.AddForce(new Vector2(0, rg2d.gravityScale * 10f), ForceMode2D.Force);
        }
        else
        {
            rg2d.velocity = new Vector2(strength * velocity.x,
                Mathf.Clamp(rg2d.velocity.y, -velocity.y, velocity.y));
        }
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ZoneHorizontal"))
        {
            changeZone?.Invoke(collision, Mathf.Sign(strength));
        }
    }


    public void ResetPlayer()
    {
        rg2d.velocity = Vector2.zero;
        rg2d.position = position;
        rg2d.gravityScale = Mathf.Abs(rg2d.gravityScale);
    }
}
