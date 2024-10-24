using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public Stack<GameObject> stack;
    public Spawner playerSpawner;

    public CameraMove mainCamara;

    [Header("References")]
    public Player player;

    [Header("PLayer Stats")]
    public bool playerIsDeath = false;
    public int playerLifes;
    public Vector2 playerVelocityinit;
    public Vector2 playerPositionInit;
    public float playerGravity;


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
        stack = new Stack<GameObject>();
        playerPositionInit = playerSpawner.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathPlayerHandle()
    {
        player.ResetPlayer();
        playerSpawner.ReSpawn(player.gameObject);
        if (--playerLifes <= 0)
        {
            playerSpawner.Push(player.gameObject);
        }
    }

    public void DestroyGameObject(GameObject obj)
    {
        Destroy(obj);
    }

    public void ChangeScene(string sceneName)
    {
        playerSpawner.Push(player.gameObject);
        SceneManager.LoadScene(sceneName);
    }
}
