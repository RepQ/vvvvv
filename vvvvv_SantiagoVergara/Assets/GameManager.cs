using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public CameraMove Camera;

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

    private void OnEnable()
    {
        player.deathPlayer += DeathPlayerHandle;
        player.changeZone += ChangeZoneHandle;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathPlayerHandle()
    {
        player.ResetPlayer();
        if (--playerLifes <= 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    public void ChangeZoneHandle(Collider2D collision)
    {
        Camera.ChangeScenarioRight(collision);
    }
}
