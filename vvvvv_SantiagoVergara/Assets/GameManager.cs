using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public Player player;
    public OnDeath DeathPlayer;
    public int playerLifes;
    public Vector2 playerVelocityinit;
    public Vector2 playerPositionInit;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DeathPlayer += OnDeathPlayer;
            DeathPlayer += LogicDeathPlayer;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDeathPlayer()
    {
        Debug.LogWarning("Muerte del player");
        DeathPlayer?.Invoke();
    }
    public void LogicDeathPlayer()
    {

        Destroy(player.gameObject);
    }
}
