using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public delegate void OnChangeZone(Collider2D collision, float direction);
public delegate void OnDeath();

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
        player.OnDeathPlayer += DeathPlayerHandle;
        player.OnChangeZone += ChangeZoneHandle;
    }

    private void OnDisable()
    {
        player.OnChangeZone -= ChangeZoneHandle;
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
        Camera.InitPosition();
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
        Camera.ChangeScenarioHorizontal(collision, direction);
    }
}
