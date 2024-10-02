using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public delegate void OnChangeZone(Collider2D collision, float direction);
public delegate void OnDeath();

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    private Camera mainCamara;
    private Vector2 sizeViewCamara;

    public Player player;
    public int playerLifes;
    public Vector2 playerVelocityinit;
    public Vector2 playerPositionInit;


    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamara = Camera.main;
        sizeViewCamara.x = mainCamara.orthographicSize * mainCamara.aspect;
        sizeViewCamara.y = mainCamara.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathPlayerHandle()
    {
        //Camera.InitPosition();
        player.ResetPlayer();
        if (--playerLifes <= 0)
        {
            DestroyGameObject(player.gameObject);
        }
    }

    public void DestroyGameObject(GameObject obj)
    {
        Destroy(obj);
    }

    public void ChangeZoneHandle(Collider2D collision, float direction)
    {
        if (collision.gameObject.CompareTag("ZoneHorizontal"))
        {
            //Camera.ChangeScenarioHorizontal(collision, direction);
        }
        else if (collision.gameObject.CompareTag("ZoneVertical"))
        {
            //Camera.ChangeScenarioVertical(collision, direction);
        }
    }
}
